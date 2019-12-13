using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
public class StateManager<T>  {
    public State<T> pState;    //reference to the state
    T pAgent;                  //reference to the agent
   
    public void InIt(State<T> state, T agent)
    {
        //start timer
        System.Timers.Timer aTimer = new System.Timers.Timer();
        aTimer.Elapsed += (sender, e) => OnTimedEvent(sender, e, this);
        aTimer.Interval = 500;
        aTimer.Enabled = true;
        aTimer.Start();
        pAgent = agent;
        pState = state;
    }
    //execute has to run every .5s
    public void Execute()
    {
        pState.Execute(pAgent);
    }

    public void ChangeState(State<T> newState)
    {
        pState = newState;
    }
    private static void OnTimedEvent(object source, ElapsedEventArgs e, StateManager<T> sm)
    {

        sm.Execute();
        
    }
    

}
