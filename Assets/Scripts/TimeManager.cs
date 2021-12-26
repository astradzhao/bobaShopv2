using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    private static TimeManager singleton;
    private string sceneName;
    public Text TimerText; 
    public bool playing;
    private float Timer;
    public float startingTime;

    public Text finalScore;
    public Text finalOrdersCompleted;

    private Button playAgainBttn;

    private GameObject scoreManager;
    private ScoreManager scoreManagerScript;
    private GameObject orderManager;
    private OrderManager orderManagerScript;
    private GameObject customerManager;
    private CustomerManager customerManagerScript;
    private GameObject drinkManager;
    private DrinkManager drinkManagerScript;
    private GameObject mixingManager;
    private MixingManager mixingManagerScript;
    private GameObject soundManager;
    private SoundManager soundManagerScript;

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
        sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "GameOverScene") {
            finalScore = GameObject.Find("Final Score").GetComponent<Text>();
            finalOrdersCompleted = GameObject.Find("Orders Completed").GetComponent<Text>();
            orderManager = GameObject.Find("OrderManager");
            scoreManager = GameObject.Find("ScoreManager");
            customerManager = GameObject.Find("CustomerManager");
            drinkManager = GameObject.Find("DrinkManager");
            mixingManager = GameObject.Find("MixerManager");
            soundManager = GameObject.Find("SoundManager");
            orderManagerScript = orderManager.GetComponent<OrderManager>();
            scoreManagerScript = scoreManager.GetComponent<ScoreManager>();
            customerManagerScript = customerManager.GetComponent<CustomerManager>();
            drinkManagerScript = drinkManager.GetComponent<DrinkManager>();
            mixingManagerScript = mixingManager.GetComponent<MixingManager>();
            soundManagerScript = soundManager.GetComponent<SoundManager>();

            finalScore.text = "Final Score: " + scoreManagerScript.GetTotalScore().ToString();
            finalOrdersCompleted.text = "Orders Completed: " + orderManagerScript.GetTotalCompletedOrders().ToString();

            playAgainBttn = GameObject.Find("PlayAgain").GetComponent<Button>();
            playAgainBttn.onClick.AddListener(soundManagerScript.PlayAudioBttn);
            playAgainBttn.onClick.AddListener(ResetGame);
        } else {
            TimerText = GameObject.Find("Timer").GetComponent<Text>();
        }
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void ResetGame() {
        orderManagerScript.Reset();
        scoreManagerScript.Reset();
        customerManagerScript.Reset();
        drinkManagerScript.Reset();
        mixingManagerScript.Reset();

        SceneManager.LoadScene("OrderScene");

        Timer = startingTime;
        playing = true;
    }

    void Update () {
        if (playing == true) {
            Timer -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(Timer / 60F);
            int seconds = Mathf.FloorToInt(Timer % 60F);
            if (Timer > 0 && TimerText != null) {
                TimerText.text = "Time: " + minutes.ToString ("00") + ":" + seconds.ToString ("00");
            }   
        }

        if (Timer <= 0 && sceneName != "GameOverScene") {
            Debug.Log("Game Over");
            playing = false;
            SceneManager.LoadScene("GameOverScene");
        }
    }
}
