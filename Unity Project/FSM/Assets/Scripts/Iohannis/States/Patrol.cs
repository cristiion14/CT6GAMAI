using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : State<Iohannis>
{

    // Start is called before the first frame update
    List<Node> finalPath; //= { new List<Node>(), new List<Node>(), new List<Node>(), new List<Node>() };
   // int randomagent.nr = 0;
   
    Vector3 direction; //= { new Vector3(), new Vector3(), new Vector3(), new Vector3() };
    public override void Execute(Iohannis agent)
    {
        
        Debug.Log("Patroling");

        float distance = Vector3.Distance(agent.transform.position, agent.patrolPoints[agent.nr].position);
        //  randomagent.nr = Random.Range(0, 3);

        //first path
        if (agent.nr == 0)
        {
            
            PathRequestManager.RequestPath(agent.transform.position, agent.patrolPoints[agent.nr].position, agent.OnPathFound);
            distance = Vector3.Distance(agent.transform.position, agent.patrolPoints[agent.nr].position);
         //   Debug.Log("The distance is: " + distance);
        }
        if(distance<=2)
        {
            agent.nr++;
            Debug.Log("Should increase nr");
        }

        //second path
        if (agent.nr == 1)
        {
            PathRequestManager.RequestPath(agent.transform.position, agent.patrolPoints[agent.nr].transform.position, agent.OnPathFound);
            distance = Vector3.Distance(agent.transform.position, agent.patrolPoints[agent.nr].position);
            if (distance <= 2)
            {
                agent.nr++;
            }
        }

        //third path
        if (agent.nr == 2)
        {
            PathRequestManager.RequestPath(agent.transform.position, agent.patrolPoints[agent.nr].transform.position, agent.OnPathFound);
            distance = Vector3.Distance(agent.transform.position, agent.patrolPoints[agent.nr].position);
            if (distance <= 2)
            {
                agent.nr++;
            }
        }

       

        if (agent.nr == 3)
        {
            PathRequestManager.RequestPath(agent.transform.position, agent.patrolPoints[agent.nr].transform.position, agent.OnPathFound);
            distance = Vector3.Distance(agent.transform.position, agent.patrolPoints[agent.nr].position);
            if (distance <= 2)
            {
                agent.nr++;
            }
        }
        Debug.Log("nr = "+agent.nr);
        //reset
        if (agent.nr>3)
        {
            agent.nr = 0;
        }
        
        if(agent.targetFound())
        {
            agent.ChangeState(new Chase());
        }

        /*
          finalPath = agent.gm.GetComponent<AgentManager>().GetComponentInChildren<Pathfinding>().ReturnFinalPath(agent.gm.GetComponentInChildren<Grid>().NodeFromWorldPoint(agent.transform.position), agent.gm.GetComponentInChildren<Grid>().NodeFromWorldPoint(agent.patrolPoints[0].position));


            for (int j = 0; j <= finalPath.Count - 1; j++)
            // for (int j = finalPath[i].Count - 1; j-- > 0;)
            //        direction = agent.patrolPoints[i].transform.position - agent.transform.position;
            //    direction= gm.GetComponentInChildren<Pathfinding>().FindPath(agent.transform.position, agent.patrolPoints[i].position);
            {

                direction = finalPath[j].vPosition - agent.transform.position;
                direction.Normalize();
                agent.transform.position += direction * .3f * Time.deltaTime;
              //  agent.nr++;
                if (agent.targetFound())
                {
                    agent.ChangeState(new Chase());
                }
            }
            */
    }
}
