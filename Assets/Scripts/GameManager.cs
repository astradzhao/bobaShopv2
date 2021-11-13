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
    public void moveDrinkTo(string s) {
        if (s == "IngredientsScene") {
            DrinkManager dm = drinkManager.GetComponent<DrinkManager>();
            dm.BaseToIng();
        }
        else if (s == "MixingScene") {
            DrinkManager dm = drinkManager.GetComponent<DrinkManager>();
            dm.IngToMix();
        }
        else if (s == "SealingScene1") {
            DrinkManager dm = drinkManager.GetComponent<DrinkManager>();
            dm.Mix1ToSeal();
        }
        else if (s == "SealingScene2") {
            DrinkManager dm = drinkManager.GetComponent<DrinkManager>();
            dm.Mix2ToSeal();
        }
        else if (s == "SealingScene3") {
            DrinkManager dm = drinkManager.GetComponent<DrinkManager>();
            dm.Mix3ToSeal();
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
        mm.reset(m);
    }

    #endregion

    #region Sealing Functions

    #endregion
}
