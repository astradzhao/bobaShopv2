using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkComponent : MonoBehaviour
{
    public Drink drink { get; set; }    
    public DrinkComponent() {
        drink = null;
    }
}
