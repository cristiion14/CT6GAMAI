using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : State<Iohannis>
{
    // Start is called before the first frame update
    public override void Execute(Iohannis agent)
    {
        Debug.Log("Shooting");
        agent.Shoot();
        if(agent.foundTarget == false)
        {
            agent.ChangeState(new Chase());
        }
    }
}
