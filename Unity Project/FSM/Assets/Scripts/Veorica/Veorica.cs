using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Veorica : MonoBehaviour {


    public float travelSpeed = 0.07f;
    Vector3[] direction;

    public float money = 0;


    StateManager<Veorica> fsm = new StateManager<Veorica>();

    float distance;
    public NavMeshAgent agent;
    public bool foundPlayer, isFound;
    public float lookRadius = 25f;
    Transform target;

    Pathfinding pathFinding;

    //for shooting
    public GameObject bullet;
    public GameObject bulletPoint;
    public GameObject iohannis;
    public GameObject[] coins;



    Vector3[] path;
    public int targetIndex;

    public float health = 100f;
    Grid grid;

    float timeBtwShoots;
    public float startTimeBtwShoots;
    void Awake()
    {
        grid = GetComponent<Grid>();
        iohannis = GameObject.Find(TagManager.Iohannis);
        pathFinding = GetComponent<Pathfinding>();
        //coin = GameObject.Find(TagManager.Coin);
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = AgentManager.instance.player.transform;
        timeBtwShoots = startTimeBtwShoots;
    //    direction = pathFinding.direction;
        fsm.InIt(new Stealing(), this);
        
    }
	// Update is called once per frame
	void Update () {

        fsm.Execute();

        //PointLocation();
      //  ChasePlayer();
      //  Patrol();
     //   isChased();
        // Debug.Log(foundPlayer);
        // Debug.Log(nr);
    //    isFound = iohanis.GetComponent<Iohannis>().foundTarget;
        // Debug.Log(distance);

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
        Vector3[] direction = pathFinding.GetComponent<Pathfinding>().FindPath1(transform.position, target.transform.position);
        /*
        for (int i = 0; i < direction.Length; i++)
        {
            agent.transform.position += direction[i] * agent.travelSpeed * Time.deltaTime;
        }
        */
        Vector3 currentWaypoint = direction[0];
        while (true)
        {

            if (transform.position == currentWaypoint)
                targetIndex++;
            //   Debug.Log("the target index is: " + targetIndex);
            if (targetIndex >= direction.Length)
            {
                //               targetIndex=0;
                //              path = new Vector3[0];
                break;
            }
            currentWaypoint = direction[targetIndex];
            //        Debug.Log("current waypoint is: " + currentWaypoint);
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, travelSpeed);
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

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            Debug.Log("Shpuld be called");
            path = newPath;
            targetIndex = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
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
