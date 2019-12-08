using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : State<Veorica>
{
    public override void Execute(Veorica agent)
    {
        agent.travelSpeed = 0;

    }
}
