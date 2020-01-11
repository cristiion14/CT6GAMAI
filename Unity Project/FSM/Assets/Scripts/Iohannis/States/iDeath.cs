using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iDeath : State<Iohannis>
{
    public override void Execute(Iohannis agent)
    {
        agent.travelSpeed = 0;
    }
}
