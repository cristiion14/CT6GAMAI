using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PlayerInfo : MonoBehaviour {
    public Camera cam;
    public NavMeshAgent agent;

    public float health = 100f;
    // Use this for initialization
    void Start () {
     //   agent = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {
        PointLocation();	
	}
    void PointLocation()
    {
        //check to see if the left mouse was pressed
        if (Input.GetMouseButtonDown(0))
        {
            //get pos of the mouse
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);      //convert the position of the mouse into a ray that can be shooted anywhere in the scene and store it in a ray var
            // var to store info about what the ray hits
            RaycastHit hit;
            //shoot ray and move agent just if the ray hits something
            if (Physics.Raycast(ray, out hit))
            {
                //move agent
                agent.SetDestination(hit.point);
            }
        }
    }
}
