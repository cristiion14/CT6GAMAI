using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Timers;
using UnityEngine.UI;
public class Veorica : MonoBehaviour {

    // variables to store the extremities of the screen used for evading
    public Vector3 topR = new Vector3(14.33f, 1.02f, 13.7f);
    public Vector3 topL = new Vector3(-13.48f, 1.02f, 13.7f);
    public Vector3 bottomR = new Vector3(-13.48f, 1.02f, -14.25f);
    public Vector3 bottomL = new Vector3(13.48f, 1.02f, -14.25f);
    

   //variables for desirability
    public float healthDesire;
    float distanceFromHealth;                                   
    public bool isCloseToHealth = false;                        


    /// <summary>
    /// Check to see if the agent is facing the other agent,
    /// used by FaceObj function
    /// </summary>
    public bool lookAtPlayer;

    /// <summary>
    /// the speed of the agent
    /// </summary>
    public float travelSpeed = 0.00001f;

    /// <summary>
    /// Health of Veorica
    /// </summary>
    public float health = 100f;

    /// <summary>
    /// The waypoints array
    /// Used for Path Following
    /// </summary>
    public  Vector3[] direction;
    public int targetIndex;                                     //the index of the current waypoint used for path following

    Rigidbody rb;                                               //reference to the rigid body

    public bool hasDied = false;

    public Text moneyAmount;                                    //UI text for money
    public Image healthBar;                                     //UI image for health bar
    public float money = 0;                                     //how many coins has collected

    //references to the coin
    public GameObject coin;
    public GameObject coinPrefab;

    public int nr = 0;                                          //for chased state
    /// <summary>
    /// Controls the Finite State Machines
    /// Used by Veorica
    /// </summary>
    State<Veorica> fsm;

    /// <summary>
    /// //distance from coins;
    /// </summary>
    public float distance;                                      
    public bool isFound;                                        //if has found iohannis
    public float lookRadius = 25f;                              //perception radius


    GameObject[] coins;                                         //the place holders for the coins
    public int randNrX;                                         //Random number used for instantiating coins

    public List<SimplifiedNode> vFinalPath;                     //The completed path that the red line will be drawn along


    //for shooting...
    public GameObject bullet;
    public GameObject bulletPoint;
    float timeBtwShoots;                                        //The fire rate
    public float startTimeBtwShoots;

    public GameObject iohannis;                                                 
    public GameObject GM;
    
    /// <summary>
    /// Utility Theory to receive the health desirability
    /// </summary>
    public void GetHealthDesireability()
    {
        float k = .1f;        //constant

        //if the health was spawned
        if (GM.GetComponent<GameManager>().spawnedHealth)
        {
            //get the distance from the health pack
            Vector3 healthPackDir = GM.GetComponent<GameManager>().healthPack.transform.position - transform.position;
            distanceFromHealth = healthPackDir.sqrMagnitude;

            //set the distance to health to an arbitrary number based on the report sheet formula
            if (distanceFromHealth <= 25f)
                isCloseToHealth = true;
            else
                isCloseToHealth = false;

            if (!isCloseToHealth)
            {
                if (distanceFromHealth > 26.0f * 26.0f)
                    distanceFromHealth = 1f;            //very far
                else if (distanceFromHealth <= 100f && distanceFromHealth > 25f)
                    distanceFromHealth = 0.1f;          //close
                else if (distanceFromHealth > 100f && distanceFromHealth < 17.5f * 17.5f)
                    distanceFromHealth = 0.25f;
                else if (distanceFromHealth >= 17.5f * 17.5f && distanceFromHealth < 25.5f * 25.5f)
                    distanceFromHealth = 0.5f;
                else
                    distanceFromHealth = 0.25f;
            }

            Debug.Log("Thhe distance from health is: " + distanceFromHealth);
            //   distanceFromHealth = Mathf.Clamp()

            float healthStatus = health / 100;             // alive or not (1 or 0) 


            if (isCloseToHealth && health < 100)
                healthDesire = 1;               // is very close the the health
            else
                healthDesire = k * ((1 - healthStatus) / distanceFromHealth);       //formula to calculate the desirability
            Debug.Log("The health desire of Veorica is: " + healthDesire);
        }
            
    }
    void Awake()
    {
       //analog as in Trigger enter for random nr 
        randNrX = Random.Range(0, 23);

        // update the money amont text
        moneyAmount.text = money.ToString();

        //get references to needed objects
        iohannis = GameObject.Find(TagManager.Iohannis);
        GM = GameObject.Find("GM");
        rb = GetComponentInChildren<Rigidbody>();
        coins = GameObject.FindGameObjectsWithTag("CoinPoint");
        coin =  Instantiate(coinPrefab, coins[randNrX].transform.position, transform.rotation);
    }
    void Start()
    {
        // initialize the first state
        fsm = new Stealing();
        
        //set the timer for shooting
        timeBtwShoots = startTimeBtwShoots;
     
    }

