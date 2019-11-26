using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Iohannis : MonoBehaviour {
    
    public NavMeshAgent agent;

    public bool isInChasingState;
    StateManager<Iohannis> fsm = new StateManager<Iohannis>();

    [SerializeField]
    float travelSpeed = 0.07f;

    public float health = 100f;

    Vector3[] path;
    public int targetIndex;

    float distance;

    public Transform target;
    public Transform[] patrolPoints;

    public float lookRad = 25f;
    public float shootDist = 20f;
    public float startTimeBtwShoots, timeBtwShoots;
    public GameObject bullet, bulletPoint;

    Grid grid;
    public int nr = 0, hasBeenFoundAtNr=0;  //the path nr and the nr which keeps track at what path nr has been found

    private void Awake()
    {
        grid = GetComponent<Grid>();
     
//        patrolState = GetComponent<Patrol>();
        
    }
    void Start () {

        //adding the start state:
        fsm.InIt(new Patrol(), this);

        agent = GetComponent<NavMeshAgent>();
        target = AgentManager.instance.enemy2.transform;
        timeBtwShoots = startTimeBtwShoots;
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
    //    targetIndex = 0;
        Vector3 currentWaypoint = path[0];
        while(true)
        {
     
            if (transform.position == currentWaypoint)
                targetIndex++;
         //   Debug.Log("the target index is: " + targetIndex);
            if (targetIndex>=path.Length)
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
	// Update is called once per frame
	void Update () {
          fsm.Execute();

    }
    public bool targetFound()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRad)
            return true;
        else
            return false;
    }
   
   public void FaceTarget()
    {
        // Debug.Log("Face Target");
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 5);
    }

    public void FacePatrolPoint(int patrolPoint)
    {  
            Vector3 direction = (patrolPoints[patrolPoint].transform.position - transform.position).normalized;
            Quaternion lookRot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 5);
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, lookRad);
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

    public void ChangeState(State<Iohannis> newState)
    {
        fsm.pState = newState;
    }
}
