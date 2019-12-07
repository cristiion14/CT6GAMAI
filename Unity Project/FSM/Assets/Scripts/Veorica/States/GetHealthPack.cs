using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHealthPack : State<Veorica>
{
    public override void Execute(Veorica agent)
    {
        if (agent.healthPackPrefab.activeSelf && !agent.pickedHealth)
        {
            agent.SetDestination(agent.transform, agent.healthPack.transform.position);
            
        }
        if(agent.pickedHealth && !agent.iohannis.GetComponent<Iohannis>().targetFound())
        {
            Debug.Log("should change state");
            agent.ChangeState(new Stealing());
            agent.pickedHealth = false;
        }
        if(agent.pickedHealth && agent.iohannis.GetComponent<Iohannis>().targetFound())
        {
            agent.ChangeState(new Chased());
            agent.pickedHealth = false;
        }
    }
}