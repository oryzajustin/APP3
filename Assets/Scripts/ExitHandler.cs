using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitHandler : MonoBehaviour {
    public GameObject portal;
    public GameObject player;
    public GameOverHandler gameOver;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider col)
    {
        if(col == player.GetComponent<Collider>()){
            gameOver.GameSuccess();
        }
    }
}
