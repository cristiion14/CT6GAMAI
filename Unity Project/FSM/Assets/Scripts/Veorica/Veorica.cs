using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Veorica : MonoBehaviour {

    public Vector3 topR = new Vector3(14.33f, 1.02f, 13.7f);
    public Vector3 topL = new Vector3(-13.48f, 1.02f, 13.7f);
    public Vector3 bottomR = new Vector3(-13.48f, 1.02f, -14.25f);
    public Vector3 bottomL = new Vector3(13.48f, 1.02f, -14.25f);
    
    public GameObject healthPack;
    public bool lookAtPlayer;
    public float travelSpeed = 0.00001f;
  public  Vector3[] direction;
    public Vector3 evadeDirection;
    Rigidbody rb;
    public float money = 0;
    public int coinNr = 0;
    public int nrPath = 0;
    public GameObject coin;
   

    public int nr = 0; //for chased state
   // public int stealNr = 0;

    StateManager<Veorica> fsm = new StateManager<Veorica>();

   public float distance;
    public NavMeshAgent agent;
    public bool foundPlayer, isFound;
    public float lookRadius = 25f;
    Transform target;

    Pathfinding pathFinding;
    SimplifiedPathFinder simplifyPath;
    SimplifiedGrid sGrid;
    public List<SimplifiedNode> vFinalPath;//The completed path that the red line will be drawn along
    public List<SimplifiedNode>[] FinalPath;
    //for shooting
    public GameObject bullet;
    public GameObject bulletPoint;
    public GameObject iohannis;
    public GameObject[] coins;
     GameObject GM;
   public float randNrX, randNrY, randNrZ;

    Vector3[] path;
    public int targetIndex;

    public float health = 100f;
    Grid grid;

    float timeBtwShoots;
    public float startTimeBtwShoots;
    void Awake()
    {
        grid = GetComponent<Grid>();
        sGrid = GetComponent<SimplifiedGrid>();
        iohannis = GameObject.Find(TagManager.Iohannis);
        pathFinding = GetComponent<Pathfinding>();
        simplifyPath = GetComponent<SimplifiedPathFinder>();
        GM = GameObject.Find("GM");
        rb = GetComponentInChildren<Rigidbody>();
        Instantiate(coin, new Vector3(12.07f, 1, 1.49f), transform.rotation);

        //coin = GameObject.Find(TagManager.Coin);
    }
    void Start()
    {
        fsm.InIt(new Stealing(), this);
        agent = GetComponent<NavMeshAgent>();
        randNrX = Random.Range(-11f, 15f);
        randNrZ = Random.Range(-14.76f, 14.76f);
        evadeDirection = new Vector3(randNrX, 0, randNrZ);//+agent.transform.position;
       
        timeBtwShoots = startTimeBtwShoots;
        //    direction = pathFinding.direction;
 //       rand = GetComponent<Random>();
        target = AgentManager.instance.player.transform;
    }
    public void targetFound()
    {
        float distance = Vector3.Distance(iohannis.transform.position, transform.position);
        if (distance <= lookRadius)
            isFound=true;
        else
            isFound= false;
    }
    public void SetDestination(Transform transform, Vector3 target)
    {
        //targetIndex = 0;

        GM.GetComponentInChildren<SimplifiedPathFinder>().FindPath(transform.position, target);
        List<Vector3> wayPoints = new List<Vector3>();
        for (int i = 0; i < vFinalPath.Count; i++)
        {
            wayPoints.Add(vFinalPath[i].vPosition);
        }
       // wayPoints.Reverse();
        direction = wayPoints.ToArray();
        
        Vector3 currentWaypoint = direction[0];

        if (transform.position == currentWaypoint)
                targetIndex++;

        if (targetIndex >= direction.Length)
            {
      //      Debug.Log("Should reset targetIndex");
                targetIndex = 0;
     //       Debug.Log("Target index is: " + targetIndex);
             //   direction = new Vector3[0];
            //    break;
            }
  
        currentWaypoint = direction[targetIndex];
   //   Debug.Log("current waypoint is: " + currentWaypoint);
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, travelSpeed*Time.fixedDeltaTime);
    //  transform.position += currentWaypoint*Time.deltaTime* 1.5f;

        
        //  rb.velocity = currentWaypoint;
        
    }

    public void FaceObj(Vector3 obj)
    {
        lookAtPlayer = false;
        Vector3 direction = (obj - transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 5f);
    }

    public void GenerateRandomNr()
    {
        randNrX = Random.Range(-11f, 15f);
        randNrZ = Random.Range(-14.76f, 14.76f);
        evadeDirection = new Vector3(randNrX, 0, randNrZ);//+agent.transform.position;

    }
    // Update is called once per frame
    void Update () {
          fsm.Execute();
     //   Shoot();
     //   FaceTarget();
       if (isFound)
        {
       //     GenerateRandomNr();
        }
        GetDistanceFromCoins();
        //PointLocation();
        //  ChasePlayer();
        //  Patrol();
        //   isChased();
        // Debug.Log(foundPlayer);
        // Debug.Log(nr);
        //    isFound = iohanis.GetComponent<Iohannis>().foundTarget;
        // Debug.Log(distance);

    //      TracePath();
        /*
         
        for(int i=0; i<direction.Length; i++)
        {
            rb.MovePosition(currentWaypoint);
            currentWaypoint = direction[i];
            rb.MovePosition(currentWaypoint);
        }
        */


        //     direction = GM.GetComponentInChildren<SimplifiedPathFinder>().SimplifyPath(vFinalPath);


        /*
        GM.GetComponentInChildren<SimplifiedPathFinder>().FindPath(transform.position, iohannis.transform.position);
       direction= GM.GetComponentInChildren<SimplifiedPathFinder>().RetracePath(GM.GetComponentInChildren<SimplifiedGrid>().NodeFromWorldPoint(transform.position), GM.GetComponentInChildren<SimplifiedGrid>().NodeFromWorldPoint(iohannis.transform.position));
        Vector3 currentWay = direction[0];
        rb.MovePosition(currentWay.normalized*Time.deltaTime);
        */

        //   PathRequestManager.RequestPath1(transform.position, iohannis.transform.position, OnPathFound);
    //    SetDestination(transform);
  
    }

    void FixedUpdate()
    {
     //   SetDestination(transform, iohannis.transform.position);

        //        for (int i=0; i<direction.Length;i++)
        //      {
        //        currentWaypoint = direction[i];
        //      rb.MovePosition(currentWaypoint * Time.fixedDeltaTime);
        //}
    }

    //display the look radius
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,lookRadius);
    }
        
    void FaceTarget()     //this function is making the player to face iohannis
    {
       // Debug.Log("Face Target");
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 5);
        lookAtPlayer = true;
    }

  
    public void TracePath()
    {

        //  Vector3[] direction = pathFinding.RetracePath(grid.NodeFromWorldPoint(agent.transform.position), grid.NodeFromWorldPoint(coins[0].transform.position));

        //    Vector3[] direction = simplifyPath.GetComponent<SimplifiedPathFinder>().RetracePath(sGrid.GetComponent<SimplifiedGrid>().NodeFromWorldPoint(transform.position), sGrid.GetComponent<SimplifiedGrid>().NodeFromWorldPoint(coins[0].transform.position));
        //  GM.GetComponentInChildren<SimplifiedPathFinder>().FindPath(transform.position, iohannis.transform.position);

        /*
        List<Vector3> wayPoints = new List<Vector3>();
        for (int i = 0; i < vFinalPath.Count; i++)
        {
            wayPoints.Add(vFinalPath[i].vPosition);
        }
        direction = wayPoints.ToArray();        
        for (int i = 0; i < direction.Length; i++)
        {
            agent.transform.position += direction[i] * agent.travelSpeed * Time.deltaTime;
        }
        */

        GM.GetComponentInChildren<SimplifiedPathFinder>().FindPath(transform.position, iohannis.transform.position);
     //   direction = GM.GetComponentInChildren<SimplifiedPathFinder>().SimplifyPath(vFinalPath);
        Vector3 currentWaypoint = direction[0];

        while (true)
        {

            if (transform.position == currentWaypoint)
                targetIndex++;
            //   Debug.Log("the target index is: " + targetIndex);
            if (targetIndex >= direction.Length)
            {
                              targetIndex=0;
                              direction = new Vector3[0];
                break;
            }
            currentWaypoint = direction[targetIndex];
            //        Debug.Log("current waypoint is: " + currentWaypoint);
            //  transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, travelSpeed);
            //   transform.position += currentWaypoint*Time.deltaTime* travelSpeed;
            rb.MovePosition(currentWaypoint);
        }
    }
    public void Shoot()
    {
        if(timeBtwShoots<=0)
        {
            Instantiate(bullet, bulletPoint.transform.position, bullet.transform.rotation);
            timeBtwShoots = startTimeBtwShoots;
           
        }
        else
        {
            timeBtwShoots -= Time.deltaTime;
        }
    }

    public void OnPathFound(Vector3[] newPath)
    {
        
            path = newPath;
            targetIndex = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        
    }

    IEnumerator FollowPath()
    {
        // targetIndex = 0;
        Vector3 currentWaypoint = path[0];
        while (true)
        {

            if (transform.position == currentWaypoint)
                targetIndex++;
            //   Debug.Log("the target index is: " + targetIndex);
            if (targetIndex >= path.Length)
            {
                //               targetIndex=0;
                //              path = new Vector3[0];
                yield break;
            }
            currentWaypoint = path[targetIndex];
            //        Debug.Log("current waypoint is: " + currentWaypoint);
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, travelSpeed);
            yield return null;
        }

    }

    /*
    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Coin")
        {
            money += 1;
            Debug.Log("HOW MUCH MONEY YOU GOT?? " + money);
            Destroy(col.gameObject);
        }
    }
    */
    public float GetDistanceFromCoins()
    {
         distance = Vector3.Distance(transform.position, coin.transform.position);
        return distance;
    }
    public int destroyedObj;
   private void OnTriggerEnter(Collider other)
    {
        if(other.tag =="Coin")
        {
           money += 1;
    //       Debug.Log("HOW MUCH MONEY YOU GOT?? " + money);
      //     Debug.LogError("The other object is: " + other.gameObject);
           Destroy(other.gameObject);

            //     coin.transform.position = new Vector3(0, 1, 0);

           coin = Instantiate(coin, new Vector3(Random.Range(-12, 12), 1, Random.Range(-5, 5)), transform.rotation);
            coin.tag = "Coin";
            //    randNrX = Random.Range(-12, 12);
            //  randNrZ = Random.Range(-10, 7);
        //    Debug.LogAssertion("HOW MUCH MONEY YOU GOT?? " + money);

        }
       
        if(other.name == "Health Pack")
        {
            Destroy(agent.gameObject);
        }
    }
   void OnTriggerExit()
    {


    }
    public void ChangeState(State<Veorica> newState)
    {
        fsm.pState = newState;
    }
}
