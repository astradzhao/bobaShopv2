using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OrderManager : MonoBehaviour
{
    public Button takeOrderBtn;
    public static List<Order> orderList;
    public GameObject drinkManager;
    public static OrderManager singleton;
    public Sprite orderDoneSprite;
    private List<Drink> drinksOnOrderScene;
    private Order currOrder;
    public int totalOrderCount;

    private void Awake() {
        totalOrderCount = 1;
        if (singleton == null)
        {
            DontDestroyOnLoad(gameObject);
            singleton = this;
            orderList = new List<Order>();
            currOrder = null;
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
        drinksOnOrderScene = new List<Drink>();
        drinkManager = GameObject.Find("DrinkManager");
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "OrderScene") {
            takeOrderBtn = GameObject.Find("Take Order").GetComponent<Button>();
            CheckDrinks();
        }
        ReloadOrderText();
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }



    public void AddOrder(Order newOrder) {
        orderList.Add(newOrder);
        this.currOrder = newOrder;
        ReloadOrderText();
        totalOrderCount += 1;
    }

    public void RemoveOrder(Order completedOrder) {
        orderList.Remove(completedOrder);
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
        drinksOnOrderScene = dm.getStationDrinks("OrderScene");
        for (int i = 0; i < orderList.Count; i++) {
            Order cOrder = orderList[i];
            for (int j = 0; j < drinksOnOrderScene.Count; j++) {
                Drink currDrink = drinksOnOrderScene[j];
                if (currOrder.equalsDrink(currDrink)) {
                     takeOrderBtn.image.sprite = orderDoneSprite;
                     dm.RemoveFromOrder(currDrink);
                     this.RemoveOrder(cOrder);
                     this.ReloadOrderText();
                }
            }
        }
    }

    void ReloadOrderText() {
        Text orderNumTxt = GameObject.Find("Order#UI").GetComponent<Text>();
        orderNumTxt.text = "Order #";
        Text teaBaseTxt = GameObject.Find("TeaBaseListUI").GetComponent<Text>();
        teaBaseTxt.text = "";
        Text ingredientsTxt = GameObject.Find("IngredientsListUI").GetComponent<Text>();
        ingredientsTxt.text = "";
        Text toppingsTxt = GameObject.Find("ToppingsListUI").GetComponent<Text>();
        toppingsTxt.text = "";

        if (currOrder != null) {
            orderNumTxt.text += currOrder.GetOrderNum().ToString();
            teaBaseTxt.text = "- " + currOrder.GetTeaBase();
        
            foreach (string ing in currOrder.GetIngredients()) {
                ingredientsTxt.text += "- " + ing + "\n" ;
            }

            foreach (string top in currOrder.GetToppings()) {
                toppingsTxt.text += "- " + top + "\n" ;
            }
        }
    }
}
