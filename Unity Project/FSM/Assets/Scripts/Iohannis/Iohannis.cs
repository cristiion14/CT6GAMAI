using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class Iohannis : MonoBehaviour {
    

    State<Iohannis> fsm;
              
    [SerializeField]
   public float travelSpeed = 7f;
    public Image healthBar;
    public float health = 100f;
    public float healthDesire;
    public GameObject healthPackPrefab;
   public GameObject healthPack;
    public bool spawnedHealth = false, tookHealth = false, isCloseToHealth = false;
    int randNrY;
    GameObject[] healthPackHolders;
    float distanceFromHealth;
    public bool lookAtPlayer;

   public Text healthDesireTxT;


    public List<SimplifiedNode> iFinalPath;         //The path received by pathfinding
   public Vector3[] path;                           //the waypoints array
    public int targetIndex;                         //index in the waypoints array

    public bool hasDied = false;

    public Transform target;
    public Transform[] patrolPoints;

    public float lookRad = 25f;
    public float startTimeBtwShoots, timeBtwShoots;
    public GameObject bullet, bulletPoint;

    public int nr = 0, hasBeenFoundAtNr=0;  //the path nr and the nr which keeps track at what path nr has been found
   public GameObject GM;

    private void Awake()
    {
        healthPackHolders = GameObject.FindGameObjectsWithTag("CoinPoint");
        GM = GameObject.Find("GM");
    }
    void Start () {

        fsm = new Patrol();
        target = AgentManager.instance.enemy2.transform;
        timeBtwShoots = startTimeBtwShoots;
    }
	
   
	// Update is called once per frame
	void Update () {
        fsm.Execute(this);
        healthBar.fillAmount = health / 100;
        GetHealthDesireability();
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

    /// <summary>
    /// Function which makes the agent follow a path to a target
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="target"></param>
    public void SetDestination(Transform transform, Vector3 target)
    {
        //start by finding the path
        GM.GetComponentInChildren<SimplifiedPathFinder>().iFindPath(transform.position, target);

        //initialise a list of waypoints and add to it the final path nodes position
        List<Vector3> wayPoints = new List<Vector3>();
        for (int i = 0; i < iFinalPath.Count; i++)
        {
            wayPoints.Add(iFinalPath[i].vPosition);
        }
        //convert the list to an array and pass it to the direction array
        path = wayPoints.ToArray();
        
        //set the current waypoint to the first element from the waypoint array
        Vector3 currentWaypoint = path[0];

        //if the agent reached the point, move it to the next one
        if (transform.position == currentWaypoint)
            targetIndex++;
     
        //if it's outside the range, reset it back to 0
        if (targetIndex >= path.Length)
            targetIndex = 0;

        //update the current waypoint
        currentWaypoint = path[targetIndex];
       
        //update the position of the agent
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, travelSpeed * Time.fixedDeltaTime);

    }
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
            healthDesireTxT.text = healthDesire.ToString();

        }
        else
            healthDesire = 0;
    }

    /// <summary>
    /// Checks to see if the target is within the sight radius
    /// </summary>
    public bool targetFound()
    {
        float distance = (target.position - transform.position).sqrMagnitude;
        if (distance <= lookRad*lookRad)
            return true;
        else
            return false;
    }
   
   public void FaceTarget()
    {
        //get the direction normalized from the agents and based on that direction, rotate the agent

        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 5);
        lookAtPlayer = true;
    }

    /// <summary>
    /// used to face the patrol points
    /// </summary>
    /// <param name="patrolPoint"></param>
    public void FacePatrolPoint(int patrolPoint)
    {  
            Vector3 direction = (patrolPoints[patrolPoint].transform.position - transform.position).normalized;
            Quaternion lookRot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 5);
        
    }
    /// <summary>
    /// used to draw the perception sight sphere
    /// </summary>
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, lookRad);
    }

    /// <summary>
    /// Shooting function
    /// </summary>
   public void Shoot()
    {
        if (timeBtwShoots <= 0)
        {
            Instantiate(bullet, bulletPoint.transform.position, Quaternion.identity);
            timeBtwShoots = startTimeBtwShoots;

        }
        else
        {
            timeBtwShoots -= Time.deltaTime;
        }
    }
    /// <summary>
    /// Function which changes the state
    /// </summary>
    /// <param name="newState"></param>
    public void ChangeState(State<Iohannis> newState)
    {
        fsm = newState;
    }
}
