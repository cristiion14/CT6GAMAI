using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text clockText;
    GameObject Veorica, Iohannis;
    public GameObject explosion;
    public GameObject deathEffect;
    // Start is called before the first frame update
    void Start()
    {
        Veorica = GameObject.Find(TagManager.Veorica);
        Iohannis = GameObject.Find(TagManager.Iohannis);
    }

    // Update is called once per frame
    void Update()
    {
        clockText.text = Time.time.ToString();
        if (Veorica.GetComponent<Veorica>().health == 0 || Iohannis.GetComponent<Iohannis>().health == 0)
            Application.Quit();

        if (Time.time > 120)
            Application.Quit();

        if (Veorica.GetComponent<Veorica>().money == 10)
            Application.Quit();
    }
}
