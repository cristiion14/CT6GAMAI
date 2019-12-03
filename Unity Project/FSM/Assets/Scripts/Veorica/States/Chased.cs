using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chased : State<Veorica>
{
    public override void Execute(Veorica agent)
    {
       
        Debug.Log("Veorica is being chased");
        agent.travelSpeed += 0.1f;
        //   agent.GenerateRandomNr();
        //     agent.SetDestination(agent.transform, agent.iohannis.transform.position+new Vector3(-10,0,5));


        float distance = Vector3.Distance(agent.transform.position, agent.topR);
        //  randomagent.nr = Random.Range(0, 3);
        //first path
        if (agent.nr == 0)
        {

            agent.SetDestination(agent.transform, agent.topR);
            //distance = Vector3.Distance(agent.transform.position, agent.patrolPoints[agent.nr].position);
            //   Debug.Log("The distance is: " + distance);
            if (distance <= 2)
            {
                agent.nr++;
                Debug.Log("Should increase nr");
            }
        }
       

        //second path
        if (agent.nr == 1)
        {
            // PathRequestManager.RequestPath(agent.transform.position, agent.patrolPoints[agent.nr].transform.position, agent.OnPathFound);
             distance = Vector3.Distance(agent.transform.position, agent.bottomR);
            agent.SetDestination(agent.transform, agent.bottomR);
            if (distance <= 2)
            {
                agent.nr = 0;
            }
        }

        /*
        agent.SetDestination(agent.transform, agent.topR);
        //if it's on the top right
        if(Vector3.Distance(agent.transform.position, agent.topR)<3)
        {
            Debug.LogError("GO TO NEXT POINT");
            agent.SetDestination(agent.transform, agent.bottomR);
        }
        
        Debug.Log("The distance from topR is: " + Vector3.Distance(agent.transform.position, agent.topR));
        if (Vector3.Distance(agent.transform.position, agent.topL) <3)
        {
            agent.SetDestination(agent.transform, agent.bottomR);
        }
        Debug.Log("The distance from topL is: " + Vector3.Distance(agent.transform.position, agent.topL));

        if (Vector3.Distance(agent.transform.position, agent.bottomR) <3)
        {
            agent.SetDestination(agent.transform, agent.topL);
        }

        Debug.Log("The distance from bottomL is: " + Vector3.Distance(agent.transform.position, agent.bottomL));

        if (Vector3.Distance(agent.transform.position, agent.bottomL) <3)
        {
            agent.SetDestination(agent.transform, agent.topR);
        }

        Debug.Log("The distance from bottomR is: " + Vector3.Distance(agent.transform.position, agent.bottomR));
        */
        if (Vector3.Distance(agent.transform.position, agent.iohannis.transform.position)>10)
        {
            Debug.LogError("Changing state to stealing");
            agent.ChangeState(new Stealing());
        }
    }
}
