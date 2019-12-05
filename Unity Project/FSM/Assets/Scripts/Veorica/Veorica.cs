﻿using System.Collections;
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
    public GameObject coin;
    bool validPos = false;
    
    public int nr = 0; //for chased state

    StateManager<Veorica> fsm = new StateManager<Veorica>();

    public float distance;
    public NavMeshAgent agent;
    public bool hasTouched, isFound;
    public float lookRadius = 25f;
    Transform target;


    SimplifiedPathFinder simplifyPath;
    SimplifiedGrid sGrid;
    public List<SimplifiedNode> vFinalPath;//The completed path that the red line will be drawn along

    //for shooting
    public GameObject bullet;
    public GameObject bulletPoint;
    public GameObject iohannis;
    GameObject GM;
    public float randNrX, randNrY, randNrZ;

    public int targetIndex;

    public float health = 100f;

    float timeBtwShoots;
    public float startTimeBtwShoots;
    void Awake()
    {
        sGrid = GetComponent<SimplifiedGrid>();
        iohannis = GameObject.Find(TagManager.Iohannis);
        GM = GameObject.Find("GM");
        rb = GetComponentInChildren<Rigidbody>();
        Instantiate(coin, new Vector3(-11.8f, 0.4f, 1.77f), transform.rotation);

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
    void Update ()
    {
        fsm.Execute();
        GetDistanceFromCoins();
        CheckPosAndInstantiate();
    }

   public void CheckPosAndInstantiate()
    {
        if (hasTouched)
        {
            
        }
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

  
    public float GetDistanceFromCoins()
    {
         distance = Vector3.Distance(transform.position, coin.transform.position);
        return distance;
    }

   private void OnTriggerEnter(Collider other)
    {
        if(other.tag =="Coin")
        {
           money += 1;
    //       Debug.Log("HOW MUCH MONEY YOU GOT?? " + money);
      //     Debug.LogError("The other object is: " + other.gameObject);
           Destroy(other.gameObject);
            hasTouched = true;
            //     coin.transform.position = new Vector3(0, 1, 0);

            Vector3 position = Vector3.zero;

            if (!validPos)
            {
                //pick a random pos
                position = new Vector3(Random.Range(-14, 14), 1f, Random.Range(-14f, 14f));

                //this pos is valid until proven invalid
                validPos = true;

                //Collect all colliders within our Obstacle Check Radius
                Collider[] colliders = Physics.OverlapSphere(coin.transform.position, 3f);

                // go through each col
                foreach (Collider col in colliders)
                {
                    if (col.tag == "Wall")
                    {
                        validPos = false;       //if it's has a wall tag, then is not a valid pos
                    }
                }

            }
            if (validPos)
            {

                coin = Instantiate(coin, position, transform.rotation);
                coin.tag = "Coin";
            }
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
