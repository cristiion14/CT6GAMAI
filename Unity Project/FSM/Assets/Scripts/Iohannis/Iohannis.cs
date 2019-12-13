using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class Iohannis : MonoBehaviour {
    
    public NavMeshAgent agent;

    State<Iohannis> fsm;

    [SerializeField]
   public float travelSpeed = 7f;
    public Image healthBar;
    public float health = 100f;
    public float healthDesire;
    public GameObject healthPackPrefab;
   public GameObject healthPack;
    public bool spawnedHealth = false, tookHealth = false, isCloseToHealth = false;
    int randNrY;
    GameObject[] healthPackHolders;
    float distanceFromHealth;
    public bool lookAtPlayer;

    public List<SimplifiedNode> iFinalPath;//The completed path that the red line will be drawn along
   public Vector3[] path;
    public int targetIndex;

    float distance;         //distance from 2 specific points
    public bool hasDied = false;

    public Transform target;
    public Transform[] patrolPoints;

    public float lookRad = 25f;
    public float startTimeBtwShoots, timeBtwShoots;
    public GameObject bullet, bulletPoint;

    Grid grid;
    public int nr = 0, hasBeenFoundAtNr=0;  //the path nr and the nr which keeps track at what path nr has been found
   public GameObject GM;

    private void Awake()
    {
        healthPackHolders = GameObject.FindGameObjectsWithTag("CoinPoint");
        grid = GetComponent<Grid>();
        GM = GameObject.Find("GM");
//        patrolState = GetComponent<Patrol>();
        
    }
    void Start () {

        //adding the start state:
    //    fsm.InIt(new Patrol(), this);
        fsm = new Patrol();
        agent = GetComponent<NavMeshAgent>();
        target = AgentManager.instance.enemy2.transform;
        timeBtwShoots = startTimeBtwShoots;
    }
	
   
	// Update is called once per frame
	void Update () {
        fsm.Execute(this);
        healthBar.fillAmount = health / 100;
        GetHealthDesireability();
        //  Shoot();
        // FaceTarget();
     //   Debug.LogError("The final path is: " + iFinalPath);
     //   SetDestination(transform, patrolPoints[0].transform.position);
    }

    public void FaceObj(Vector3 obj)
    {
        lookAtPlayer = false;
        Vector3 direction = (obj - transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 5f);
    }

    public void SetDestination(Transform transform, Vector3 target)
    {
        //targetIndex = 0;

        GM.GetComponentInChildren<SimplifiedPathFinder>().iFindPath(transform.position, target);
        List<Vector3> wayPoints = new List<Vector3>();
        for (int i = 0; i < iFinalPath.Count; i++)
        {
            wayPoints.Add(iFinalPath[i].vPosition);
        }
        // wayPoints.Reverse();
        path = wayPoints.ToArray();

        Vector3 currentWaypoint = path[0];

        if (transform.position == currentWaypoint)
            targetIndex++;

        if (targetIndex >= path.Length)
        {
            //      Debug.Log("Should reset targetIndex");
            targetIndex = 0;
            //       Debug.Log("Target index is: " + targetIndex);
            //   path = new Vector3[0];
            //    break;
        }

        currentWaypoint = path[targetIndex];
        //   Debug.Log("current waypoint is: " + currentWaypoint);
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, travelSpeed * Time.fixedDeltaTime);
        //  transform.position += currentWaypoint*Time.deltaTime* 1.5f;


        //  rb.velocity = currentWaypoint;

    }

    public void GetHealthDesireability()
    {
        float k = .1f;        //constant

        if (spawnedHealth)
        {
            Vector3 healthPackDir = healthPack.transform.position - transform.position;
            //distanceFromHealth = Vector3.Distance(transform.position, healthPack.transform.position);     //distance from healthPack
            distanceFromHealth = healthPackDir.sqrMagnitude;

            if (distanceFromHealth <= 25f)
                isCloseToHealth = true;
            else
                isCloseToHealth = false;

            if (!isCloseToHealth)
            {
                if (distanceFromHealth > 26.0f * 26.0f)
                    distanceFromHealth = 1f;
                else if (distanceFromHealth <= 100f && distanceFromHealth > 25f)
                    distanceFromHealth = 0.1f;
                else if (distanceFromHealth > 100f && distanceFromHealth < 17.5f * 17.5f)
                    distanceFromHealth = 0.25f;
                else if (distanceFromHealth >= 17.5f * 17.5f && distanceFromHealth < 25.5f * 25.5f)
                    distanceFromHealth = 0.5f;
                else
                    distanceFromHealth = 0.25f;
            }

            Debug.Log("Thhe distance from health is: " + distanceFromHealth);
            //   distanceFromHealth = Mathf.Clamp()
        }
        float healthStatus = health / 100;             // alive or not (1 or 0) 


        if (isCloseToHealth && health < 100)
            healthDesire = 1;
        else
            healthDesire = k * ((1 - healthStatus) / distanceFromHealth);
        Debug.Log("The health desire is: " + healthDesire);

        if (Input.GetKeyDown(KeyCode.G))
        {
            health -= 10;
        }
        //   healthDesire = Mathf.Clamp(healthDesire, 0, 1);

    }

    public bool targetFound()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRad)
            return true;
        else
            return false;
    }
   
   public void FaceTarget()
    {
        // Debug.Log("Face Target");
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 5);
        lookAtPlayer = true;
    }

    public void FacePatrolPoint(int patrolPoint)
    {  
            Vector3 direction = (patrolPoints[patrolPoint].transform.position - transform.position).normalized;
            Quaternion lookRot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 5);
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, lookRad);
    }

   public void Shoot()
    {
        if (timeBtwShoots <= 0)
        {
            Instantiate(bullet, bulletPoint.transform.position, Quaternion.identity);
            timeBtwShoots = startTimeBtwShoots;

        }
        else
        {
            timeBtwShoots -= Time.deltaTime;
        }
    }

    public void ChangeState(State<Iohannis> newState)
    {
        fsm = newState;
    }
}
