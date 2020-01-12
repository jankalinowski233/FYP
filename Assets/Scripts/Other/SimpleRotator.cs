﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotator : MonoBehaviour
{
    public Vector3 rotationAxis;
    public float rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = transform.eulerAngles + rotationAxis * rotationSpeed * Time.deltaTime;
    }

}
