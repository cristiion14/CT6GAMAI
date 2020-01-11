using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Bullet : MonoBehaviour
{

    public float speed = 1000f;
    public float damage = 10f;
    public Rigidbody rb;

    private Vector3 target;
    Transform player;
  // public PlayerInfo playerInf;
    float pHealth;
    GameObject gb, GM;


    //Collider bulletCol, playerCol;

    // Use this for initialization
    void Start()
    {

        player = GameObject.FindGameObjectWithTag(TagManager.Iohannis).transform;
        target = (player.transform.position - gameObject.transform.position).normalized;
        rb.velocity = target *Time.fixedDeltaTime* speed;        
        gb = GameObject.Find(TagManager.Iohannis);
        GM = GameObject.Find("GM");
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        StartCoroutine(DestroyBullet());
       
    }

 

    void OnCollisionEnter(Collision col)
    {


         if(col.collider.name=="Iohannis")
        {
            //lower health
            //pHealth -= 1;
            gb.GetComponent<Iohannis>().health -= damage;
            GameObject hitEffect = (GameObject)Instantiate(GM.GetComponent<GameManager>().explosion, col.collider.ClosestPoint(gb.transform.position), transform.rotation);
            Destroy(hitEffect, 2);

            if (gb.GetComponent<Iohannis>().health <= 0)
            {
                gb.GetComponent<Iohannis>().health = 0;
                gb.GetComponent<Iohannis>().hasDied = true;
                //spawn particle effect
                GameObject dieEffect = (GameObject)Instantiate(GM.GetComponent<GameManager>().deathEffect, col.collider.ClosestPoint(gb.transform.position), transform.rotation);
                Destroy(dieEffect, 4);
                Destroy(gb, 2);
            }
            Destroy(gameObject);
        }


        //  Destroy(gameObject);
        //lower health

        //destroy bullet
        // Destroy(gameObject);

        //add explosive, etc

    }
    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
  
}

