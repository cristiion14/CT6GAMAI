using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankingGold : State<Iohannis> {
    StateManager<Iohannis> fsm;
    public override void Execute(Iohannis cb)
    {
        Debug.Log("Banking gold\n");
        cb.isMining = false;
        cb.isBanking = true;
        cb.m_bankedGold += cb.m_gold;
        cb.m_gold = 0;
        cb.ChangeState(new MiningForGold());
    }
}
