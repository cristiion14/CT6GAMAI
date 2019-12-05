using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stealing : State<Veorica>
{
    
    public override void Execute(Veorica agent)
    {
        agent.travelSpeed = 10f;

        Debug.Log("Stealing money :) ");

        agent.FaceObj(agent.coin.transform.position);

        agent.SetDestination(agent.transform, agent.coin.transform.position);

        
        if (agent.health < 90&&agent.spawnedHealth)
        {
         //   agent.vFinalPath = new List<SimplifiedNode>();
            agent.SetDestination(agent.transform, agent.healthPack.transform.position);
            if (agent.pickedHealth)
            {
             //   agent.followPath2 = false;
                //agent.followPath1 = true;
            }
        }
     //   agent.followPath1 = true;
        if (agent.iohannis.GetComponent<Iohannis>().targetFound())
            agent.ChangeState(new Chased());
    }
}
