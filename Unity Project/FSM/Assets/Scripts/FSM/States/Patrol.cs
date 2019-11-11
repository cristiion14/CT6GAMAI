using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : State<Iohannis> {

    public override void Execute(Iohannis cb)
    {
        Debug.Log("Looking for player");
    }
}
