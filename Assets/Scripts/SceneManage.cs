using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class SceneManage : MonoBehaviour
{
    public void OrderScene()
    {
        SceneManager.LoadScene("OrderScene");
    }

    public void BasesScene()
    {
        SceneManager.LoadScene("TeaBaseScene");
    }

    public void IngredientsScene() {
        SceneManager.LoadScene("IngredientsScene");
    }

    public void SealingScene() {
        SceneManager.LoadScene("SealingScene");
    }
}