    /// <summary>
    /// Checks to see if the target is within the sight radius
    /// </summary>
    public void targetFound()
    {
        // get better performance using .sqrmagnitude
         float distance = (iohannis.transform.position - transform.position).sqrMagnitude;
        if (distance <= lookRadius*lookRadius)
            isFound=true;
        else
            isFound= false;
    }
    /// <summary>
    /// Function which makes the agent follow a path to a target
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="target"></param>
    public void SetDestination(Transform transform, Vector3 target)
    {
        //start by finding the path
        GM.GetComponentInChildren<SimplifiedPathFinder>().FindPath(transform.position, target); 

        //initialise a list of waypoints and add to it the final path nodes position
        List<Vector3> wayPoints = new List<Vector3>();
        for (int i = 0; i < vFinalPath.Count; i++)
        {
            wayPoints.Add(vFinalPath[i].vPosition);
        }

        //convert the list to an array and pass it to the direction array
        direction = wayPoints.ToArray();
        
        //set the current waypoint to the first element from the waypoint array
        Vector3 currentWaypoint = direction[0];

        //if the agent reached the point, move it to the next one
        if (transform.position == currentWaypoint)
                targetIndex++;

        //if it's outside the waypoint, reset it back to 0
        if (targetIndex >= direction.Length)
                targetIndex = 0;

        //update the current waypoint
        currentWaypoint = direction[targetIndex];

        //update the position of the agent
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, travelSpeed*Time.fixedDeltaTime);
    }


    /// <summary>
    /// Used for facing the coins and evading points
    /// </summary>
    /// <param name="obj"></param>
    public void FaceObj(Vector3 obj)
    {
        lookAtPlayer = false;               //is not looking at player

        //get the facing direction and normalize it
        Vector3 direction = (obj - transform.position).normalized;

        //make the agent rotate
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 5f);
    }

    void Update ()
    {
        //keep running the state machine
        fsm.Execute(this);

        //fill the health bar
        healthBar.fillAmount = health / 100;

        //get health desire
        GetHealthDesireability();
    }   

    /// <summary>
    /// Display the sight perception radius
    /// </summary>
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,lookRadius);
    }

    /// <summary>
    /// This function is making Veorica to face iohannis
    /// </summary>
    void FaceTarget()     
    {
        //get the direction normalized from the agents and based on that direction, rotate the agent
        Vector3 direction = (iohannis.transform.position - transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 5);
        lookAtPlayer = true;
    }

  
  
    public void Shoot()
    {
        //if it hasn't shotted yet
        if(timeBtwShoots<=0)
        {
            //shoot
            Instantiate(bullet, bulletPoint.transform.position, bullet.transform.rotation);
            timeBtwShoots = startTimeBtwShoots;
           
        }
        else
        {
            timeBtwShoots -= Time.deltaTime;
        }
    }

   private void OnTriggerEnter(Collider other)
    {
        //if has touched the coin
        if(other.tag =="Coin")
        {
            //set the rand spawn number to match one of the coin holders index
            randNrX = Random.Range(0, 23);
            
            // increase money and update the text
            money += 1;
            moneyAmount.text = money.ToString();

            //destroy the game object and instantiate a new one
            Destroy(other.gameObject);
            coin = Instantiate(coinPrefab, coins[randNrX].transform.position, transform.rotation);
            
        }       
    }
    /// <summary>
    /// Function to change the state
    /// </summary>
    /// <param name="newState"></param>
    public void ChangeState(State<Veorica> newState)
    {
        fsm = newState;
    }
}
