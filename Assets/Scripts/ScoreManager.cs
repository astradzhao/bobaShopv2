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

    public void Reset() {
        totalScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Score.scoreValue = totalScore;
    }

    public void addScore(Drink drink, float time) {
        float score = 0;
        if (time > 95) {
            score += Mathf.Max(0, 500 - ((time - 95) * 10));
        }
        else {
            score += 500;
        }
        score += drink.getMixScore() + drink.getSealScore();
        totalScore += (int) score;
    }

    public float GetTotalScore() {
        return totalScore;
    }
}
