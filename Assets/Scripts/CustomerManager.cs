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
    private GameObject soundManager;
    private SoundManager soundManagerScript;
    private GameObject scoreManager;
    private ScoreManager scoreManagerScript;
    private GameObject drinkManager;
    private DrinkManager drinkManagerScript;
    private GameObject inputManager;
    private InputManager inputManagerScript;

    private string sceneName;

    private void Awake() {
        if (singleton == null)
        {
            DontDestroyOnLoad(gameObject);
            singleton = this;
            customerList = new List<Customer>();
            lastCustomerSpawned = 0f;
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
            soundManager = GameObject.Find("SoundManager");
            scoreManager = GameObject.Find("ScoreManager");
            drinkManager = GameObject.Find("DrinkManager");
            inputManager = GameObject.Find("InputManager");
            sceneBackground = GameObject.Find("SceneBackground");
            orderingPos1 = GameObject.Find("Position1");
            orderingPos2 = GameObject.Find("Position2");
            orderingPos3 = GameObject.Find("Position3");
            orderDonePos = GameObject.Find("PositionOrderDone");
            sceneBackground.GetComponent<Image>().overrideSprite = backgroundDoorClosed;
            orderManagerScript = orderManager.GetComponent<OrderManager>();
            soundManagerScript = soundManager.GetComponent<SoundManager>();
            scoreManagerScript = scoreManager.GetComponent<ScoreManager>();
            drinkManagerScript = drinkManager.GetComponent<DrinkManager>();
            inputManagerScript = inputManager.GetComponent<InputManager>();
            Invoke("ReloadCustomers", 0.001f);
        }
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start() {
        ReloadCustomers();
    }

    private void Update() {
        string customerNumbers = "";
            foreach (Customer customer in customerList) {
                customerNumbers += customer.GetCustOrder().ToString() + " ";
            }
        Debug.Log("list of customers: " + customerNumbers);
        //Debug.Log("Customer List" + customerList.Count.ToString());
        //Debug.Log("Order List" + orderManagerScript.GetOrderCount());
        if (sceneName == "OrderScene" && CanAddCustomer()) {
            // Play door opening
            audiosource.Play();

            if (!customerAtPos1) {
                AddCustomer(orderingPos1);
                customerAtPos1 = true;
                lastCustomerSpawned = Time.time;
            } else if (!customerAtPos2) {
                AddCustomer(orderingPos2);
                customerAtPos2 = true;
                lastCustomerSpawned = Time.time;
            } else if (!customerAtPos3) {
                AddCustomer(orderingPos3);
                customerAtPos3 = true;
                lastCustomerSpawned = Time.time;
            }
        } else if (CanAddCustomer() && sceneName != "StartScene") {
            // Play door opening
            audiosource.Play();
    
            if (!customerAtPos1) {
                Customer customer = new Customer(ChooseCustomer(), orderStatuses, "Position1");
                customerAtPos1 = true;
                customerList.Add(customer);
                lastCustomerSpawned = Time.time;
            } else if (!customerAtPos2) {
                Customer customer = new Customer(ChooseCustomer(), orderStatuses, "Position2");
                customerAtPos2 = true;
                customerList.Add(customer);
                lastCustomerSpawned = Time.time;
            } else if (!customerAtPos3) {
                Customer customer = new Customer(ChooseCustomer(), orderStatuses, "Position3");
                customerAtPos3 = true;
                customerList.Add(customer);
                lastCustomerSpawned = Time.time;
            }
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

    // Adds a customer to the given position. Only happens on order scene.
    private void AddCustomer(GameObject pos) {

        List<Sprite> custSprites = ChooseCustomer();
        Customer customer = new Customer(custSprites, orderStatuses, pos.name);
        customerList.Add(customer);
        customer.setIsOnScene(true);

        GameObject customerObj = Instantiate(customerPrefab) as GameObject;
        customerObj.GetComponent<Image>().overrideSprite = customer.GetCurrSprite();
        customerObj.transform.SetParent(pos.transform);
        customerObj.transform.localScale = new Vector3(1, 1, 1);
        
        CanvasGroup cgCustomer = customerObj.GetComponent<CanvasGroup>();
        cgCustomer.alpha = 0f;
        cgCustomer.interactable = false;

        GameObject customerBttnObj = customerObj.transform.GetChild(0).gameObject;
        Button newCustomerBttn = customerBttnObj.GetComponent<Button>();
        newCustomerBttn.onClick.AddListener(delegate{inputManagerScript.UpdateLastOrder(newCustomerBttn);});
        newCustomerBttn.onClick.AddListener(AddCustSpawnDelay);
        newCustomerBttn.onClick.AddListener(orderManagerScript.AddOrder);
        newCustomerBttn.onClick.AddListener(delegate{UpdateCustOrderNum(customer);});
        newCustomerBttn.onClick.AddListener(delegate{CustomerDisappear(customer, customerObj, customerBttnObj, cgCustomer);});
        newCustomerBttn.onClick.AddListener(soundManagerScript.PlayAudioCustomerOrder);
       
        StartCoroutine(FadeInCustomer(cgCustomer));
    }

    // Fades Customer in
    IEnumerator FadeInCustomer(CanvasGroup cgCustomer) {
        if (cgCustomer != null && sceneBackground != null) {
            sceneBackground.GetComponent<Image>().overrideSprite = backgroundDoorOpened;
            for (float alpha = 0f; alpha <= 2f; alpha += 0.1f) {
                if (cgCustomer != null) {
                    cgCustomer.alpha = alpha;
                }
                yield return new WaitForSeconds(0.08f);
            }
            if (sceneBackground != null) {
                sceneBackground.GetComponent<Image>().overrideSprite = backgroundDoorClosed;
                cgCustomer.interactable = true;
            }
        }
    }

    // Fades customer out
    IEnumerator FadeOutCustomer(Customer customer, GameObject customerObj, CanvasGroup cgCustomer) {
        if (cgCustomer != null) {
            for (float alpha = 2f; alpha >= 0f; alpha -= 0.1f) {
                if (cgCustomer != null) {
                    cgCustomer.alpha = alpha;
                }
            yield return new WaitForSeconds(0.08f);
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
        } else if (pos == "PositionOrderDone") {
            customerAtOrderDone = false;
            customerList.Remove(customer);
        }
        Destroy(customerObj);
    }

    // Upon Clicking their order button
    private void CustomerDisappear(Customer customer, GameObject customerObj, GameObject customerBttnObj, CanvasGroup cgCustomer) {
        cgCustomer.interactable = false;
        customerBttnObj.GetComponent<Image>().overrideSprite = orderStatuses[1];
        if (sceneName == "OrderScene") {
            StartCoroutine(FadeOutCustomer(customer, customerObj, cgCustomer));
        }
    }

    // Checks to see if a customer's order is done (Only one at a time)
    public void CheckCustomers(Order order, Drink drink, float timeCompleted) {
        if (!customerAtOrderDone) {
            //Debug.Log("OrderDonePos free when checking for: " + order.GetOrderNum());
            foreach (Customer customer in customerList) {
                if (customer.isMyDrink(order)) {
                    //Debug.Log("Customer found for order #: " + customer.GetCustOrder());
                    customer.setCustPos("PositionOrderDone");
                    customer.setSpriteToOrderDone();
                    customer.setIsOnScene(true);
                    customer.SetCompletedDrink(order, drink, timeCompleted);
                    return;
                }
            }
            // Debug.Log("No customers found for order #: " + order.GetOrderNum());
            // string customerNumbers = "";
            // foreach (Customer customer in customerList) {
            //     customerNumbers += customer.GetCustOrder().ToString() + " ";
            // }
            //Debug.Log("list of customers: " + customerNumbers);
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
        } else if (customer.GetCustPos() == "PositionOrderDone") {
            customerAtOrderDone = true;
        }

        // Checks to see if a customer's order is done with their order
        GameObject customerBttnObj = customerObj.transform.GetChild(0).gameObject;
        if (customer.GetCustPos() == "PositionOrderDone") {
            customerBttnObj.GetComponent<Image>().overrideSprite = orderStatuses[2];
        }

        Button customerBttn = customerBttnObj.GetComponent<Button>();
        if (customer.GetCustPos() == "PositionOrderDone") {
            customerBttn.onClick.AddListener(delegate{SetUpOrderCompletion(customer);});
        } else {
            customerBttn.onClick.AddListener(delegate{inputManagerScript.UpdateLastOrder(customerBttn);});
            customerBttn.onClick.AddListener(AddCustSpawnDelay);
            customerBttn.onClick.AddListener(orderManagerScript.AddOrder);
            customerBttn.onClick.AddListener(delegate{UpdateCustOrderNum(customer);});
            customerBttn.onClick.AddListener(soundManagerScript.PlayAudioCustomerOrder);
        }
        CanvasGroup cgCustomer = customerObj.GetComponent<CanvasGroup>();
        customerBttn.onClick.AddListener(delegate{CustomerDisappear(customer, customerObj, customerBttnObj, cgCustomer);});
        customerBttn.onClick.AddListener(soundManagerScript.PlayAudioCustomerOrder);
    }

    // Adds score and deletes customer + their order upon completion of order.
    private void SetUpOrderCompletion(Customer customer) {
        scoreManagerScript.addScore(customer.GetCompletedDrink(), customer.GetTimeCompleted());
        orderManagerScript.IncreaseCompletedOrders();
        drinkManagerScript.RemoveFromOrder(customer.GetCompletedDrink());
        orderManagerScript.RemoveOrder(customer.GetCompletedOrder());
        orderManagerScript.ReloadOrderText();
    }

    // Assigns the customer the corresponding order number upon clicking order button.
    private void UpdateCustOrderNum(Customer customer) {
        customer.AssignCustOrderNum(orderManagerScript.GetOrderCount());
    }

    // Adds a delay for the next customer to spawn upon clicking order button.
    private void AddCustSpawnDelay() {
        lastCustomerSpawned = Time.time;
    }
    
}
