using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase :State<Iohannis> 
{
    StateManager<Iohannis> fsm;
    //Transform target;
    public override void Execute(Iohannis agent)
    {
        // agent.isInChasingState = true;
        Debug.Log("Chasing");
        //  target = AgentManager.instance.enemy2.transform;

        PathRequestManager.RequestPath(agent.transform.position, new Vector3(agent.target.position.x, 0, agent.target.position.z+3) , agent.OnPathFound);
        
        if(!agent.targetFound())
        {
            agent.nr = agent.hasBeenFoundAtNr;
            agent.ChangeState(new Patrol());
        }
        agent.FaceTarget();
        agent.Shoot();
      
    }
}
