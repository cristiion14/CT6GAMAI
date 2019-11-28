using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stealing : State<Veorica>
{
    
    public override void Execute(Veorica agent)
    {

        //  agent.travelSpeed = 0.07f;
      //  int agent.coinNr=0;
        Debug.Log("Stealing money :) ");
        //  PathRequestManager.RequestPath(agent.transform.position, agent.coins[agent.coinNr].transform.position, agent.OnPathFound);
        // agent.TracePath();
        // PathRequestManager.RequestPath(agent.transform.position, agent.coins[agent.coinNr].transform.position, agent.OnPathFound);

        // agent.SetDestination(agent.transform, agent.coins[0].transform.position);
        /*
        float distance = Vector3.Distance(agent.transform.position, agent.coins[0].transform.position);
        Debug.Log("The distance is: "+distance);
        if (distance == 1.68855f)
            agent.coinNr++;
        Debug.Log(agent.coinNr);
        switch(agent.coinNr)
        {
            case 1: agent.SetDestination(agent.transform, agent.coins[agent.coinNr].transform.position);
                    distance = Vector3.Distance(agent.transform.position, agent.coins[agent.coinNr].transform.position);
               // if (distance <= 2)
                 //   agent.coinNr++;
                break;
            case 2: agent.SetDestination(agent.transform, agent.coins[agent.coinNr].transform.position);
                    distance = Vector3.Distance(agent.transform.position, agent.coins[agent.coinNr].transform.position);
               // if (distance <= 2)
                //    agent.coinNr++;
                break;
          
        }
        */
        
        float distance = Vector3.Distance(agent.transform.position, agent.coins[agent.coinNr].transform.position);
        Debug.LogError("The distance is: " + distance);
        //  randomagent.nr = Random.Range(0, 3);
    //    agent.FacePatrolPoint(agent.nr);
        //first path
        if (agent.coinNr == 0)
        {

           agent.SetDestination(agent.transform, agent.coins[agent.coinNr].transform.position);
            distance = Vector3.Distance(agent.transform.position, agent.coins[agent.coinNr].transform.position);
            //   Debug.Log("The distance is: " + distance);
            if (distance <= 2)
            {
                agent.coinNr++;
                Debug.Log("Should increase coinNr");
            }
        }
        
        Debug.Log(agent.coinNr);
        //second path
        if (agent.coinNr == 1)
        {
            Debug.Log("Should go to the next one");
            Debug.Log("The position is: " + agent.coins[agent.coinNr].transform.localPosition);
            agent.SetDestination(agent.transform, agent.coins[agent.coinNr].transform.position);
            distance = Vector3.Distance(agent.transform.position, agent.coins[agent.coinNr].transform.position);
            //   Debug.Log("The distance is: " + distance);
            if (distance <= 2)
            {
                agent.coinNr++;
                Debug.Log("Should increase coinNr");
            }
        }
       
        //third path
        if (agent.coinNr == 2)
        {

            agent.SetDestination(agent.transform, agent.coins[agent.coinNr].transform.position);
            distance = Vector3.Distance(agent.transform.position, agent.coins[agent.coinNr].transform.position);
            //   Debug.Log("The distance is: " + distance);
            if (distance <= 2)
            {
                agent.coinNr++;
                Debug.Log("Should increase coinNr");
            }
        }
      
        if (agent.coinNr > agent.coins.Length)
        {
            agent.coinNr = 0;
        }
        
        
        /*
        agent.SetDestination(agent.transform, agent.coins[agent.coinNr].transform.position);
        if (Input.GetKeyDown(KeyCode.G))
        {
            agent.coinNr++;
            Debug.Log("coin nr: " + agent.coinNr);
           
        }
        */
        if (agent.iohannis.GetComponent<Iohannis>().targetFound())
            agent.ChangeState(new Chased());
    }
}
