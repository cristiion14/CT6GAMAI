using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase :State<Iohannis> 
{
    StateManager<Iohannis> fsm;
    public override void Execute(Iohannis agent)
    {
       // agent.isInChasingState = true;
        //Debug.Log("Chasing the player");
        agent.ChasePlayer();
        if(agent.foundTarget)
        {
            Debug.Log("should change state");
            agent.ChangeState(new Shooting());
        }
    }
}
