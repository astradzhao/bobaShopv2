using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class CustomerManager : MonoBehaviour
{

    public static CustomerManager singleton;

    // All Different sprites for customers
    public List<Sprite> frogSprites = new List<Sprite>();
    public List<Sprite> bunnySprites = new List<Sprite>();
    public List<Sprite> kittySprites = new List<Sprite>();
    public List<Sprite> PandaSprites = new List<Sprite>();

    public List<Sprite> orderStatuses = new List<Sprite>();

    public static List<Customer> customerList;
    
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
    private Vector3 orderingPos3;
    private Vector3 orderDonePos;

    private bool customerAtPos1;
    private bool customerAtPos2;
    private bool customerAtPos3;
    private bool customerAtOrderDone;

    private GameObject orderManager;
    private OrderManager orderManagerScript;

    private string sceneName;


    private void Awake() {
        if (singleton == null)
        {
            DontDestroyOnLoad(gameObject);
            singleton = this;
            customerList = new List<Customer>();
            lastCustomerSpawned = 0;
            customerAtPos1 = false;
            customerAtPos2 = false;
            customerAtPos3 = false;
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
        sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "OrderScene") {
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            orderManager = GameObject.Find("OrderManager");
            orderManagerScript = orderManager.GetComponent<OrderManager>();
            ReloadCustomers();

            // Calculating Positions
            RectTransform r = canvas.GetComponent<RectTransform>();
            CanvasScaler canvasScale = canvas.GetComponent<CanvasScaler>();
            float x = r.position.x;
            float y = r.position.y;
            Vector2 res = canvasScale.referenceResolution;
            float h = res.y;
            float w = res.x;
            orderingPos1 = new Vector3(x - w / 2, y + h / 10, 0);
            orderingPos2 = new Vector3(x, y + h / 10, 0);
            orderingPos3 = new Vector3(x + w / 2, y + h / 10, 0);
            orderDonePos = new Vector3(x - w * 2 + w / 2, y - h / 10, 0);
        }
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Update() {
        if (sceneName == "OrderScene" && CanAddCustomer()) {
            if (!customerAtPos1) {
                AddCustomer(orderingPos1);
                customerAtPos1 = true;
            } else if (!customerAtPos2) {
                AddCustomer(orderingPos2);
                customerAtPos2 = true;
            } else if (!customerAtPos3) {
                AddCustomer(orderingPos3);
                customerAtPos3 = true;
            }
            lastCustomerSpawned = Time.time;
        } else if (CanAddCustomer()) {
            if (!customerAtPos1) {
                Customer customer = new Customer(ChooseCustomer(), orderStatuses, orderingPos1, orderManagerScript.totalOrderCount + 1);
                customerAtPos1 = true;
                customerList.Add(customer);
            } else if (!customerAtPos2) {
                Customer customer = new Customer(ChooseCustomer(), orderStatuses, orderingPos2, orderManagerScript.totalOrderCount + 1);
                customerAtPos2 = true;
                customerList.Add(customer);
            } else if (!customerAtPos3) {
                Customer customer = new Customer(ChooseCustomer(), orderStatuses, orderingPos3, orderManagerScript.totalOrderCount + 1);
                customerAtPos3 = true;
                customerList.Add(customer);
            }
            lastCustomerSpawned = Time.time;
        } else {
            // Do nothing
        }


    }

    private bool CanAddCustomer() {
        return Time.time > lastCustomerSpawned + customerSpawnRate && (!customerAtPos1 || !customerAtPos2 || !customerAtPos3);
    }

    // Chooses the sprites associated with a customer
    private List<Sprite> ChooseCustomer() {
        int spriteNum = UnityEngine.Random.Range(0, 4);
        if (spriteNum == 0) {
            return frogSprites;
        } else if (spriteNum == 1) {
            return bunnySprites;
        } else if (spriteNum == 2) {
            return kittySprites;
        } else {
            return PandaSprites;
        }
    }

    // Adds a customer to the given position. Only happens on order scene
    private void AddCustomer(Vector3 pos) {
        List<Sprite> custSprites = ChooseCustomer();
        Customer customer = new Customer(custSprites, orderStatuses, pos, orderManagerScript.totalOrderCount + 1);
        customerList.Add(customer);
        customer.setIsOnScene(true);

        GameObject newCustomer = Instantiate(customerPrefab, pos, Quaternion.identity);
        newCustomer.GetComponent<Image>().overrideSprite = customer.GetCurrSprite();
        
        GameObject newCustomerBttnObj = newCustomer.transform.GetChild(0).gameObject;

        newCustomer.transform.SetParent(canvas.transform);
        newCustomer.transform.localScale = new Vector3(1, 1, 1);

        CanvasRenderer crCustomer = newCustomer.GetComponent<CanvasRenderer>();
        CanvasRenderer crCustomerBttn = newCustomerBttnObj.GetComponent<CanvasRenderer>();
        if (sceneName == "OrderScene") {
            StartCoroutine(FadeInCustomer(crCustomer, crCustomerBttn));
        }

        Button newCustomerBttn = newCustomerBttnObj.GetComponent<Button>();
        newCustomerBttn.onClick.AddListener(orderManagerScript.AddOrder);
        newCustomerBttn.onClick.AddListener(delegate{CustomerDisappear(customer, newCustomer, newCustomerBttnObj, pos);});
    }

    // Fades Customer in
    IEnumerator FadeInCustomer(CanvasRenderer crCustomer, CanvasRenderer crCustomerBttn) {
        if (crCustomer != null && crCustomerBttn != null) {
            crCustomer.SetAlpha(0f);
            crCustomerBttn.SetAlpha(0f);
            for (float alpha = 0f; alpha <= 2f; alpha += 0.1f) {
                if (crCustomer != null && crCustomerBttn != null) {
                crCustomer.SetAlpha(alpha);
                crCustomerBttn.SetAlpha(alpha);
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    // Fades customer out
    IEnumerator FadeOutCustomer(Customer customer, GameObject customerObj, Vector3 pos, CanvasRenderer crCustomer, CanvasRenderer crCustomerBttn) {
        if (crCustomer != null && crCustomerBttn != null) {
            for (float alpha = 2f; alpha >= 0f; alpha -= 0.1f) {
                if (crCustomer != null && crCustomerBttn != null) {
                    crCustomer.SetAlpha(alpha);
                    crCustomerBttn.SetAlpha(alpha);
                }
            yield return new WaitForSeconds(0.1f);
            }
        }
        customer.setIsOnScene(false);
        if (pos == orderingPos1) {
            customerAtPos1 = false;
        } else if (pos == orderingPos2) {
            customerAtPos2 = false;
        } else if (pos == orderingPos3) {
            customerAtPos3 = false;
        } else {
            customerAtOrderDone = false;
            customerList.Remove(customer);
        }
        Destroy(customerObj);
    }

    // Upon Clicking their order button
    private void CustomerDisappear(Customer customer, GameObject customerObj, GameObject customerBttnObj, Vector3 pos) {
        Button button = customerBttnObj.GetComponent<Button>();
        button.interactable = false;

        customerBttnObj.GetComponent<Image>().overrideSprite = orderStatuses[1];

        CanvasRenderer crCustomer = customerObj.GetComponent<CanvasRenderer>();
        CanvasRenderer crCustomerBttn = customerBttnObj.GetComponent<CanvasRenderer>();
        if (sceneName == "OrderScene") {
            StartCoroutine(FadeOutCustomer(customer, customerObj, pos, crCustomer, crCustomerBttn));
        }
    }

    // Checks to see if a customer's order is done (Only one at a time)
    public void CheckCustomers(Order completedOrder) {
        if (!customerAtOrderDone) {
            foreach (Customer customer in customerList) {
                if (customer.isMyDrink(completedOrder)) {
                    customer.setCustPos(orderDonePos);
                    customer.setSpriteToOrderDone();
                    customer.setIsOnScene(true);
                    customerAtOrderDone = true;
                    return;
                }
            }
        }
    }

    // Reloads the customers after changing scenes
    private void ReloadCustomers() {
        if (customerList.Count > 0) {
            foreach (Customer customer in customerList) {
                if (customer.CheckOnScene()) {
                    ReAdd(customer);
                }
            }
        } 
    }

    // Readds the customer into the scene.
    private void ReAdd(Customer customer) {
        GameObject newCustomer = Instantiate(customerPrefab, customer.GetCustPos(), Quaternion.identity);
        newCustomer.GetComponent<Image>().overrideSprite = customer.GetCurrSprite();
        newCustomer.transform.SetParent(canvas.transform);
        newCustomer.transform.localScale = new Vector3(1, 1, 1);

        if (customer.GetCustPos() == orderingPos1) {
            customerAtPos1 = true;
        } else if (customer.GetCustPos() == orderingPos2) {
            customerAtPos2 = true;
        } else if (customer.GetCustPos() == orderingPos3) {
            customerAtPos3 = true;
        } else {
            customerAtOrderDone = true;
        }

        GameObject newCustomerBttnObj = newCustomer.transform.GetChild(0).gameObject;

        CanvasRenderer crCustomer = newCustomer.GetComponent<CanvasRenderer>();
        CanvasRenderer crCustomerBttn = newCustomerBttnObj.GetComponent<CanvasRenderer>();

        Button newCustomerBttn = newCustomerBttnObj.GetComponent<Button>();

        if (customer.GetCustPos() != orderDonePos) {
            newCustomerBttn.onClick.AddListener(orderManagerScript.AddOrder);
        }
        newCustomerBttn.onClick.AddListener(delegate{CustomerDisappear(customer, newCustomer, newCustomerBttnObj, customer.GetCustPos());});
    }
}
