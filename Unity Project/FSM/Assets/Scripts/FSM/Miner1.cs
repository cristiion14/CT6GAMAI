using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Miner1:State<Iohannis>
{
    State<Iohannis> fsm;
    // Start is called before the first frame update
    void Start()
    {
        fsm = new Patrol();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeState()
    {

    }
}
