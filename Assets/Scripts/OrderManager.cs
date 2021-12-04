using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class OrderManager : MonoBehaviour
{
    public Button myOrdersBtn;
    public static List<Order> orderList;
    private static List<float> orderTimers;
    public GameObject drinkManager;
    public GameObject scoreManager;
    public GameObject customerManager;
    public static OrderManager singleton;
    private List<Drink> drinksOnOrderScene;
    private static Order currOrder;
    private static bool myOrdersTabOpen;
    public int totalOrderCount;
    public static int ordersCompleted;

    public GameObject buttonScrollList;
    public Transform buttonListContent;
    public GameObject newOrderBttnPrefab;

    private void Awake() {
        totalOrderCount = 0;
        ordersCompleted = 0;
        if (singleton == null)
        {
            DontDestroyOnLoad(gameObject);
            singleton = this;
            ordersCompleted = 0;
            orderList = new List<Order>();
            orderTimers = new List<float>();
            currOrder = null;
        }
        else if (singleton != this)
        {
            Destroy (gameObject);
        }
    }

    void Update() {
        for (int i = 0; i < orderTimers.Count; i++) {
            orderTimers[i] += Time.deltaTime;
        }
    }


    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        myOrdersBtn = GameObject.Find("My Orders").GetComponent<Button>();
        myOrdersBtn.onClick.AddListener(ShowOrderList);
        buttonScrollList = GameObject.Find("ButtonScrollList");
        buttonListContent = GameObject.Find("ButtonListContent").transform;
        if (!myOrdersTabOpen) {
           buttonScrollList.SetActive(false); 
        }
        drinksOnOrderScene = new List<Drink>();
        drinkManager = GameObject.Find("DrinkManager");
        scoreManager = GameObject.Find("ScoreManager");
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "OrderScene") {
            customerManager = GameObject.Find("CustomerManager");
            CheckDrinks();
        }
        ReloadOrderText();
        ReAddMyOrdersList();
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    public void AddOrder() {
        totalOrderCount += 1;
        Order newOrder = new Order(totalOrderCount); // Added
        orderList.Add(newOrder);
        orderTimers.Add(0);
        currOrder = newOrder;
        ReloadOrderText();
        AddOrderButton(newOrder);
    }

     // Adds new order button to the UI
    private void AddOrderButton(Order order) {
        GameObject newButton = Instantiate(newOrderBttnPrefab) as GameObject;
        newButton.transform.SetParent(buttonListContent, false);
        Text newButtonText = newButton.transform.GetChild(0).gameObject.GetComponent<Text>();
        newButtonText.text = order.GetOrderNum().ToString();
    }

    public void RemoveOrder(Order completedOrder) {
        int orderIndex = orderList.IndexOf(completedOrder);
        orderList.Remove(completedOrder);
        orderTimers.RemoveAt(orderIndex);

        if (orderList.Count <= 0) {
            currOrder = null;
        }
        else {
            currOrder = orderList[0];
        }
        ReloadOrderText();
    }

    private void CheckDrinks() {
        DrinkManager dm = this.drinkManager.GetComponent<DrinkManager>();
        ScoreManager sm = this.scoreManager.GetComponent<ScoreManager>();
        drinksOnOrderScene = dm.getStationDrinks("OrderScene");
        for (int i = 0; i < orderList.Count; i++) {
            Order cOrder = orderList[i];
            for (int j = 0; j < drinksOnOrderScene.Count; j++) {
                Drink currDrink = drinksOnOrderScene[j];
                if (cOrder.equalsDrink(currDrink)) {
                     int orderIndex = orderList.IndexOf(cOrder);
                     sm.addScore(cOrder, currDrink, orderTimers[orderIndex]);
                     ordersCompleted += 1;
                     dm.RemoveFromOrder(currDrink);
                     this.RemoveOrder(cOrder);
                     this.ReloadOrderText();

                     // Update Customer's position
                     CustomerManager custManagerScript = customerManager.GetComponent<CustomerManager>();
                     custManagerScript.CheckCustomers(cOrder);
                }
            }
        }
    }

    void ReloadOrderText() {
        Text orderNumTxt = GameObject.Find("Order#UI").GetComponent<Text>();
        orderNumTxt.text = "Order #";
        Text teaBaseTxt = GameObject.Find("TeaBaseListUI").GetComponent<Text>();
        teaBaseTxt.text = "";
        Text milkTxt = GameObject.Find("MilkUI").GetComponent<Text>();
        milkTxt.text = "Milk: ";
        Text ingredientsTxt = GameObject.Find("IngredientsListUI").GetComponent<Text>();
        ingredientsTxt.text = "";
        Text toppingsTxt = GameObject.Find("ToppingsListUI").GetComponent<Text>();
        toppingsTxt.text = "";
        Text ordersCompletedTxt = GameObject.Find("OrdersCompleted").GetComponent<Text>();
        ordersCompletedTxt.text = "Orders Completed: " + ordersCompleted;

        if (currOrder != null) {
            orderNumTxt.text += currOrder.GetOrderNum().ToString();
            teaBaseTxt.text = "- " + currOrder.GetTeaBase();
            if (currOrder.hasMilk()) {
                milkTxt.text += "Yes";
            }
            else {
                milkTxt.text += "No";
            }
            
            foreach (string ing in currOrder.GetIngredients()) {
                ingredientsTxt.text += "- " + ing + "\n" ;
            }

            foreach (string top in currOrder.GetToppings()) {
                toppingsTxt.text += "- " + top + "\n" ;
            }
        }
    }

    // Re-adds all the orders to the 'My Orders' UI between scenes
    void ReAddMyOrdersList() {
        if (orderList.Count > 0) {
            foreach (Order order in orderList) {
                AddOrderButton(order);
            }
        }
    }

    // Controls the visibility of the 'My Orders' UI.
    public void ShowOrderList() {
        if (buttonScrollList.activeSelf) {
            buttonScrollList.SetActive(false);
            myOrdersTabOpen = false;
            print("Setting visibility off");
        } else {
            buttonScrollList.SetActive(true);
            myOrdersTabOpen = true;
            print("Setting visibility on");
        }
    }

    // Upon clicking an order from 'My Orders,' this will search the orderList for an order with the same order# as the
    // button clicked. It will then set that order as the current order.
    public void SearchOrders(Text orderNumText) {
        int orderNum = Convert.ToInt32(orderNumText.text);
        foreach (Order order in orderList) {
            if (order.GetOrderNum() == orderNum) {
                currOrder = order;
                ReloadOrderText();
                print("Current Order displayed");
                return;
            }
        }
    }
}
