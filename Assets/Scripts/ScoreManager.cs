using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager singleton;
    private static int totalScore;
    void Awake()
    {
        if (singleton == null)
        {
            totalScore = 0;
            DontDestroyOnLoad(gameObject);
            singleton = this;
        }
        else if (singleton != this)
        {
            Destroy (gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Score.scoreValue = totalScore;
    }

    public void addScore(Order order, Drink drink, float time) {
        float score = 0;
        if (time > 80) {
            score += Mathf.Max(0, 500 - ((time - 80) * 10));
        }
        else {
            score += 500;
        }
        score += drink.getMixScore() + drink.getSealScore();
        totalScore += (int) score;
    }
}
