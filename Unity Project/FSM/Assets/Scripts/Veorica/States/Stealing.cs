using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stealing : State<Veorica>
{
    
    public override void Execute(Veorica agent)
    {
        
        agent.travelSpeed = 0.07f;
        int nr = 0;
        Debug.Log("Stealing money :) ");
        //  PathRequestManagerV.RequestPath(agent.transform.position, agent.coins[nr].transform.position, agent.OnPathFound);
        agent.TracePath();
       // PathRequestManager.RequestPath(agent.transform.position, agent.coins[nr].transform.position, agent.OnPathFound);
        float distance = Vector3.Distance(agent.transform.position, agent.coins[nr].transform.position);
        if(distance<=2)
        {
          //  nr++;
            Debug.Log("nr is: " + nr);
            if (nr > agent.coins.Length)
                Debug.Log("Out of index");
             //   agent.transform.position = Vector3.zero;
        }
        if (agent.iohannis.GetComponent<Iohannis>().targetFound())
            agent.ChangeState(new Chased());
    }
}
