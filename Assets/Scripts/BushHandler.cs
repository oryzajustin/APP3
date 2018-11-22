using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BushHandler : MonoBehaviour {
    public PlayerController playerControls;
    public MouseController view;
    public BoxCollider entry;
    public Transform bushPerspective;
    public GameObject player;
    public Camera playerCamera;
    public MeshRenderer playerMesh;
    public GameObject flashlight;
    public Text hiddenText;
    //private Transform oldPlayerCameraPosition;
	// Use this for initialization
	void Start () {
        playerControls.hidden = false;
        //oldPlayerCameraPosition = playerCamera.transform;
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void OnTriggerEnter(Collider collision)
    {
        if(collision == player.GetComponent<Collider>()){
            if(!playerControls.hidden){
                playerControls.hidden = true;
                playerCamera.transform.position = bushPerspective.position;
                //view.frozen = true;
                hiddenText.gameObject.SetActive(true);
                playerMesh.enabled = false;
                flashlight.SetActive(false);
            }
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision == player.GetComponent<Collider>())
        {
            if (playerControls.hidden)
            {
                playerControls.hidden = false;
                playerCamera.transform.position = player.transform.position;
                hiddenText.gameObject.SetActive(false);
                //view.frozen = false;
                playerMesh.enabled = true;
                flashlight.SetActive(true);
            }
        }
    }
}
