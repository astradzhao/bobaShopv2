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

    // The time between customer spawns
    public float customerSpawnRate;
    // The time of the last spawned customer
    private float lastCustomerSpawned;
    // The timer that determines customer spawning
    private float customerSpawnTimer;
    
    private Vector3 orderingPos1;
    private Vector3 orderingPos2;
    private Vector3 orderDonePos;

    private bool customerAtPos1;
    private bool customerAtPos2;
    private bool customerAtOrderDone;

    private GameObject orderManager;
    private OrderManager orderManagerScript;


    private void Awake() {
        if (singleton == null)
        {
            DontDestroyOnLoad(gameObject);
            singleton = this;
            customerList = new List<GameObject>();
            lastCustomerSpawned = 0;
            customerAtPos1 = false;
            customerAtPos2 = false;
            customerAtOrderDone = false;
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

            // Calculating Positions
            RectTransform r = canvas.GetComponent<RectTransform>();
            CanvasScaler canvasScale = canvas.GetComponent<CanvasScaler>();
            float x = r.position.x;
            float y = r.position.y;
            Vector2 res = canvasScale.referenceResolution;
            float h = res.y;
            float w = res.x;
            orderingPos1 = new Vector3(x - w / 4, y + h / 10, 0);
            orderingPos2 = new Vector3(x + w / 4, y + h / 10, 0);
            orderDonePos = new Vector3(x - w * 2 + w / 2, y - h / 10, 0);
        }
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Update() {
        if (CanAddCustomer()) {
            if (!customerAtPos1) {
                AddCustomer(orderingPos1);
                customerAtPos1 = true;
            } else {
               AddCustomer(orderingPos2);
               customerAtPos2 = true;
            }
            lastCustomerSpawned = Time.time;
        }
    }

    private bool CanAddCustomer() {
        return Time.time > lastCustomerSpawned + customerSpawnRate && (!customerAtPos1 || !customerAtPos2);
    }

    // Adds a customer to the given position.
    private void AddCustomer(Vector3 pos) {
        GameObject newCustomer = Instantiate(customerPrefab, pos, Quaternion.identity);
        GameObject newCustomerBttnObj = newCustomer.transform.GetChild(0).gameObject;
        newCustomer.transform.SetParent(canvas.transform);
        newCustomer.transform.localScale = new Vector3(1, 1, 1);

        CanvasRenderer crCustomer = newCustomer.GetComponent<CanvasRenderer>();
        CanvasRenderer crCustomerBttn = newCustomerBttnObj.GetComponent<CanvasRenderer>();
        StartCoroutine(FadeInCustomer(crCustomer, crCustomerBttn));

        Button newCustomerBttn = newCustomerBttnObj.GetComponent<Button>();
        newCustomerBttn.onClick.AddListener(orderManagerScript.AddOrder);
        newCustomerBttn.onClick.AddListener(delegate{CustomerDisappear(newCustomer, newCustomerBttnObj, pos);});
    }

    IEnumerator FadeInCustomer(CanvasRenderer crCustomer, CanvasRenderer crCustomerBttn) {
        crCustomer.SetAlpha(0f);
        crCustomerBttn.SetAlpha(0f);
        for (float alpha = 0f; alpha <= 2f; alpha += 0.1f) {
            crCustomer.SetAlpha(alpha);
            crCustomerBttn.SetAlpha(alpha);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator FadeOutCustomer(GameObject customer, CanvasRenderer crCustomer, CanvasRenderer crCustomerBttn, Vector3 pos) {
        for (float alpha = 2f; alpha >= 0f; alpha -= 0.1f) {
            crCustomer.SetAlpha(alpha);
            crCustomerBttn.SetAlpha(alpha);
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(customer);
        if (pos == orderingPos1) {
            customerAtPos1 = false;
        } else {
            customerAtPos2 = false;
        }
    }

    private void CustomerDisappear(GameObject customer, GameObject customerBttnObj, Vector3 pos) {
        Button button = customerBttnObj.GetComponent<Button>();
        button.interactable = false;

        CanvasRenderer crCustomer = customer.GetComponent<CanvasRenderer>();
        CanvasRenderer crCustomerBttn = customerBttnObj.GetComponent<CanvasRenderer>();
        StartCoroutine(FadeOutCustomer(customer, crCustomer, crCustomerBttn, pos));
        


        // Set customerPos to false
        // Add the customer to List of customer-order links

    }
}
