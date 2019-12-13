using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHealthPack : State<Veorica>
{
    public override void Execute(Veorica agent)
    {
        if (agent.GM.GetComponent<GameManager>().healthPackPrefab.activeSelf && !agent.GM.GetComponent<GameManager>().pickedHealth)
        {
            agent.SetDestination(agent.transform, agent.GM.GetComponent<GameManager>().healthPack.transform.position);
            agent.FaceObj(agent.GM.GetComponent<GameManager>().healthPack.transform.position);

        }
        if (agent.GM.GetComponent<GameManager>().pickedHealth && !agent.iohannis.GetComponent<Iohannis>().targetFound())
        {
            //    Debug.Log("should change state");
            agent.ChangeState(new Stealing());
            agent.GM.GetComponent<GameManager>().pickedHealth = false;
        }
        if (agent.GM.GetComponent<GameManager>().pickedHealth && agent.iohannis.GetComponent<Iohannis>().targetFound())
        {
            agent.ChangeState(new Chased());
            agent.GM.GetComponent<GameManager>().pickedHealth = false;
        }

        if (agent.hasDied)
            agent.ChangeState(new Death());
    }
}