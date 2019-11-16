using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase :State<Iohannis> 
{
    StateManager<Iohannis> fsm;
    Transform target;
    public override void Execute(Iohannis agent)
    {
        // agent.isInChasingState = true;
        Debug.Log("Chasing");
        //  target = AgentManager.instance.enemy2.transform;
        agent.FindTarget();
        agent.FaceTarget();
        agent.Shoot();
      
    }
}
