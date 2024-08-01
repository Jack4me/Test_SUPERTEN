using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCoin : MonoBehaviour
{
    public float rotationSpeed = 100f; 

    void Update()
    {
       
        transform.Rotate(rotationSpeed * Time.deltaTime, 0, 0);
    }
}
