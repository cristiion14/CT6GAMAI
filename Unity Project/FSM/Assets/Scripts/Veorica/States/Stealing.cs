﻿using System;
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
        if (agent.health < 90)
        {
       //     agent.SetDestination(agent.transform, agent.healthPack.transform.position);
        }
        if (agent.iohannis.GetComponent<Iohannis>().targetFound())
            agent.ChangeState(new Chased());
    }
}
