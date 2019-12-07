using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase :State<Iohannis> 
{
    public override void Execute(Iohannis agent)
    {
      //  Debug.Log("Chasing");

        agent.SetDestination(agent.transform, new Vector3(agent.target.position.x, 0, agent.target.position.z + 3));
        if (!agent.targetFound())
        {
            agent.nr = agent.hasBeenFoundAtNr;
            agent.ChangeState(new Patrol());
        }
        agent.FaceTarget();
        agent.Shoot();
      
    }
}
