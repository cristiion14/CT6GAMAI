using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed= 10f;
    public float fireRate = 2f;
    public float damage = 10f;
    public float range = 100f;
    public Rigidbody rb;


    private Vector3 target;
    Transform player;
    Collider bulletCol, playerCol;
    
	// Use this for initialization
	void Start () {

             player = GameObject.FindGameObjectWithTag("Player").transform;
        target = player.transform.position - gameObject.transform.position;
         bulletCol = GameObject.Find("Bullet").GetComponent<Collider>();
        playerCol = GameObject.Find("Player").GetComponent<Collider>();
        rb.velocity = target*speed;
	}
	
	// Update is called once per frame
	void Update () {
       // transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        
	}

    void FixedUpdate()
    {
        OnTriggerEnter(gameObject.GetComponent<Collider>());
    }

    void OnTriggerEnter(Collider col)
    {
        Player playerInf = col.GetComponent<Player>();
       
        if (bulletCol == playerCol)
        {
            playerInf.health -= damage;
            Debug.Log(playerInf.health);
            if (playerInf.health <= 0)
                playerInf.health = 0;
        }
       
            
        }
      //  Destroy(gameObject);
        //lower health

        //destroy bullet
       // Destroy(gameObject);

        //add explosive, etc

    }

