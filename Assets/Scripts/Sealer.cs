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
    
    private Image sealerImg;
    private Image sealerImgClosed;
    private Image lidImgClosed;
    private Image lidImg;

    public float sealerSwitchDelay;
    private bool touchingSealer;

    // Start is called before the first frame update
    void Start()
    {
        lidImg = this.GetComponent<Image>();
        sealerImg = GameObject.Find("Sealer").GetComponent<Image>();
        sealerImgClosed = GameObject.Find("SealerClosed").GetComponent<Image>();
        sealerImg.enabled = true;
        sealerImgClosed.enabled = false;
        touchingSealer = false;
        lidImgClosed = GameObject.Find("LidClosedPosition").GetComponent<Image>();
        lidImgClosed.enabled = false;
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
        //Debug.Log(touchingSealer);
    }

    public void SealDrink()
    {
        isSealed = true;  // stop the seal from automatically moving 
        Vector3 v = transform.position;
        if (touchingSealer) {
            lidImg.enabled = false; 
            lidImgClosed.enabled = true;
        }
        sealButton.interactable = false;  // prevent user from sealing multiple times
        sealerImg.enabled = false;
        sealerImgClosed.enabled = true;
        StartCoroutine("SwitchAgain");
        // middle position v.x = 257.0f
        // ratio 0.8f comes from max points / delta 
        DrinkManager dm = drinkManager.GetComponent<DrinkManager>();
        Drink currentDrink = dm.getStationDrinks("SealingScene")[0];
        int sealScore = (int) (100 - (Math.Abs(startPos.x - v.x) * 100 / delta));
        sealScore *= 5;
        currentDrink.seal(sealScore);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "sealer") {
            touchingSealer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "sealer") {
            touchingSealer = false;
        }
    }

    IEnumerator SwitchAgain() {
        yield return new WaitForSeconds(sealerSwitchDelay);
        sealerImgClosed.enabled = false;
        sealerImg.enabled = true;
    }
}
