using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    GameObject veorica;
    void Awake()
    {
        veorica = GameObject.Find("Veorica");
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.name ==veorica.GetComponent<Veorica>().name&&veorica.GetComponent<Veorica>().health<100)
        {
            
            veorica.GetComponent<Veorica>().health += 20;
            veorica.GetComponent<Veorica>().pickedHealth = true;
            Destroy(veorica.GetComponent<Veorica>().healthPack);
            veorica.GetComponent<Veorica>().spawnedHealth = false;


        }
    }
}
