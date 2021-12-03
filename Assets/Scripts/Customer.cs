using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    // All Different sprites for customers
    public List<Sprite> allCustomersIdle = new List<Sprite>();
    public List<Sprite> allCustomersAngry = new List<Sprite>();
    public List<Sprite> allCustomersOrderDone = new List<Sprite>();
    public List<Sprite> allOrderStatuses = new List<Sprite>();

    public GameObject customerPrefab;

    // The current sprite of the customer's order button
    private Sprite currOrderStatSprite;

    // The current sprite of the customer
    private Sprite currSprite;

    // The customer's sprites
    private Sprite idleSprite;
    private Sprite angrySprite;
    private Sprite orderDoneSprite;

    // The customer's order number
    private int custOrderNum;
    // The status of the customer's order
    private int custOrderStatus;
    //The position of the customer on the order scene
    private int custPos;

    // The game object of the customer reflecting all it's states.
    private GameObject customerObj;

    public Customer(int custOrderNum, int custPos) {
        this.custOrderNum = custOrderNum;
        this.custPos = custPos;
        this.custOrderStatus = 0;
        this.currOrderStatSprite = allOrderStatuses[0];
        ChooseCustomerSprite();
        this.currSprite = this.idleSprite;
    }

    private void ChooseCustomerSprite() {
        int spriteNum = Random.Range(0, allCustomersIdle.Count);
        this.idleSprite = allCustomersIdle[spriteNum];
        this.angrySprite = allCustomersAngry[spriteNum];
        this.orderDoneSprite = allCustomersOrderDone[spriteNum];
    }

    private void ChangeSprite(string state) {
        if (state == "angry") {
            this.currSprite = this.angrySprite;
        } else if (state == "orderDone") {
            this.currSprite = this.orderDoneSprite;
            this.currOrderStatSprite = allOrderStatuses[2];
        } else {
            this.currSprite = this.idleSprite;
        }
    }

    //public GetGameObject


    public int GetCustOrder() {
        return this.custOrderNum;
    }

    public int GetCustPos() {
        return this.custPos;
    }

    public int GetOrderStatus() {
        return this.custOrderStatus;
    }








}
