using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    private static TimeManager singleton;
    public Text TimerText; 
    public bool playing;
    private float Timer;
    public float startingTime;

    public Text finalScore;
    public Text finalOrdersCompleted;

    private GameObject scoreManager;
    private ScoreManager scoreManagerScript;
    private GameObject orderManager;
    private OrderManager orderManagerScript;

    private void Awake() {
        if (singleton == null)
        {
            DontDestroyOnLoad(gameObject);
            singleton = this;
            playing = true;
            Timer = startingTime;
        }
        else if (singleton != this)
        {
            Destroy (gameObject);
        }
    }

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "GameOverScene") {
            finalScore = GameObject.Find("Final Score").GetComponent<Text>();
            finalOrdersCompleted = GameObject.Find("Orders Completed").GetComponent<Text>();
            orderManager = GameObject.Find("OrderManager");
            scoreManager = GameObject.Find("ScoreManager");
            orderManagerScript = orderManager.GetComponent<OrderManager>();
            scoreManagerScript = scoreManager.GetComponent<ScoreManager>();
            finalScore.text = "Final Score: " + scoreManagerScript.GetTotalScore().ToString();
            finalOrdersCompleted.text = "Orders Completed: " + orderManagerScript.GetTotalCompletedOrders().ToString();
        } else {
            TimerText = GameObject.Find("Timer").GetComponent<Text>();
        }
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Update () {

        if (playing == true) {
    
        Timer -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(Timer / 60F);
        int seconds = Mathf.FloorToInt(Timer % 60F);
        if (Timer > 0) {
            TimerText.text = "Time: " + minutes.ToString ("00") + ":" + seconds.ToString ("00");
        }
    }

        if (Timer <= 0) {
            Debug.Log("Game Over");
            playing = false;
            SceneManager.LoadScene("GameOverScene");
        }
    }
}
