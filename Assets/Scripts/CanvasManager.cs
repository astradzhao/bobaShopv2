using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager singleton;
    public static GameObject orderSceneCanvas; 
    
    private void Awake() {
        if (singleton == null)
        {
            DontDestroyOnLoad(gameObject);
            singleton = this;
            orderSceneCanvas = GameObject.Find("Canvas");
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
        if (sceneName != "OrderScene") {
            Debug.Log("Not on order scene");
            orderSceneCanvas.SetActive(false);
        } else {
            Debug.Log("On OrderScene");
            orderSceneCanvas.SetActive(true);
        }
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
