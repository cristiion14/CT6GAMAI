﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stealing : State<Veorica>
{
    
    public override void Execute(Veorica agent)
    {
        agent.travelSpeed = 10f;

     //   Debug.Log("Stealing money :) ");

        agent.FaceObj(agent.coin.transform.position);

        agent.SetDestination(agent.transform, agent.coin.transform.position);


        if (agent.healthDesire >= 0.4 && agent.GM.GetComponent<GameManager>().spawnedHealth)
        {
            agent.ChangeState(new GetHealthPack());
            agent.GM.GetComponent<GameManager>().spawnedHealth = false;
        }

        //   agent.followPath1 = true;
        if (agent.iohannis.GetComponent<Iohannis>().targetFound())
            agent.ChangeState(new Chased());

        if (agent.hasDied)
            agent.ChangeState(new Death());
    }
}
