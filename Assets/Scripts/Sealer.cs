using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Sealer : MonoBehaviour
{
    // Canvas canvas = FindObjectOfType<Canvas>();
    // private float height = canvas.GetComponent<RectTransform>().rect.height;
    // private float width = canvas.GetComponent<RectTransform>().rect.width;
    public float speed = 2.0f; 
    public float delta;   // Amount to move left and right from the start point
    private bool isSealed;
    private Vector3 canvasPos;
    private Vector3 startPos;
    public Button sealButton;
    private GameObject drinkManager;
    
    private GameObject sealer;
    private GameObject sealerClosed;

    public float sealerDropDist;
    public float sealerSwitchDelay1;
    public float sealerSwitchDelay2;

    // Start is called before the first frame update
    void Start()
    {
        sealer = GameObject.Find("Sealer");
        sealerClosed = GameObject.Find("SealerClosed");
        sealerClosed.SetActive(false);

        drinkManager = GameObject.Find("DrinkManager");
        GameObject canvas = GameObject.Find("Canvas");
        canvasPos = canvas.transform.position;
        startPos = transform.position;

        delta = canvasPos.x / 2;

        Button btn = sealButton.GetComponent<Button>();
        btn.onClick.AddListener(SealDrink);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSealed) 
        {
            Vector3 v = startPos;
            v.x += (delta * Mathf.Sin(Time.time * speed));
            transform.position = v; 
        } 
    }

    public void SealDrink()
    {
        isSealed = true;  // stop the seal from automatically moving 
        Vector3 v = transform.position;  
        v.y -= sealerDropDist;
        transform.position = v;
        sealButton.interactable = false;  // prevent user from sealing multiple times
        Invoke("SwitchSealers", sealerSwitchDelay1);
        Invoke("SwitchSealers", sealerSwitchDelay2);
        // middle position v.x = 257.0f
        // ratio 0.8f comes from max points / delta 
        DrinkManager dm = drinkManager.GetComponent<DrinkManager>();
        Drink currentDrink = dm.getStationDrinks("SealingScene")[0];
        int sealScore = (int) (100 - (Math.Abs(startPos.x - v.x) * 100 / delta));
        sealScore *= 5;
        currentDrink.seal(sealScore);
    }

    private void SwitchSealers() {
        if (sealer.activeSelf) {
            sealer.SetActive(false);
            sealerClosed.SetActive(true);
        } else {
            sealerClosed.SetActive(false);
            sealer.SetActive(true);
        }
    }
}
