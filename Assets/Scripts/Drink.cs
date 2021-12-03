using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink
{
    #region Instance Variables
    private string teaBase;
    private List<string> ingredients;
    private List<string> toppings;
    private string ingredientsText;
    private string toppingsText;
    private int sealingScore;
    private int mixingScore;
    private int milk;
    #endregion 

    #region Instantiation
    public Drink() {
        this.teaBase = null;
        this.ingredients = new List<string>();
        this.toppings = new List<string>();
        this.ingredientsText = "";
        this.toppingsText = "";
        this.sealingScore = 0;
        this.mixingScore = 0;
        this.milk = 0;
    }
    #endregion
    
    #region GameFunctions
    public void changeTeaBase(string b) {
        if(this.teaBase == null) {
            this.teaBase = b;
        }
    }

    public void addIngredient(string i) {
        this.ingredients.Add(i);
        if (this.ingredientsText == "") {
            this.ingredientsText += i;
        }
        else {
            this.ingredientsText += ", " + i;
        }
    }

    public void addToppings(string t) {
        this.toppings.Add(t);
        if (this.toppingsText == "") {
            this.toppingsText += t;
        }
        else {
            this.toppingsText += ", " + t;
        }
    }

    public void addMilk() {
        this.milk = 1;
    }
    #endregion

    #region GetFunctions
    public string getBase() {
        return this.teaBase;
    }
    public List<string> getIngredients() {
        return this.ingredients;
    }

    public List<string> getToppings() {
        return this.toppings;
    }

    public string getIngText() {
        return this.ingredientsText;
    }

    public string getTopText() {
        return this.toppingsText;
    }

    public string getMilkText() {
        if (this.milk == 0) {
            return "";
        }
        return "Milk";
    }
    #endregion

    public override bool Equals(object obj)
    {
        Drink o = obj as Drink;
        if (o == null) {
            return false;
        }
        return this.GetHashCode() == o.GetHashCode();
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

    public void seal(int s) {
        this.sealingScore = s;
    }
    
    public void mix(int s) {
        this.mixingScore = s;
    }

    public int getSealScore() {
        return sealingScore;
    }

    public int getMixScore() {
        return mixingScore;
    }
}
