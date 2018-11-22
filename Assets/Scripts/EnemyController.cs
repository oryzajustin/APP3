using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public Animator anim;
    public float speed;
    public GameObject player;
    public PlayerController playerControls;
    public GameObject[] waypoints;
    public DetectionSystem detectionSystem;
    public FlashlightController flashlight;
    public float farlookdistance;
    public float shortlookdistance;
    public float chaseDistance;
    public MouseController mouseControls;
    public GameOverHandler gameOver;
    private float lightChaseDistance;
    private float oldLightChaseDistance;
    private float waypointTimer;
    private int randomIndex;
    private bool lightdetected;
    private bool sightdetected;

    private int playerMask;
    private RaycastHit shortlookHit;
    private RaycastHit farlookHit;
    private UnityEngine.AI.NavMeshAgent nav;
    private float distanceToPlayer;

    // Use this for initialization
    void Start () 
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        playerMask = LayerMask.GetMask("Detectable");
        waypointTimer = Random.Range(10, 20);
        randomIndex = Random.Range(0, 10);
        nav.speed = 2;
        lightChaseDistance = 7;
        lightdetected = false;
        sightdetected = false;
        //print(randomIndex);
        //print(waypointTimer);
    }
	
	// Update is called once per frame
	void Update () 
    {
        //cast 2 rays, 1 for flashlight off, 1 for flashlight on
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        //print(distanceToPlayer);
        HandleTimer();

        if (sightdetected)
        {
            SightChase();
        }
        else if(detectionSystem.lightdetected){
            lightdetected = true;
            LightChase();
        }
        else{
            Traverse();
        }
    }

    void HandleTimer(){
        if (waypointTimer <= 0)
        {
            waypointTimer = Random.Range(10, 20);
        }
        else
        {
            waypointTimer -= Time.deltaTime;
        }
    }

    void Traverse()
    {
        if (flashlight.lighton)
        {
            if (Physics.Raycast(transform.position, transform.forward, out farlookHit, farlookdistance, playerMask))
            {
                if (farlookHit.collider.tag == "Player")
                {
                    sightdetected = true;
                    //Chase();
                }
            }
            if(waypointTimer > 0 && !lightdetected && !sightdetected)
            {
                //if (waypointTimer > 0)
                //{
                    //var navpoint = waypoints[randomIndex];
                //print(waypoints[randomIndex].transform.position);
                //nav.SetDestination(waypoints[randomIndex].transform.position);
                nav.destination = waypoints[randomIndex].transform.position;
                //print(Vector3.Distance(waypoints[randomIndex].transform.position, transform.position));
                if (Vector3.Distance(waypoints[randomIndex].transform.position, transform.position) <= 1.5)
                {
                    nav.speed = 0;
                    anim.SetFloat("speed", nav.speed);
                }
                else
                {
                    nav.speed = 2;
                    anim.SetFloat("speed", nav.speed);
                }
            }
            if(lightdetected){
                LightChase();
            }
            else if(sightdetected){
                SightChase();
            }
            if(waypointTimer <= 0)
            {
                randomIndex = Random.Range(0, 10);
            }
            //}
        }
        else
        {
            if (Physics.Raycast(transform.position, transform.forward, out shortlookHit, shortlookdistance, playerMask))
            {
                if (shortlookHit.collider.tag == "Player" && !playerControls.hidden)
                {
                    sightdetected = true;
                    //Chase();
                }
            }
            if(waypointTimer > 0 && !lightdetected && !sightdetected)
            {
                //if (waypointTimer > 0)
                //{
                //anim.SetFloat("speed", nav.speed);
                //var navpoint = waypoints[randomIndex];
                //nav.SetDestination(waypoints[randomIndex].transform.position);
                nav.destination = waypoints[randomIndex].transform.position;
                if (Vector3.Distance(waypoints[randomIndex].transform.position, transform.position) <= 1.5)
                {
                    nav.speed = 0;
                    anim.SetFloat("speed", nav.speed);
                }
                else
                {
                    nav.speed = 2;
                    anim.SetFloat("speed", nav.speed);
                }
            }
            if(waypointTimer <= 0)
            {
                randomIndex = Random.Range(0, 10);
            }
            //}
        }
        //nav.SetDestination(waypoints[0].transform.position);
        //if(nav.isStopped){
        //    anim.SetFloat("speed", 0);
        //}
    }

    void SightChase()
    {
        nav.speed = 4;
        if (distanceToPlayer <= chaseDistance && !playerControls.hidden)
        {
            sightdetected = true;
            nav.destination = player.transform.position;
            anim.SetFloat("speed", nav.speed);
            if (distanceToPlayer <= 1.7)
            {
                nav.speed = 0;
                anim.SetFloat("speed", nav.speed);
                playerControls.RIP();
                mouseControls.RIP();
                Attack();
                gameOver.GameOver();
            }
        }
        else
        {
            sightdetected = false;
            nav.destination = waypoints[randomIndex].transform.position;
            nav.speed = 2;
        }
    }

    void LightChase()
    {
        nav.speed = 4;
        if(distanceToPlayer <= lightChaseDistance && !playerControls.hidden)
        {
            lightdetected = true;
            nav.destination = player.transform.position;
            anim.SetFloat("speed", nav.speed);
            if (distanceToPlayer < chaseDistance){
                lightChaseDistance = 10;
            }
            if (distanceToPlayer <= 1.7)
            {
                nav.speed = 0;
                anim.SetFloat("speed", nav.speed);
                playerControls.RIP();
                mouseControls.RIP();
                Attack();
                gameOver.GameOver();
            }
        }
        else
        {
            lightdetected = false;
            nav.destination = waypoints[randomIndex].transform.position;
            lightChaseDistance = 13;
            nav.speed = 2;
        }
    }

    void Attack(){
        anim.SetBool("attack", true);
    }
}
