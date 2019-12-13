using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : State<Iohannis>
{
    public override void Execute(Iohannis agent)
    {

        //    Debug.Log("Patroling");
        if (agent.healthDesire >= 0.4f && agent.GM.GetComponent<GameManager>().spawnedHealth)
        {
            agent.GM.GetComponent<GameManager>().spawnedHealth = false;
            agent.ChangeState(new iGetHealth());
        }
        float distance = Vector3.Distance(agent.transform.position, agent.patrolPoints[agent.nr].position);
        //  randomagent.nr = Random.Range(0, 3);
        agent.FacePatrolPoint(agent.nr);
        //first path
        if (agent.nr == 0)
        {

            agent.SetDestination(agent.transform, agent.patrolPoints[agent.nr].position);
            distance = Vector3.Distance(agent.transform.position, agent.patrolPoints[agent.nr].position);
         //   Debug.Log("The distance is: " + distance);
        }
        if(distance<=2)
        {
            agent.nr++;
    //        Debug.Log("Should increase nr");
        }

        //second path
        if (agent.nr == 1)
        {
            agent.SetDestination(agent.transform, agent.patrolPoints[agent.nr].position);
            distance = Vector3.Distance(agent.transform.position, agent.patrolPoints[agent.nr].position);
            if (distance <= 2)
            {
                agent.nr++;
            }
        }

        //third path
        if (agent.nr == 2)
        {
            agent.SetDestination(agent.transform, agent.patrolPoints[agent.nr].position);
            distance = Vector3.Distance(agent.transform.position, agent.patrolPoints[agent.nr].position);
            if (distance <= 2)
            {
                agent.nr++;
            }
        }

       

        if (agent.nr == 3)
        {
            agent.SetDestination(agent.transform, agent.patrolPoints[agent.nr].position);
            distance = Vector3.Distance(agent.transform.position, agent.patrolPoints[agent.nr].position);
            if (distance <= 2)
            {
                agent.nr++;
            }
        }
  //      Debug.Log("nr = "+agent.nr);
        //reset
        if (agent.nr>3)
        {
            agent.nr = 0;
        }
        
        if(agent.targetFound())
        {
            agent.hasBeenFoundAtNr = agent.nr;
            agent.ChangeState(new Chase());
        }

        if (agent.hasDied)
            agent.ChangeState(new iDeath());
    }
}
