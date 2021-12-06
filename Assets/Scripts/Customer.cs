using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer
{
    List<Sprite> orderStatuses;
    private List<Sprite> customerSprites;
    private string pos;
    private int orderNum;
    private bool isOnScene;

    private Sprite currSprite;
    private Sprite currOrderSprite;

    // Upon Completetion of Order
    private Order completedOrder;
    private Drink completedDrink;
    private float timeCompleted;

    public Customer(List<Sprite> customerSprites, List<Sprite> orderStatuses, string pos, int orderNum) {
        this.customerSprites = customerSprites;
        this.orderStatuses = orderStatuses;
        this.pos = pos;
        this.orderNum = orderNum;
        this.currSprite = customerSprites[0];
        this.currOrderSprite = orderStatuses[0];
        this.isOnScene = true;
    }

    public int GetCustOrder() {
        return this.orderNum;
    }

    public string GetCustPos() {
        return this.pos;
    }

    public Sprite GetCurrSprite() {
        return this.currSprite;
    }

    public bool CheckOnScene() {
        return this.isOnScene;
    }

    public void setIsOnScene(bool onScene) {
        this.isOnScene = onScene;
    }

    public bool isMyDrink(Order order) {
        return this.orderNum == order.GetOrderNum();
    }

    public void setCustPos(string newPos) {
        this.pos = newPos;
    }

    public void setSpriteToOrderDone() {
        this.currSprite = customerSprites[1];
    }

    public void SetCompletedDrink(Order order, Drink drink, float time) {
        this.completedOrder = order;
        this.completedDrink = drink;
        this.timeCompleted = time;
    }

    public Order GetCompletedOrder() {
        return this.completedOrder;
    }

    public Drink GetCompletedDrink() {
        return this.completedDrink;
    }

    public float GetTimeCompleted() {
        return this.timeCompleted;
    }
}
