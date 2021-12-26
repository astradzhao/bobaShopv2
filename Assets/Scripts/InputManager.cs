using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public float disableDuration;
    public float lastOrderTakened;
    public GameObject[] orderBubbles;

    private Button lastOrderBttnPressed;

    void Start() {
        orderBubbles = GameObject.FindGameObjectsWithTag("orderBubble");
    }

    void Update()
    {
        orderBubbles = GameObject.FindGameObjectsWithTag("orderBubble");

        foreach (GameObject orderBubble in orderBubbles)
        {
            Button orderBttn = orderBubble.GetComponent<Button>();
            if (Time.time > lastOrderTakened + disableDuration && orderBttn != lastOrderBttnPressed) {
                orderBttn.interactable = true;
            } else {
                orderBttn.interactable = false;
            }
        }
    }

    public void UpdateLastOrder(Button latestBttn) {
        lastOrderBttnPressed = latestBttn;
        lastOrderTakened = Time.time;
    }

}
