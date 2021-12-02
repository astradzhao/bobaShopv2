using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    // #region Public Variables
    // public Button takeOrderBtn;
    // public GameObject orderManager;
    // public Sprite orderAlertSprite;
    // public Sprite orderTakingSprite;
    // public Sprite orderDoneSprite;
    // public Text orderNumTxt;
    // public Text teaBaseTxt;
    // public Text ingredientsTxt;
    // public Text toppingsTxt;
    // #endregion

    // #region Private Variables
    // private Sprite currOrderSprite;
    // private OrderManager orderManagerScript;
    // #endregion

    // void Start() {
    //     currOrderSprite = takeOrderBtn.image.sprite;
    //     orderManager = GameObject.Find("OrderManager");
    //     orderManagerScript = orderManager.GetComponent<OrderManager>(); 
    // }

    // void Update() {
    // }
    
	// public void DoSomething () {
    //     if (currOrderSprite == orderAlertSprite) {
    //         DisplayOrder();
    //         takeOrderBtn.image.sprite = orderTakingSprite;
    //     } else if (currOrderSprite == orderDoneSprite) {
    //         print("Order Complete!");
    //         takeOrderBtn.image.sprite = orderAlertSprite;
    //     }
    //     currOrderSprite = takeOrderBtn.image.sprite;

	// }

	// void DisplayOrder() {
	// 	Order newOrder = new Order(orderManagerScript.totalOrderCount);
    //     orderManagerScript.AddOrder(newOrder);
    //     //takeOrderBtn.
	// }

    // void SetOrderText(Order order) {
    //     orderNumTxt.text = "Order #" + order.GetOrderNum().ToString();
    //     teaBaseTxt.text = "";
    //     ingredientsTxt.text = "";
    //     toppingsTxt.text = "";

    //     teaBaseTxt.text = "- " + order.GetTeaBase();
        
    //     foreach (string ing in order.GetIngredients()) {
    //         ingredientsTxt.text += "- " + ing + "\n" ;
    //     }

    //     foreach (string top in order.GetToppings()) {
    //         toppingsTxt.text += "- " + top + "\n" ;
    //     }
    // }

}
