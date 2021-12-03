using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject drinkManager;
    private GameObject orderManager;
    private GameObject mixingManager;
    void Start()
    {
        drinkManager = GameObject.Find("DrinkManager");
        orderManager = GameObject.Find("OrderManager");
        mixingManager = GameObject.Find("MixerManager");
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
    public void addMilk() {
        DrinkManager dm = drinkManager.GetComponent<DrinkManager>();
        dm.addMilk();
    }
    public void moveDrinkTo(string s) {
        if (s == "IngredientsScene") {
            DrinkManager dm = drinkManager.GetComponent<DrinkManager>();
            dm.BaseToIng();
        }
        else if (s == "MixingScene") {
            DrinkManager dm = drinkManager.GetComponent<DrinkManager>();
            dm.IngToMix();
        }
        else if (s == "ToppingsScene1") {
            DrinkManager dm = drinkManager.GetComponent<DrinkManager>();
            dm.Mix1ToTop();
        }
        else if (s == "ToppingsScene2") {
            DrinkManager dm = drinkManager.GetComponent<DrinkManager>();
            dm.Mix2ToTop();
        }
        else if (s == "ToppingsScene3") {
            DrinkManager dm = drinkManager.GetComponent<DrinkManager>();
            dm.Mix3ToTop();
        }
        else if (s == "SealingScene") {
            DrinkManager dm = drinkManager.GetComponent<DrinkManager>();
            dm.TopToSeal();
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

    #region Mixing Functions
    public void startMixer(int m) {
        MixingManager mm = mixingManager.GetComponent<MixingManager>();
        mm.startMixer(m);
    }
    public void stopMixer(int m) {
        MixingManager mm = mixingManager.GetComponent<MixingManager>();
        mm.stopMixer(m);
    }

    public void resetMixer(int m) {
        MixingManager mm = mixingManager.GetComponent<MixingManager>();
        mm.addScore(m);
        mm.reset(m);
    }

    #endregion

    #region Sealing Functions

    #endregion
}
