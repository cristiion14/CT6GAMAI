using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 2f;
    public float damage = 10f;
    
    public Rigidbody rb;


    private Vector3 target;
    Transform player;
  // public PlayerInfo playerInf;
    float pHealth;
    GameObject gb;
    //Collider bulletCol, playerCol;

    // Use this for initialization
    void Start()
    {

        player = GameObject.FindGameObjectWithTag(TagManager.Iohannis).transform;
        target = player.transform.position - gameObject.transform.position;

        rb.velocity = target * speed;        
        gb = GameObject.Find(TagManager.Iohannis);
     
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
            Debug.Log("Iohanis has: " + gb.GetComponent<Iohannis>().health + " remaining health");
            if (gb.GetComponent<Iohannis>().health <= 0)
            {
                Debug.Log("Mort");
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

