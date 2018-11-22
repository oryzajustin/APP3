using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreButton : MonoBehaviour {
    public GameObject scoreCanvas;
    public GameObject mainCanvas;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DisplayScoreSection(){
        scoreCanvas.SetActive(true);
        mainCanvas.SetActive(false);
    }

    public void Back(){
        scoreCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }
}
