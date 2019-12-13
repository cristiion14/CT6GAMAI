using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    GameObject veorica, iohannis, GM;

    void Awake()
    {
        veorica = GameObject.Find("Veorica");
        iohannis = GameObject.Find(TagManager.Iohannis);
        GM = GameObject.Find("GM");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == veorica.GetComponent<Veorica>().name && veorica.GetComponent<Veorica>().health < 100)
        {

            veorica.GetComponent<Veorica>().health += 20;
            GM.GetComponent<GameManager>().pickedHealth = true;
            Destroy(GM.GetComponent<GameManager>().healthPack);
            GM.GetComponent<GameManager>().spawnedHealth = false;

        }
        else if (other.name == iohannis.GetComponent<Iohannis>().name && iohannis.GetComponent<Iohannis>().health < 100)
        {
            iohannis.GetComponent<Iohannis>().health += 20;
            GM.GetComponent<GameManager>().pickedHealth = true;
            Destroy(GM.GetComponent<GameManager>().healthPack);
            GM.GetComponent<GameManager>().spawnedHealth = false;
        }
    }
}
