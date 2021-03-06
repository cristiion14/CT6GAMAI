﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chased : State<Veorica>
{
    public override void Execute(Veorica agent)
    {
      //  Debug.Log("Veorica is being chased");
       
        agent.Shoot();
        agent.travelSpeed += 0.05f;
        
       // float distance = Vector3.Distance(agent.transform.position, agent.topR);
        float distance = (agent.transform.position - agent.topR).sqrMagnitude;
        //first path
        if (agent.nr == 0)
        {

            agent.SetDestination(agent.transform, agent.topR);
            if (!agent.lookAtPlayer)
            {
                agent.FaceObj(agent.topR);
            }
            if (distance <= 4)
            {
                agent.nr++;
//                Debug.Log("Should increase nr");
            }
        }

        //second path
        if (agent.nr == 1)
        {
            distance = (agent.transform.position - agent.bottomR).sqrMagnitude;
            agent.SetDestination(agent.transform, agent.bottomR);
            if (!agent.lookAtPlayer)
            {
                agent.FaceObj(agent.topR);
            }
            if (distance <= 4)
            {
                agent.nr = 0;
            }
        }
        if (agent.healthDesire >= 0.4 && agent.GM.GetComponent<GameManager>().spawnedHealth)
        {
            agent.ChangeState(new GetHealthPack());
            agent.GM.GetComponent<GameManager>().spawnedHealth = false;

        }

        //   if (Vector3.Distance(agent.transform.position, agent.iohannis.transform.position)>10)
        if ((agent.transform.position - agent.iohannis.transform.position).sqrMagnitude > 100)
        {
            //      Debug.LogError("Changing state to stealing");
            agent.ChangeState(new Stealing());
        }

        if (agent.hasDied)
            agent.ChangeState(new Death());
    }
}
