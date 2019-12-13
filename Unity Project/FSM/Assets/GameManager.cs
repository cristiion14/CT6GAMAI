using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text clockText;
    GameObject Veorica, Iohannis;
    GameObject[] coins;
    public GameObject explosion;
    public GameObject deathEffect, healthPackPrefab, healthPack;
    public bool spawnedHealth, pickedHealth = false;
    int randNrY;
    // Start is called before the first frame update
    void Start()
    {
        Veorica = GameObject.Find(TagManager.Veorica);
        Iohannis = GameObject.Find(TagManager.Iohannis);
        StartCoroutine(SpawnHealthPack());
        coins = GameObject.FindGameObjectsWithTag("CoinPoint");


    }

    // Update is called once per frame
    void Update()
    {
        clockText.text = Time.time.ToString();
        if (Veorica.GetComponent<Veorica>().health == 0 || Iohannis.GetComponent<Iohannis>().health == 0)
            Application.Quit();

        if (Time.time > 90)
            Application.Quit();

        if (Veorica.GetComponent<Veorica>().money == 20)
            Application.Quit();
    }

    private IEnumerator SpawnHealthPack()
    {
        while (true)
        {
            yield return new WaitForSeconds(4);
            if (!spawnedHealth)
            {
                randNrY = Random.Range(0, 23);
                healthPack = Instantiate(healthPackPrefab, coins[randNrY].transform.position, transform.rotation);
                spawnedHealth = true;
            }

            yield return new WaitForSeconds(3);
            if (spawnedHealth && !pickedHealth)
            {
                Destroy(healthPack);
                spawnedHealth = false;

            }
        }

    }
}
