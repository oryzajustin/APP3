using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour {
    public PlayerController playerControls;
    public MouseController playerView;
    public bool gameIsOver;
    public bool gameWon;
    public GameTimer gameTimer;
    public Canvas gameOverCanvas;
    public Text winText;
    public Canvas deathCanvas;
    private string writeToFile;

    public void Start()
    {
        gameIsOver = false;
        gameWon = false;
    }

    public void Update()
    {
        if(gameWon){
            gameOverCanvas.gameObject.SetActive(true);
            winText.text = "You won with time: " + gameTimer.gameTime.ToString();
        }
        if(gameIsOver){
            deathCanvas.gameObject.SetActive(true);
        }
    }

    public void GameOver(){
        //when player dies
        playerControls.disabled = true;
        playerView.frozen = true;
        gameIsOver = true;
    }
    public void GameSuccess(){
        Cursor.lockState = CursorLockMode.None;
        playerControls.disabled = true;
        playerView.frozen = true;
        gameWon = true;
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("Menu");
        if (gameWon == true)
        {
            writeToFile = "You won with time of: " + gameTimer.gameTime.ToString();
            System.IO.File.AppendAllText("./Assets/Scripts/scorefile.txt", writeToFile + "\n");
        }
    }
}
