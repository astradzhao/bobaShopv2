using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class CustomerManager : MonoBehaviour
{

    public static CustomerManager singleton;

    public Sprite backgroundDoorClosed;
    public Sprite backgroundDoorOpened;
    public GameObject sceneBackground;

    public AudioSource audiosource;

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

    private GameObject orderingPos1;
    private GameObject orderingPos2;
    private GameObject orderingPos3;
    private GameObject orderDonePos;

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

            audiosource = this.GetComponent<AudioSource>();
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
            sceneBackground = GameObject.Find("SceneBackground");
            orderingPos1 = GameObject.Find("Position1");
            orderingPos2 = GameObject.Find("Position2");
            orderingPos3 = GameObject.Find("Position3");
            orderDonePos = GameObject.Find("PositionOrderDone");
            sceneBackground.GetComponent<Image>().overrideSprite = backgroundDoorClosed;
            orderManagerScript = orderManager.GetComponent<OrderManager>();
            Invoke("ReloadCustomers", 0.001f);
        }
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Update() {
        if (sceneName == "OrderScene" && CanAddCustomer()) {
            // Play door opening
            audiosource.Play();

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
            // Play door opening
            audiosource.Play();
    
            if (!customerAtPos1) {
                Customer customer = new Customer(ChooseCustomer(), orderStatuses, "Position1", orderManagerScript.totalOrderCount + 1);
                customerAtPos1 = true;
                customerList.Add(customer);
            } else if (!customerAtPos2) {
                Customer customer = new Customer(ChooseCustomer(), orderStatuses, "Position2", orderManagerScript.totalOrderCount + 1);
                customerAtPos2 = true;
                customerList.Add(customer);
            } else if (!customerAtPos3) {
                Customer customer = new Customer(ChooseCustomer(), orderStatuses, "Position3", orderManagerScript.totalOrderCount + 1);
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
    private void AddCustomer(GameObject pos) {
        List<Sprite> custSprites = ChooseCustomer();
        Customer customer = new Customer(custSprites, orderStatuses, pos.name, orderManagerScript.totalOrderCount + 1);
        customerList.Add(customer);
        customer.setIsOnScene(true);

        GameObject customerObj = Instantiate(customerPrefab) as GameObject;
        customerObj.GetComponent<Image>().overrideSprite = customer.GetCurrSprite();
        
        GameObject customerBttnObj = customerObj.transform.GetChild(0).gameObject;

        customerObj.transform.SetParent(pos.transform);
        customerObj.transform.localScale = new Vector3(1, 1, 1);

        CanvasRenderer crCustomer = customerObj.GetComponent<CanvasRenderer>();
        CanvasRenderer crCustomerBttn = customerBttnObj.GetComponent<CanvasRenderer>();

        if (sceneName == "OrderScene") {

            StartCoroutine(FadeInCustomer(crCustomer, crCustomerBttn));
        }

        Button newCustomerBttn = customerBttnObj.GetComponent<Button>();
        newCustomerBttn.onClick.AddListener(orderManagerScript.AddOrder);
        newCustomerBttn.onClick.AddListener(delegate{CustomerDisappear(customer, customerObj, customerBttnObj);});
    }

    // Fades Customer in
    IEnumerator FadeInCustomer(CanvasRenderer crCustomer, CanvasRenderer crCustomerBttn) {
        if (crCustomer != null && crCustomerBttn != null && sceneBackground != null) {
            sceneBackground.GetComponent<Image>().overrideSprite = backgroundDoorOpened;
            crCustomer.SetAlpha(0f);
            crCustomerBttn.SetAlpha(0f);
            for (float alpha = 0f; alpha <= 2f; alpha += 0.1f) {
                if (crCustomer != null && crCustomerBttn != null) {
                crCustomer.SetAlpha(alpha);
                crCustomerBttn.SetAlpha(alpha);
                }
                yield return new WaitForSeconds(0.1f);
            }
            if (sceneBackground != null) {
                sceneBackground.GetComponent<Image>().overrideSprite = backgroundDoorClosed;
            }
        }
    }

    // Fades customer out
    IEnumerator FadeOutCustomer(Customer customer, GameObject customerObj, CanvasRenderer crCustomer, CanvasRenderer crCustomerBttn) {
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
        string pos = customer.GetCustPos();
        if (pos == "Position1") {
            customerAtPos1 = false;
        } else if (pos == "Position2") {
            customerAtPos2 = false;
        } else if (pos == "Position3") {
            customerAtPos3 = false;
        } else {
            customerAtOrderDone = false;
            customerList.Remove(customer);
        }
        Destroy(customerObj);
    }

    // Upon Clicking their order button
    private void CustomerDisappear(Customer customer, GameObject customerObj, GameObject customerBttnObj) {
        Button button = customerBttnObj.GetComponent<Button>();
        button.interactable = false;

        customerBttnObj.GetComponent<Image>().overrideSprite = orderStatuses[1];

        CanvasRenderer crCustomer = customerObj.GetComponent<CanvasRenderer>();
        CanvasRenderer crCustomerBttn = customerBttnObj.GetComponent<CanvasRenderer>();
        if (sceneName == "OrderScene") {
            StartCoroutine(FadeOutCustomer(customer, customerObj, crCustomer, crCustomerBttn));
        }
    }

    // Checks to see if a customer's order is done (Only one at a time)
    public void CheckCustomers(Order completedOrder) {
        if (!customerAtOrderDone) {
            foreach (Customer customer in customerList) {
                if (customer.isMyDrink(completedOrder)) {
                    customer.setCustPos("PositionOrderDone");
                    customer.setSpriteToOrderDone();
                    customer.setIsOnScene(true);
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
        GameObject customerObj = Instantiate(customerPrefab) as GameObject;
        customerObj.GetComponent<Image>().overrideSprite = customer.GetCurrSprite();
        string customerPos = customer.GetCustPos();
        GameObject pos = GameObject.Find(customerPos);
        customerObj.transform.SetParent(pos.transform);
        customerObj.transform.localScale = new Vector3(1, 1, 1);

        if (customer.GetCustPos() == "Position1") {
            customerAtPos1 = true;
        } else if (customer.GetCustPos() == "Position2") {
            customerAtPos2 = true;
        } else if (customer.GetCustPos() == "Position3") {
            customerAtPos3 = true;
        } else {
            customerAtOrderDone = true;
        }

        GameObject customerBttnObj = customerObj.transform.GetChild(0).gameObject;
        if (customer.GetCustPos() == "PositionOrderDone") {
            customerBttnObj.GetComponent<Image>().overrideSprite = orderStatuses[2];
        }
        CanvasRenderer crCustomer = customerObj.GetComponent<CanvasRenderer>();
        CanvasRenderer crCustomerBttn = customerBttnObj.GetComponent<CanvasRenderer>();

        Button customerBttn = customerBttnObj.GetComponent<Button>();

        if (customer.GetCustPos() != "PositionOrderDone") {
            customerBttn.onClick.AddListener(orderManagerScript.AddOrder);
        }
        customerBttn.onClick.AddListener(delegate{CustomerDisappear(customer, customerObj, customerBttnObj);});
    }
}
