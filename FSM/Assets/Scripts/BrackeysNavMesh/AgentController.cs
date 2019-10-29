﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AgentController : MonoBehaviour {
    

    float distance;
    public Camera cam;
    public NavMeshAgent agent;
    bool foundPlayer;
    public float lookRadius = 25f;
    Transform target;
   public Transform downLeft, downRight, upLeft, upRight;



    //for shooting
    public GameObject bullet;
    public GameObject bulletPoint;
 
    public float health = 100f;
 
    float timeBtwShoots;
    public float startTimeBtwShoots;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = AgentManager.instance.player.transform;
        agent.stoppingDistance = 20f;
       // agent.SetDestination(downLeft.transform.position);

        timeBtwShoots = startTimeBtwShoots;

    }
	// Update is called once per frame
	void Update () {
        //PointLocation();
        ChasePlayer();
        Patrol();
        // Debug.Log(foundPlayer);
        // Debug.Log(nr);

       // Debug.Log(distance);

	}

    //display the look radius
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,lookRadius);
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
    void ChasePlayer()
    {
        //get the distance from agent to target
         distance = Vector3.Distance(target.position, transform.position);

        if(distance<=lookRadius)
        {
            foundPlayer = true;
            //chase
            agent.SetDestination(target.position);
            Shoot();

            if (distance<=agent.stoppingDistance)
            {
               // Debug.Log("Shoot!");
                //Attack the target
               
                //Face the target
                FaceTarget();
            }
        }
        foundPlayer = false;
    }
    void FaceTarget()
    {
       // Debug.Log("Face Target");
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 5);
    }
    void Patrol()
    {
        if (!(distance <= lookRadius))
        {
            
            if (agent.transform.position.x == downLeft.transform.position.x)
            {
              //  Debug.Log("Urmatoarea tinta");
                agent.SetDestination(upRight.transform.position);
            }
            if (agent.transform.position.x == upRight.transform.position.x)
            {
              //  Debug.Log("Urmatoarul");
                agent.SetDestination(upLeft.transform.position);
            }
            if (agent.transform.position.x == upLeft.transform.position.x)
            {
                //Debug.Log("Urmatoarea");
                agent.SetDestination(downRight.transform.position);
            }
        }
    }
    void Shoot()
    {
        /*
        Debug.Log("Shooting");
        RaycastHit hit;
        if (Physics.Raycast(gun.transform.position, gun.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
        }
        */

        if(timeBtwShoots<=0)
        {
            Instantiate(bullet, bulletPoint.transform.position, Quaternion.identity);
            timeBtwShoots = startTimeBtwShoots;

            //check collision
            if(bullet.GetComponent<Collider>() == gameObject.GetComponent<Collider>())
            {
                Debug.Log("L-a impuscat");
            }
           
        }
        else
        {
            timeBtwShoots -= Time.deltaTime;
        }


    }
    
}
