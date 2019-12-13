using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempState2 : State<Miner1>
{
    public override void Execute(Miner1 agent)
    {
        agent.rb.velocity = new Vector3(0, 1, 0);
        if (Input.GetKeyDown(KeyCode.C))
            agent.ChangeState(new TempState());
            
    }
}
