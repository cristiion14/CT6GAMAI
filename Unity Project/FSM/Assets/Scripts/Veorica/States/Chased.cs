using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chased : State<Veorica>
{
    public override void Execute(Veorica agent)
    {
        Debug.Log("Veorica is being chased");
        agent.travelSpeed += 0.0005f;

    }
}
