using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour {
    public float gameTime;
    public GameOverHandler gameOver;
	// Use this for initialization
	void Start () {
        gameTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if(!gameOver.gameIsOver && !gameOver.gameWon)
        {
            gameTime += Time.deltaTime;
        }
	}
}
