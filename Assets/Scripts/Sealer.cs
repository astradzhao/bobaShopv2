using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Sealer : MonoBehaviour
{
    public float delta = 125.0f;   // Amount to move left and right from the start point
    public float speed = 2.0f; 
    private bool isSealed;
    private Vector3 startPos;
    public Button sealButton;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        Console.WriteLine(transform.position);
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
        v.y -= 5.5f;
        transform.position = v;
        sealButton.interactable = false;  // prevent user from sealing multiple times
        // middle position v.x = 257.0f
        // ratio 0.8f comes from max points / delta
        Score.scoreValue = (int) (100 - (Math.Abs(startPos.x - v.x) * 0.8f));
    }
}
