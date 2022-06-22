using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBox : MonoBehaviour
{
    public GameObject child;
    
    void Start()
    {
        child = transform.GetChild(0).gameObject;
    }
}
