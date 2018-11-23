using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour {
    public Light flashlight;
    public bool lighton;
    public float radius;
    public float distance;
    public float raydistance;
    public DetectionSystem detectionSystem;
    public GameOverHandler gameOver;
    public AudioSource flashlightSound;
    
    //public GameObject skeleton;
    int shootableMask;
    RaycastHit shootHit;
    private void Start()
    {
        lighton = false;
        shootableMask = LayerMask.GetMask("Shootable");
    }
    void Update () {
        if (Input.GetButtonDown("Fire1") && (!gameOver.gameWon && !gameOver.gameIsOver))
        {
            if (!lighton)
            {
                flashlightSound.Play();
                flashlight.enabled = true;
                lighton = true;
            }
            else
            {
                flashlightSound.Play();
                flashlight.enabled = false;
                lighton = false;
            }
        }
        if(lighton){
            var forwardRay = new Ray(flashlight.transform.position, flashlight.transform.forward);
            if(Physics.Raycast(flashlight.transform.position, flashlight.transform.forward, out shootHit, raydistance, shootableMask)){
                Debug.DrawRay(forwardRay.origin, forwardRay.direction * raydistance, Color.red);

                if (shootHit.collider.tag == "Skeleton")
                {
                    detectionSystem.lightdetected = true;
                    //print("RUN");
                }
                //else
                //{
                //    detectionSystem.detected = false;
                //    print("not the enemy");
                //}
            }
        }
	}
}
