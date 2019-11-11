using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : State<cube> {

    public override void Execute(cube cb)
    {
        Debug.Log("Looking for player");
    }
}
