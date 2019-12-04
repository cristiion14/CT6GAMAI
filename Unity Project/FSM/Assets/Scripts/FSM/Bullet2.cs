using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2 : MonoBehaviour
{
    //Bullet class for Iohannis


    public float speed = 1000f;
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

        player = GameObject.FindGameObjectWithTag(TagManager.Veorica).transform;
        target = (player.transform.position - gameObject.transform.position).normalized;

        rb.velocity = target * Time.fixedDeltaTime * speed;
        gb = GameObject.Find(TagManager.Veorica);
        
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        StartCoroutine(DestroyBullet());

    }



    void OnCollisionEnter(Collision col)
    {


        if (col.collider.name == TagManager.Veorica)
        {
            //lower health
            //pHealth -= 1;
            gb.GetComponent<Veorica>().health -= damage;
           
            Debug.LogError("Veorica has: "+ gb.GetComponent<Veorica>().health + " remaining health");
            if (gb.GetComponent<Veorica>().health <= 0)
            {
                gb.GetComponent<Veorica>().health = 0;
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
