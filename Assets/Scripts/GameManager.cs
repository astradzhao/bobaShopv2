using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject drinkManager;
    private GameObject orderManager;
    void Start()
    {
        drinkManager = GameObject.Find("DrinkManager");
        orderManager = GameObject.Find("OrderManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Drink functions
    public void addToppings(string s) {
        DrinkManager dm = drinkManager.GetComponent<DrinkManager>();
        dm.addToppings(s);
    }
    public void addIngredient(string s) {
        DrinkManager dm = drinkManager.GetComponent<DrinkManager>();
        dm.addIngredient(s);
    }
    public void changeTeaBase(string s) {
        DrinkManager dm = drinkManager.GetComponent<DrinkManager>();
        dm.changeTeaBase(s);
    }
    public void moveDrinkTo(string s) {
        if (s == "IngredientsScene") {
            DrinkManager dm = drinkManager.GetComponent<DrinkManager>();
            dm.BaseToIng();
        }
        else if (s == "SealingScene") {
            DrinkManager dm = drinkManager.GetComponent<DrinkManager>();
            dm.IngToSeal();
        }
        else if (s == "OrderScene") {
            DrinkManager dm = drinkManager.GetComponent<DrinkManager>();
            dm.SealToOrder();
        }
    }

    public void trashCan() {
        DrinkManager dm = drinkManager.GetComponent<DrinkManager>();
        dm.Trash();
    }
    #endregion

    #region Sealing Functions

    #endregion
}
