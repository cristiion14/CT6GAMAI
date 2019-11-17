using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Iohannis : MonoBehaviour {
    public Camera cam;
    public NavMeshAgent agent;

    public bool isInChasingState;
    StateManager<Iohannis> fsm = new StateManager<Iohannis>();

    public float health = 100f;
    float speed = 2.5f;

    Vector3[] path;
    int targetIndex;
    // Use this for initialization
    Patrol patrolState;

    float distance;
   public Transform target;
    public Transform[] patrolPoints;
    public float lookRad = 25f;
    public float shootDist = 20f;
   public float startTimeBtwShoots, timeBtwShoots;
    public GameObject bullet, bulletPoint;
    Grid grid;
   public GameObject gm;
  public  bool isFound, foundTarget;
    public int nr = 0;
    private void Awake()
    {
        grid = GetComponent<Grid>();
        gm = GameObject.Find("GM");
//        patrolState = GetComponent<Patrol>();
        
    }
    void Start () {

        //adding the start state:
        fsm.InIt(new Patrol(), this);

        agent = GetComponent<NavMeshAgent>();
        target = AgentManager.instance.enemy2.transform;
        timeBtwShoots = startTimeBtwShoots;
       // PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
        for (int i = 0; i < 3; i++)
        {
            patrolPoints[i] = gm.GetComponent<AgentManager>().patrolPoints[i].transform;
        }
        //grid = GetComponent<Grid>();
    }
	public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if(pathSuccessful)
        {
            path = newPath;
            targetIndex = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }
   
    IEnumerator FollowPath()
    {
        targetIndex = 0;
        Vector3 currentWaypoint = path[0];
        while(true)
        {
            if (transform.position == currentWaypoint)
                targetIndex++;
                if(targetIndex>=path.Length)
                {
     //               targetIndex=0;
     //              path = new Vector3[0];
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, 0.1f);
            yield return null;
        }
       
    }
	// Update is called once per frame
	void Update () {
        PointLocation();
          fsm.Execute();
        //    ChasePlayer();
        // isChased();
      
        targetFound();
        FindTarget();
      //  PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
        //  FaceTarget();
        //  Shoot();

    }
    public bool targetFound()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRad)
            return true;
        else
            return false;
    }
    public void FindTarget()
    {
     //   gm.GetComponentInChildren<Pathfinding>().StartFindPath(gameObject.transform.position, target.transform.position);

    }
   public void FaceTarget()
    {
        // Debug.Log("Face Target");
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 5);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, lookRad);
    }
   public void ChasePlayer()
    {
     //  get the distance from player to enemy
       distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRad)
        {//if distance is less-----set destination
            foundTarget = true;


            agent.SetDestination(target.position);
            agent.stoppingDistance = 75f;
            FaceTarget();
            Shoot();
        }
        //foundTarget = false;
    }

    void isChased()
    {
        if(isFound)
        {
            agent.speed += 5f;
            agent.SetPath(new NavMeshPath());
        }
       
    }
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
    void PointLocation()
    {
        //check to see if the left mouse was pressed
        if (Input.GetMouseButtonDown(0))
        {
            //get pos of the mouse
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);      //convert the position of the mouse into a ray that can be shooted anywhere in the scene and store it in a ray var
            // var to store info about what the ray hits
            RaycastHit hit;
            //shoot ray and move agent just if the ray hits something
            if (Physics.Raycast(ray, out hit))
            {
                //move agent
                agent.SetDestination(hit.point);
            }
        }
    }

    public void ChangeState(State<Iohannis> newState)
    {
        fsm.pState = newState;
    }
}
