using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class CustomerManager : MonoBehaviour
{

    public static CustomerManager singleton;
    public static List<GameObject> customerList;
    public GameObject customerPrefab;
    public Canvas canvas;

    private GameObject orderManager;
    private OrderManager orderManagerScript;


    private void Awake() {
        if (singleton == null)
        {
            DontDestroyOnLoad(gameObject);
            singleton = this;
            customerList = new List<GameObject>();
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
        if (sceneName == "OrderScene") {
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            orderManager = GameObject.Find("OrderManager");
            orderManagerScript = orderManager.GetComponent<OrderManager>(); 
            AddCustomer();
        }
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void AddCustomer() {
        RectTransform r = canvas.GetComponent<RectTransform>();
        CanvasScaler canvasScale = canvas.GetComponent<CanvasScaler>();
        float x = r.position.x;
        float y = r.position.y;
        Vector2 res = canvasScale.referenceResolution;
        float h = res.y;
        float w = res.x;

        GameObject newCustomer = Instantiate(customerPrefab, new Vector3(x - w / 4, y + h / 10, 0), Quaternion.identity);
        newCustomer.transform.SetParent(canvas.transform);
        newCustomer.transform.localScale = new Vector3(1, 1, 1);
    }
}
