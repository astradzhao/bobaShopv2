using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugging : MonoBehaviour
{
     public Transform someVariable;
 
 void Start()
 {
     if(someVariable == null)
         Debug.LogError("SomeVariable has not been assigned.", this)    ;
     // Notice, that we pass 'this' as a context object so that Unity will highlight this object when clicked.
 }
}
