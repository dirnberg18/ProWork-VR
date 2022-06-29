using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
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
    
    [Header("Flower Variables")]
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


    void StartButtonFunction(){
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

    void DestroyFlower(GameObject _flower){
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
