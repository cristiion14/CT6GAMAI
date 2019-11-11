using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iohannis : MonoBehaviour {
   
    public int m_gold;
    public int m_bankedGold;
    public bool isMining;
    public bool isBanking;
    float speed = 1.5f;
    StateManager<Iohannis> fsm = new StateManager<Iohannis>();
    //create a reference to state
    //assign the first state ---- execute it-----change the state if nec

	// Use this for initialization
	void Start () {
        //  pState = new MiningForGoldOLD();
        fsm.InIt(new MiningForGold(), this);
	}
	
	// Update is called once per frame
    void Update()
    {
        if(isMining)
        {
            transform.position += new Vector3(-2, 0, 0) * speed * Time.fixedDeltaTime;
        }
        else if(isBanking)
        {
            transform.position += new Vector3(6, 0, 0) * speed * Time.fixedDeltaTime;
        }
        if(transform.position.x >10 || transform.position.x <10)
        {
           // transform.position = new Vector3(0, 0, 0);
        }
    }
   public void ChangeState(State<Iohannis> newState)
    {
        fsm.pState = newState;
    }
}
