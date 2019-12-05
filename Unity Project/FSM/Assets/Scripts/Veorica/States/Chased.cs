﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chased : State<Veorica>
{
    public override void Execute(Veorica agent)
    {
        Debug.Log("Veorica is being chased");
       
        agent.Shoot();
        agent.travelSpeed += 0.01f;
        
        float distance = Vector3.Distance(agent.transform.position, agent.topR);
      
        //first path
        if (agent.nr == 0)
        {

            agent.SetDestination(agent.transform, agent.topR);
            if (!agent.lookAtPlayer)
            {
                agent.FaceObj(agent.topR);
            }
            if (distance <= 2)
            {
                agent.nr++;
//                Debug.Log("Should increase nr");
            }
        }

        //second path
        if (agent.nr == 1)
        {
             distance = Vector3.Distance(agent.transform.position, agent.bottomR);
            agent.SetDestination(agent.transform, agent.bottomR);
            if (!agent.lookAtPlayer)
            {
                agent.FaceObj(agent.topR);
            }
            if (distance <= 2)
            {
                agent.nr = 0;
            }
        }
        if(agent.health<90&&agent.spawnedHealth)
        {
            agent.SetDestination(agent.transform, agent.healthPack.transform.position);
        }
       
        if (Vector3.Distance(agent.transform.position, agent.iohannis.transform.position)>agent.lookRadius)
        {
            Debug.LogError("Changing state to stealing");
            agent.ChangeState(new Stealing());
        }
    }
}
