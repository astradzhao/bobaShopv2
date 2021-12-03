using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyClass: MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}