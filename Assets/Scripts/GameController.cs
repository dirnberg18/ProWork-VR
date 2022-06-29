using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // public variables
    public static GameController instance; //Singelton to call it somewhere else
    public GameObject UIElements;
    public TMP_Text flowerCounter;
    public bool gamePlaying { get; private set; } //safety meassure, we don't want to be able to change something when the game is over
    public float timeRemaining;
    public bool timerIsRunning = false;
    //public float timeRemaining2; //only for testing winning screen 
    private bool timerIsRunning2 = false; //only for testing winning screen
    public TMP_Text timeText;
    //public Button startButton;
    //public GameObject gameOverPanel, introscreen1, introscreen2, winPanel;

    // private variables
    private int numTotalFlowers, numPickedFlowers;

    private void Awake() // called a little before Start() function
    {
        instance = this; //in other Scripts we can call GameController.instance
    }

    private void Start()
    {
        numTotalFlowers = 500;
        numPickedFlowers = 0;
        flowerCounter.GetComponent<TMP_Text>().text = "Points: " + numPickedFlowers; //flowerCounter.GetComponent<TMP_Text>().text = "Flowers: " + numPickedFlowers + " / "+ numTotalFlowers;

        timerIsRunning = true;
        timerIsRunning2 = true;
        gamePlaying = true;


        //gamePlaying = false;
        //introscreen1.SetActive(true);
        //introscreen2.SetActive(false);
        //Button btn = startButton.GetComponent<Button>();
        //btn.onClick.AddListener(BeginGame);
        // BeginGame(); //Should be called in On Click, WAR VORHER SCHON AUS
    }

   /* private void BeginGame()
    {
        Debug.Log("You have clicked the button!");
        timerIsRunning = true;
        timerIsRunning2 = true;
        gamePlaying = true;
        introscreen1.SetActive(false);
        introscreen2.SetActive(false);
    }*/

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                Debug.Log("Time has run out!");
                //GameOver();
            }
        }

       /* if (timerIsRunning2)
        {
            if (timeRemaining2 > 0)
            {
                timeRemaining2 -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Congratulation, you won!");
                timeRemaining2 = 0;
                timerIsRunning2 = false;
                //WonGame();
            }
        }*/
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.GetComponent<TMP_Text>().text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void PickFlower()
    {
        numPickedFlowers++;
        string flowerCounterStr = "Flower: " + numPickedFlowers + " / " + numTotalFlowers;
        flowerCounter.GetComponent<TMP_Text>().text = flowerCounterStr;
    }

    /*private void GameOver()
    {
        gamePlaying = false;
        Invoke("ShowGameOverScreen", 1.25f); // f in the end stands for float, to specify it is not a double, numerical etc type
    }*/

    /*private void ShowGameOverScreen()
    {
        gameOverPanel.SetActive(true);
        UIElements.SetActive(false);
        gameOverPanel.transform.Find("TryAgain").GetComponent<Button>().Select();
    }*/

    /*private void WonGame()
    {
        gamePlaying = false;
        Invoke("ShowWinScreen", 1.25f); // f in the end stands for float, to specify it is not a double, numerical etc type
    }*/

    /*private void ShowWinScreen()
    {
        winPanel.SetActive(true);
        UIElements.SetActive(false);
        winPanel.transform.Find("Menu").GetComponent<Button>().Select();
    }*/

    /*public void OnButtonLoadLevel(string levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad);
    }*/
}
