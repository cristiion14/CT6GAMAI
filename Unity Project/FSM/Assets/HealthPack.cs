using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    GameObject veorica, iohannis;
    void Awake()
    {
        veorica = GameObject.Find("Veorica");
        iohannis = GameObject.Find(TagManager.Iohannis);
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
        if(other.name == iohannis.GetComponent<Iohannis>().name && iohannis.GetComponent<Iohannis>().health<100)
        {
            iohannis.GetComponent<Iohannis>().health += 20;
            iohannis.GetComponent<Iohannis>().tookHealth = true;
            Destroy(iohannis.GetComponent<Iohannis>().healthPack);
            iohannis.GetComponent<Iohannis>().spawnedHealth = false;
        }
    }
}
