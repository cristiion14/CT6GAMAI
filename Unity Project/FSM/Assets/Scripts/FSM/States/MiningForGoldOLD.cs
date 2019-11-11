using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningForGold : State<Iohannis> {
    StateManager<Iohannis> fsm;
    public override void Execute(Iohannis agent)
    {
        Debug.Log("MiningForGold\n");
        agent.isMining = true;
        agent.isBanking = false;
        agent.m_gold++;

        if(agent.m_gold>3)
        {
            agent.ChangeState(new BankingGold());
        }
    }
}

