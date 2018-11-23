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
    //public AudioSource audioSource;
    //public AudioClip yell;
    //public AudioClip hitSound;
    public AudioSource yell;
    public AudioSource hitSound;
    private float lightChaseDistance;
    private float oldLightChaseDistance;
    private float waypointTimer;
    private int randomIndex;
    public bool lightdetected;
    public bool sightdetected;
    public bool playedYell;

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
        playedYell = false;
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
        if (!gameOver.gameWon && !gameOver.gameIsOver)
        {
            if (sightdetected)
            {
                if(!playedYell){
                    //audioSource.PlayOneShot(yell);
                    yell.Play();
                    playedYell = true;
                }
                SightChase();
            }
            else if (detectionSystem.lightdetected)
            {
                if (!playedYell)
                {
                    //audioSource.PlayOneShot(yell);
                    yell.Play();
                    playedYell = true;
                }
                lightdetected = true;
                LightChase();
            }
            else
            {
                Traverse();
            }
        }
        else{
            anim.SetFloat("speed", 0);
            nav.speed = 0;
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
                    if (!playedYell)
                    {
                        //audioSource.PlayOneShot(yell);
                        yell.Play();
                        playedYell = true;
                    }
                    sightdetected = true;
                    //Chase();
                }
            }
            if(waypointTimer > 0 && !lightdetected && !sightdetected)
            {
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
            if(lightdetected){
                if (!playedYell)
                {
                    //audioSource.PlayOneShot(yell);
                    yell.Play();
                    playedYell = true;
                }
                LightChase();
            }
            else if(sightdetected){
                if (!playedYell)
                {
                    //audioSource.PlayOneShot(yell);
                    yell.Play();
                    playedYell = true;
                }
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
                    if (!playedYell)
                    {
                        //audioSource.PlayOneShot(yell);
                        yell.Play();
                        playedYell = true;
                    }
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
            if (distanceToPlayer <= 1.7 && (!gameOver.gameWon && !gameOver.gameIsOver))
            {
                nav.speed = 0;
                anim.SetFloat("speed", nav.speed);
                playerControls.RIP();
                mouseControls.RIP();
                Attack();
                if(!gameOver.gameWon && !gameOver.gameIsOver){
                    //audioSource.PlayOneShot(hitSound);
                    hitSound.Play();
                }
                gameOver.GameOver();
            }
        }
        else
        {
            sightdetected = false;
            //playedYell = false;
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
            if (distanceToPlayer <= 1.7 && (!gameOver.gameWon && !gameOver.gameIsOver))
            {
                nav.speed = 0;
                anim.SetFloat("speed", nav.speed);
                playerControls.RIP();
                mouseControls.RIP();
                Attack();
                if (!gameOver.gameWon && !gameOver.gameIsOver)
                {
                    //audioSource.PlayOneShot(hitSound);
                    hitSound.Play();
                }
                gameOver.GameOver();
            }
        }
        else
        {
            lightdetected = false;
            //playedYell = false;
            nav.destination = waypoints[randomIndex].transform.position;
            lightChaseDistance = 13;
            nav.speed = 2;
        }
    }

    void Attack(){
        anim.SetBool("attack", true);
    }
}
