using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order
{
    #region Private Class Variables
    private static List<string> allTeaBases = new List<string>{"Oolong", "Green", "Black", "Rose", "Herbal", "Thai"};
    private static List<string> allIngredients = new List<string>{"Mango", "Taro", "Strawberry", "Matcha", "Honeydew", "Brown Sugar",
        "Ginger", "Lychee"};
    private static List<string> allToppings = new List<string>{"Lychee Jelly", "Tapioca Pearls", "Strawberry Stars", "Mango Pudding", "Crystal Boba", "Pineapple Jelly",
        "Aloe", "Red Bean", "Coconut Jelly", "Custard Pudding"};
    #endregion

    #region Private Instance Variables
    private int numIngredients;
    private int milk;
    private int numToppings;
    private int orderNum;
    private string teaBase;
    private List<string> ingredients;
    private List<string> toppings;
    #endregion

    #region Start Functions
    private void Start() {
    }
    #endregion

    #region Instantiation
    // Instantiate a new order
    public Order(int num) {
        this.orderNum = num;
        this.teaBase = allTeaBases[Random.Range(0, allTeaBases.Count)];
        this.ingredients = new List<string>();
        this.toppings = new List<string>();
        // 0 = doesn't have milk, 1 = has milk
        this.milk = Random.Range(0, 2);

        // Number of Ingredients and toppings. Max for both is set to 2
        numIngredients = Random.Range(1, 4);
        numToppings = Random.Range(1, 5);

        // Assign ingredients
        while (this.ingredients.Count < numIngredients) {
            string newIng = allIngredients[Random.Range(0, allTeaBases.Count)];
            if (!this.ingredients.Contains(newIng)) {
                this.ingredients.Add(newIng);
            }
        }

        // Assign toppings
        while (this.toppings.Count < numToppings) {
            string newTop = allToppings[Random.Range(0, allToppings.Count)];
            if (!this.toppings.Contains(newTop)) {
                this.toppings.Add(newTop);
            }
        }
    }
    #endregion

    #region Get Functions
    public int GetOrderNum() {
        return this.orderNum;
    }

    public string GetTeaBase() {
        return this.teaBase;
    }

    public List<string> GetIngredients() {
        return this.ingredients;
    }

    public List<string> GetToppings() {
        return this.toppings;
    }
    #endregion

    public bool equalsDrink(Drink d) {
        return this.GetHashCode() == d.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        Order o = obj as Order;
        if (o == null) {
            return false;
        }
        return this.GetHashCode() == o.GetHashCode();
    }

    public bool hasMilk() {
        return this.milk == 1;
    }

    public override int GetHashCode()
    {
        int hash = 0;
        for (int i = 0; i < this.toppings.Count; i++) {
            hash += this.toppings[i].GetHashCode() * 11;
        }
        for (int i = 0; i < this.ingredients.Count; i++) {
            hash += this.ingredients[i].GetHashCode() * 17;
        }
        return hash + this.teaBase.GetHashCode() * 13 + this.milk * 71;
    }
}
