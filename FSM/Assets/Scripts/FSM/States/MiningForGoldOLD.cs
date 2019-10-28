using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningForGold : State<cube> {
    StateManager<cube> fsm;
    public override void Execute(cube cb)
    {
        Debug.Log("MiningForGold\n");
        cb.isMining = true;
        cb.isBanking = false;
        cb.m_gold++;

        if(cb.m_gold>3)
        {
            cb.ChangeState(new BankingGold());
        }
    }
}

