using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DrinkManager : MonoBehaviour
{
    public static DrinkManager singleton;
    public GameObject myDrink;
    public GameObject currObject;
    public Canvas canvas;
    private static Dictionary<string, List<Drink>> drinks;

    private void Awake() {
        if (singleton == null)
        {
            DontDestroyOnLoad(gameObject);
            singleton = this;
            drinks = new Dictionary<string, List<Drink>>();
            drinks.Add("OrderScene", new List<Drink>());
            drinks.Add("TeaBaseScene", new List<Drink>());
            drinks.Add("IngredientsScene", new List<Drink>());
            drinks.Add("MixingScene", new List<Drink>());
            drinks.Add("MixingScene1", new List<Drink>());
            drinks.Add("MixingScene2", new List<Drink>());
            drinks.Add("MixingScene3", new List<Drink>());
            drinks.Add("SealingScene", new List<Drink>());
            newDrink();
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
        newDrink();
        if (scene.name != "OrderScene") {
            reloadObject();
        }
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void reloadObject() {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "MixingScene") {
            if (drinks["MixingScene"].Count == 0 && drinks["MixingScene1"].Count == 0 && drinks["MixingScene2"].Count == 0 && drinks["MixingScene3"].Count == 0) {
                return;
            }
            else {
                if (drinks["MixingScene1"].Count != 0) {
                    Drink sceneD = drinks["MixingScene1"][0];
                    DrinkComponent d = myDrink.GetComponent<DrinkComponent>();
                    canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
                    d.drink = sceneD;
                    RectTransform r = canvas.GetComponent<RectTransform>();
                    CanvasScaler canvasScale = canvas.GetComponent<CanvasScaler>();
                    float x = r.position.x;
                    float y = r.position.y;
                    Vector2 res = canvasScale.referenceResolution;
                    float h = res.y;
                    float w = res.x;
                    currObject = Instantiate(myDrink, new Vector3(x - 7 * w / 5, y - 3 * h / 4, 0), Quaternion.identity);
                    RectTransform dR = currObject.GetComponent<RectTransform>();
                    dR.sizeDelta = new Vector3(w / 2, 5 * h / 4, 0);
                    currObject.transform.SetParent(canvas.transform);
                    reloadText(d.drink, currObject);
                }

                if (drinks["MixingScene2"].Count != 0) {
                    Drink sceneD = drinks["MixingScene2"][0];
                    DrinkComponent d = myDrink.GetComponent<DrinkComponent>();
                    canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
                    d.drink = sceneD;
                    RectTransform r = canvas.GetComponent<RectTransform>();
                    CanvasScaler canvasScale = canvas.GetComponent<CanvasScaler>();
                    float x = r.position.x;
                    float y = r.position.y;
                    Vector2 res = canvasScale.referenceResolution;
                    float h = res.y;
                    float w = res.x;
                    currObject = Instantiate(myDrink, new Vector3(x - 2 * w / 5, y - 3 * h / 4, 0), Quaternion.identity);
                    RectTransform dR = currObject.GetComponent<RectTransform>();
                    dR.sizeDelta = new Vector3(w / 2, 5 * h / 4, 0);
                    currObject.transform.SetParent(canvas.transform);
                    reloadText(d.drink, currObject);
                }

                if (drinks["MixingScene3"].Count != 0) {
                    Drink sceneD = drinks["MixingScene3"][0];
                    DrinkComponent d = myDrink.GetComponent<DrinkComponent>();
                    canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
                    d.drink = sceneD;
                    RectTransform r = canvas.GetComponent<RectTransform>();
                    CanvasScaler canvasScale = canvas.GetComponent<CanvasScaler>();
                    float x = r.position.x;
                    float y = r.position.y;
                    Vector2 res = canvasScale.referenceResolution;
                    float h = res.y;
                    float w = res.x;
                    currObject = Instantiate(myDrink, new Vector3(x + 3 * w / 5, y - 3 * h / 4, 0), Quaternion.identity);
                    RectTransform dR = currObject.GetComponent<RectTransform>();
                    dR.sizeDelta = new Vector3(w / 2, 5 * h / 4, 0);
                    currObject.transform.SetParent(canvas.transform);
                    reloadText(d.drink, currObject);
                }
            }
        }
        else {
            if (drinks[sceneName].Count == 0) {
                return;
            }
            else {
                Drink sceneD = drinks[sceneName][0];
                DrinkComponent d = myDrink.GetComponent<DrinkComponent>();
                canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
                d.drink = sceneD;
                RectTransform r = canvas.GetComponent<RectTransform>();
                CanvasScaler canvasScale = canvas.GetComponent<CanvasScaler>();
                float x = r.position.x;
                float y = r.position.y;
                Vector2 res = canvasScale.referenceResolution;
                float h = res.y;
                float w = res.x;
                currObject = Instantiate(myDrink, new Vector3(x - 2 * w / 5, y - 9 * h / 8, 0), Quaternion.identity);
                RectTransform dR = currObject.GetComponent<RectTransform>();
                dR.sizeDelta = new Vector3(w / 2, 5 * h / 4, 0);
                currObject.transform.SetParent(canvas.transform);
                reloadText(d.drink, currObject);
            }
        }
        
    }

    public void reloadText(Drink d, GameObject o) {
        Text teaBaseTxt = o.transform.Find("BaseText").GetComponent<Text>();
        if (d.getBase() == null) {
            teaBaseTxt.text = "";
        }
        else {
            teaBaseTxt.text = d.getBase();
        }

        Text ingredientsTxt = o.transform.Find("IngredientsText").GetComponent<Text>();
        ingredientsTxt.text = d.getIngText();

        Text toppingsTxt = o.transform.Find("ToppingsText").GetComponent<Text>();
        toppingsTxt.text = d.getTopText();
    }
    public void addDrink(Drink d, string station) {
        drinks[station].Add(d);
    }

    public void newDrink() {
        if (drinks["TeaBaseScene"].Count <= 0) {
            Drink d = new Drink();
            drinks["TeaBaseScene"].Add(d);
        }
    }

    public List<Drink> getStationDrinks(string station) {
        return drinks[station];
    }

    public void addIngredient(string i) {
        if (myDrink != null) {
            DrinkComponent d = myDrink.GetComponent<DrinkComponent>();
            if (d != null) {
                d.drink.addIngredient(i);
                AddIngredientText(i, d.drink);
            }
        }
        else {
            AddIngredientText(i, null);
        }
    }

    public void addToppings(string t) {
        if (myDrink != null) {
            DrinkComponent d = myDrink.GetComponent<DrinkComponent>();
            if (d != null) {
                d.drink.addToppings(t);
                AddToppingText(t, d.drink);
            }
        }
        else {
            AddToppingText(t, null);
        }
    }

    public void changeTeaBase(string b) {
        if (myDrink != null) {
            DrinkComponent d = myDrink.GetComponent<DrinkComponent>();
            d.drink.changeTeaBase(b);
        }
        SetTeaBaseText(b);
    }

    public void SetTeaBaseText(string b) {
        Text teaBaseTxt = GameObject.Find("BaseText").GetComponent<Text>();
        if (teaBaseTxt != null) {
            if (teaBaseTxt.text == "") {
            teaBaseTxt.text = b;
            }
        }
    }

    public void AddIngredientText(string b, Drink d) {
        GameObject ing = GameObject.Find("IngredientsText");
        if (ing == null) {
            return;
        }
        Text ingredientsTxt = ing.GetComponent<Text>();
        if (ingredientsTxt == null) {
            return;
        }
        if (d == null) {
            ingredientsTxt.text = "";
        }
        else {
            ingredientsTxt.text = d.getIngText();
        }
    }

    public void AddToppingText(string b, Drink d) {
        GameObject top = GameObject.Find("ToppingsText");
        if (top == null) {
            return;
        }
        Text toppingsTxt = top.GetComponent<Text>();
        if (toppingsTxt == null) {
            return;
        }
        if (d == null) {
            toppingsTxt.text = "";
        }
        else {
            toppingsTxt.text = d.getTopText();
        }
    }

    public void Trash() {
        string sceneName = SceneManager.GetActiveScene().name;
        if (drinks[sceneName].Count >= 1) {
            drinks[sceneName].RemoveAt(0);
        }
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

    public void RemoveFromOrder(Drink d) {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName != "OrderScene") {
            return;
        }
        drinks[sceneName].Remove(d);
    }

    
    #region Moving Drinks
    public void BaseToIng() {
        List<Drink> baseDrinks = drinks["TeaBaseScene"];
        if (baseDrinks.Count <= 0) {
            return;
        }
        Drink d = baseDrinks[0]; 
        baseDrinks.RemoveAt(0);
        drinks["IngredientsScene"].Add(d);
    }

    public void IngToMix() {
        List<Drink> ingDrinks = drinks["IngredientsScene"];
        if (ingDrinks.Count <= 0) {
            return;
        }
        Drink d = ingDrinks[0]; 
        ingDrinks.RemoveAt(0);
        List<Drink> mixDrinks1 = drinks["MixingScene1"];
        List<Drink> mixDrinks2 = drinks["MixingScene2"];
        List<Drink> mixDrinks3 = drinks["MixingScene3"];
        if (mixDrinks1.Count <= 0) {
            drinks["MixingScene1"].Add(d);
        }
        else if (mixDrinks2.Count <= 0) {
            drinks["MixingScene2"].Add(d);
        }
        else if (mixDrinks3.Count <= 0) {
            drinks["MixingScene3"].Add(d);
        }
        else {
            drinks["MixingScene"].Add(d);
        }
    }

    public void Mix1ToSeal() {
        List<Drink> mixDrinks = drinks["MixingScene1"];
        if (mixDrinks.Count <= 0) {
            return;
        }
        Drink d = mixDrinks[0]; 
        mixDrinks.RemoveAt(0);
        drinks["SealingScene"].Add(d);
        List<Drink> queuedDrinks = drinks["MixingScene"];
        if (queuedDrinks.Count > 0) {
            Drink firstD = queuedDrinks[0];
            queuedDrinks.RemoveAt(0);
            drinks["MixingScene1"].Add(firstD);
        }
    }

    public void Mix2ToSeal() {
        List<Drink> mixDrinks = drinks["MixingScene2"];
        if (mixDrinks.Count <= 0) {
            return;
        }
        Drink d = mixDrinks[0]; 
        mixDrinks.RemoveAt(0);
        drinks["SealingScene"].Add(d);
        List<Drink> queuedDrinks = drinks["MixingScene"];
        if (queuedDrinks.Count > 0) {
            Drink firstD = queuedDrinks[0];
            queuedDrinks.RemoveAt(0);
            drinks["MixingScene2"].Add(firstD);
        }
    }

    public void Mix3ToSeal() {
        List<Drink> mixDrinks = drinks["MixingScene3"];
        if (mixDrinks.Count <= 0) {
            return;
        }
        Drink d = mixDrinks[0]; 
        mixDrinks.RemoveAt(0);
        drinks["SealingScene"].Add(d);
        List<Drink> queuedDrinks = drinks["MixingScene"];
        if (queuedDrinks.Count > 0) {
            Drink firstD = queuedDrinks[0];
            queuedDrinks.RemoveAt(0);
            drinks["MixingScene3"].Add(firstD);
        }
    }

    public void SealToOrder() {
        List<Drink> sealDrinks = drinks["SealingScene"];
        if (sealDrinks.Count <= 0) {
            return;
        }
        Drink d = sealDrinks[0]; 
        sealDrinks.RemoveAt(0);
        drinks["OrderScene"].Add(d);
    }
    #endregion
}
