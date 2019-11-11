using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 1f;
    public float fireRate = 2f;
    public float damage = 10f;
    public float range = 100f;
    public Rigidbody rb;


    private Vector3 target;
    Transform player;
  // public PlayerInfo playerInf;
    float pHealth;
    GameObject gb, gb2;
    //Collider bulletCol, playerCol;

    // Use this for initialization
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = player.transform.position - gameObject.transform.position;

        rb.velocity = target * speed;        
        gb = GameObject.Find("Player");
        gb2 = GameObject.Find("Enemy1");
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        StartCoroutine(DestroyBullet());
       
    }

 

    void OnCollisionEnter(Collision col)
    {


         if(col.collider.name=="Player")
        {
            //lower health
            //pHealth -= 1;
            gb.GetComponent<PlayerInfo>().health -= damage;
            Debug.Log(gb.GetComponent<PlayerInfo>().health);
            if (gb.GetComponent<PlayerInfo>().health <= 0)
            {
                Debug.Log("Mort");
            }
            Destroy(gameObject);
        }

       else if (col.collider.name == "Enemy1")
        {
            //lower health
            //pHealth -= 1;
            gb2.GetComponent<AgentController>().health -= damage;
            Debug.Log("Viata inamicului este: "+gb2.GetComponent<AgentController>().health);
            if (gb.GetComponent<AgentController>().health <= 0)
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

