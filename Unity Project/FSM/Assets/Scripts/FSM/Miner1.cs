using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Miner1 : MonoBehaviour
{
    State<Miner1> fsm;
   public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        fsm = new TempState();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        fsm.Execute(this);
    }

   public void ChangeState(State<Miner1> newState)
    {
        fsm = newState;
    }
}
