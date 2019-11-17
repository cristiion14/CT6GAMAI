﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : State<Iohannis>
{

    // Start is called before the first frame update
    List<Node> finalPath; //= { new List<Node>(), new List<Node>(), new List<Node>(), new List<Node>() };
    int nr = 0;
    Vector3 direction; //= { new Vector3(), new Vector3(), new Vector3(), new Vector3() };
    public override void Execute(Iohannis agent)
    {
       
        Debug.Log("Patroling");

        PathRequestManager.RequestPath(agent.transform.position, agent.patrolPoints[0].transform.position, agent.OnPathFound);
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
              //  nr++;
                if (agent.targetFound())
                {
                    agent.ChangeState(new Chase());
                }
            }
            */
    }
}
