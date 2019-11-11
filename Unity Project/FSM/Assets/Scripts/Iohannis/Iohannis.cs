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
    // Use this for initialization

    float distance;
    Transform target;
    public float lookRad = 25f;
    public float shootDist = 20f;
   public float startTimeBtwShoots, timeBtwShoots;
    public GameObject bullet, bulletPoint;

  public  bool isFound, foundTarget;
    Veorica enemy;
    void Start () {

        //adding the start state:
        fsm.InIt(new Chase(), this);


        agent = GetComponent<NavMeshAgent>();
        target = AgentManager.instance.enemy2.transform;
        timeBtwShoots = startTimeBtwShoots;
        enemy = new Veorica();

        isFound = enemy.foundPlayer;

        
    }
	
	// Update is called once per frame
	void Update () {
        PointLocation();
        fsm.Execute();
        //   ChasePlayer();
        // isChased();
        // FaceTarget();
        // Shoot();

        
	}
    void FaceTarget()
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
            //FaceTarget();
           // Shoot();
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
