using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Simple rotation
public class SimpleRotator : MonoBehaviour
{
    public Vector3 rotationAxis;
    public float rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime); // Rotate around given axis with given speed
    }

}
