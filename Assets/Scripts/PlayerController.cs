using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Transform player;
    public float speed;
    public float runspeed;
    private float translation;
    private float straffe;
    private bool paused;
    public bool hidden;
    public bool disabled;

    // Use this for initialization
    void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        paused = false;
        disabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!disabled)
        {
            if (Input.GetButton("Fire3"))
            {
                translation = Input.GetAxis("Vertical") * runspeed;
                straffe = Input.GetAxis("Horizontal") * runspeed;
            }
            else
            {
                translation = Input.GetAxis("Vertical") * speed;
                straffe = Input.GetAxis("Horizontal") * speed;
            }
            translation *= Time.deltaTime;
            straffe *= Time.deltaTime;
            player.transform.Translate(straffe, 0, translation);

            if (Input.GetKeyDown("escape") && !paused)
            {
                Cursor.lockState = CursorLockMode.None;
                paused = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                paused = false;
            }
        }
	}

    public void RIP(){
        Cursor.lockState = CursorLockMode.None;
        speed = 0;
        runspeed = 0;
    }
}
