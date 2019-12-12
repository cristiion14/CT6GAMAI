using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iGetHealth : State<Iohannis>
{
    public override void Execute(Iohannis agent)
    {
        if (agent.healthPackPrefab.activeSelf && !agent.tookHealth)
        {
            agent.SetDestination(agent.transform, agent.healthPack.transform.position);
            agent.FaceObj(agent.healthPack.transform.position);

        }
        if (agent.tookHealth)
        {
            //    Debug.Log("should change state");
            agent.ChangeState(new Patrol());
            agent.tookHealth = false;
        }
        //change state to chasing if veorica is withing radius 
        //check to see if he is actually going for the health pack...
        
        if (agent.hasDied)
            agent.ChangeState(new iDeath());
    }
}
