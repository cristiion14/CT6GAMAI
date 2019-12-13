using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iGetHealth : State<Iohannis>
{
    public override void Execute(Iohannis agent)
    {
        if (agent.healthPackPrefab.activeSelf && !agent.GM.GetComponent<GameManager>().pickedHealth)
        {
            agent.SetDestination(agent.transform, agent.GM.GetComponent<GameManager>().healthPack.transform.position);
            agent.FaceObj(agent.GM.GetComponent<GameManager>().healthPack.transform.position);

        }
        if (agent.GM.GetComponent<GameManager>().pickedHealth && !agent.targetFound())
        {
            //    Debug.Log("should change state");
            agent.ChangeState(new Patrol());
            agent.tookHealth = false;
            agent.GM.GetComponent<GameManager>().pickedHealth = false;
        }

        if (agent.GM.GetComponent<GameManager>().pickedHealth && agent.targetFound())
        {
            //    Debug.Log("should change state");
            agent.ChangeState(new Chase());
            agent.GM.GetComponent<GameManager>().pickedHealth = false;
        }
        //change state to chasing if veorica is withing radius 
        //check to see if he is actually going for the health pack...

        if (agent.hasDied)
            agent.ChangeState(new iDeath());
    }
}
