using Random=UnityEngine.Random;
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
    //public static GameController instance; //Singelton to call it somewhere else
    //public GameObject UIElements;
    //public TMP_Text flowerCounter;
    //public bool gamePlaying { get; private set; } //safety meassure, we don't want to be able to change something when the game is over
    //public float timeRemaining;
    //public bool timerIsRunning = false;
    //public float timeRemaining2; //only for testing winning screen 
    //private bool timerIsRunning2 = false; //only for testing winning screen
    //public TMP_Text timeText;
    //public Button startButton;
    //public GameObject gameOverPanel, introscreen1, introscreen2, winPanel;

    // private variables
    //private int numTotalFlowers, numPickedFlowers;

    public static GameController instance;

    private void Awake(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else{
            Destroy(this.gameObject);
        }
    }

[Header("SpawnPoint Variables")]
    public Transform SpawnPoints;

    public List<Vector3> SpawnPointList = new List<Vector3>();

[Header("Flower Variables")]
    public GameObject flowerPrefab;

    public List<GameObject> FlowersInScene = new List<GameObject>();

    public Transform playerVR; 

    int score;
    
    [Header("Timer Variables")]
    float timer;
    bool canCount = false;
    int minutes;
    int seconds;
    string timeStr;
    float durationFlower = 3f;

    [Header("UIVariable")]
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI scoreText;
    public GameObject startButton;

    private void Start()
    {
        StartFunction();
    }

    void StartFunction(){
        score = 0;
        foreach(Transform item in SpawnPoints){
            SpawnPointList.Add(item.localPosition);
        }
        
        timer = 60f;
        canCount = true;
        timeText.text = " 1 : 00 ";
        scoreText.text = "0";
        InvokeRepeating("GetFlower", 1f, durationFlower);
    }

    private void Update(){
            if(timer > 0.0f && canCount){
                timer -= Time.deltaTime;
                minutes = Mathf.FloorToInt(timer/60f);
                seconds = Mathf.FloorToInt(timer - minutes *60);
                timeStr = string.Format("{0:0}:{1:00}", minutes,seconds);
                timeText.text = timeStr;
            }
            else if(timer <= 0.0f && canCount){
                Finish();
            }
    }

    void Finish(){
        canCount = false;
        timeText.text = "0 : 00";
    }

    void GetFlower(){
        if(SpawnPointList.Count > 0){
            Vector3 randomPosition = SpawnPointList[Random.Range(0, SpawnPointList.Count)];
            SpawnPointList.Remove(randomPosition);

            GameObject currentFlower = Instantiate(flowerPrefab,randomPosition,Quaternion.identity,transform);
            currentFlower.transform.LookAt(new Vector3(playerVR.position.x,playerVR.transform.position.y,playerVR.position.z));
            currentFlower.SetActive(true);
            FlowersInScene.Add(currentFlower);
        }
        else{
            Debug.Log("Reach Max Flowers");
        }
    }

    public void DestroyFlower(GameObject _flower){
        SpawnPointList.Add(_flower.transform.localPosition);
        FlowersInScene.Remove(_flower);
        Destroy(_flower);
        score+= 10;
        scoreText.text = score.ToString();
        
        if(durationFlower<= 0.6f){
            durationFlower -= 0.1f; 
        }
    }
    }



    /*private void Start()
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
    }*/

   /* private void BeginGame()
    {
        Debug.Log("You have clicked the button!");
        timerIsRunning = true;
        timerIsRunning2 = true;
        gamePlaying = true;
        introscreen1.SetActive(false);
        introscreen2.SetActive(false);
    }*/
/*
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
        }*/

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
        }
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
    }*/

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
    }
}*/
