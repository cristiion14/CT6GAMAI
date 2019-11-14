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
        //Debug.Log("Chasing the player");

      //  target = AgentManager.instance.enemy2.transform;
        agent.ChasePlayer();
        if(agent.foundTarget)
        {
            agent.Shoot();
            //Debug.Log("should change state");
           // agent.ChangeState(new Shooting());
        }
    }
}
