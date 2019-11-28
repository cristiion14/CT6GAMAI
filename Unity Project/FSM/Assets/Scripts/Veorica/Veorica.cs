using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Veorica : MonoBehaviour {


    public float travelSpeed = 0.00001f;
  public  Vector3[] direction;
    Rigidbody rb;
    public float money = 0;


    StateManager<Veorica> fsm = new StateManager<Veorica>();

    float distance;
    public NavMeshAgent agent;
    public bool foundPlayer, isFound;
    public float lookRadius = 25f;
    Transform target;

    Pathfinding pathFinding;
    SimplifiedPathFinder simplifyPath;
    SimplifiedGrid sGrid;
    public List<SimplifiedNode> vFinalPath;//The completed path that the red line will be drawn along

    //for shooting
    public GameObject bullet;
    public GameObject bulletPoint;
    public GameObject iohannis;
    public GameObject[] coins;
     GameObject GM;


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
        //coin = GameObject.Find(TagManager.Coin);
    }
    void Start()
    {
      //  fsm.InIt(new Stealing(), this);
        agent = GetComponent<NavMeshAgent>();
       
        timeBtwShoots = startTimeBtwShoots;
        //    direction = pathFinding.direction;

        target = AgentManager.instance.player.transform;
    }

    void SetDestination(Transform transform, Transform target)
    {
        GM.GetComponentInChildren<SimplifiedPathFinder>().FindPath(transform.position, target.position);
        List<Vector3> wayPoints = new List<Vector3>();
        for (int i = 0; i < vFinalPath.Count; i++)
        {
            wayPoints.Add(vFinalPath[i].vPosition);
        }
       // wayPoints.Reverse();
        direction = wayPoints.ToArray();
        
        Vector3 currentWaypoint = direction[0];
        
        //rb.MovePosition(currentWaypoint);
     //   transform.position += direction[0].normalized*3f*Time.deltaTime;
        
        
       

            if (transform.position == currentWaypoint)
                targetIndex++;
            //   Debug.Log("the target index is: " + targetIndex);
            if (targetIndex >= direction.Length)
            {
                targetIndex = 0;
                direction = new Vector3[0];
            //    break;
            }
            currentWaypoint = direction[targetIndex];
            //        Debug.Log("current waypoint is: " + currentWaypoint);
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, travelSpeed*Time.fixedDeltaTime);
             //  transform.position += currentWaypoint*Time.deltaTime* 1.5f;

        
        //  rb.velocity = currentWaypoint;
        
    }
	// Update is called once per frame
	void Update () {

        //   fsm.Execute();

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
        SetDestination(transform, iohannis.transform);

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
        
    void FaceTarget()
    {
       // Debug.Log("Face Target");
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 5);
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
     void Shoot()
    {
        if(timeBtwShoots<=0)
        {
            Instantiate(bullet, bulletPoint.transform.position, Quaternion.identity);
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
    void OnTriggerEnter(Collider other)
    {
        if(other.tag =="Coin")
        {
            money += 1;
            Debug.Log("HOW MUCH MONEY YOU GOT?? " + money);
            Destroy(other.gameObject);
        }
    }
    
    public void ChangeState(State<Veorica> newState)
    {
        fsm.pState = newState;
    }
}
