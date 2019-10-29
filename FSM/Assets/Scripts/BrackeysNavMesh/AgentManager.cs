using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour {

    #region Singleton
    public static AgentManager instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject player;
    public PlayerInfo player1;
}
