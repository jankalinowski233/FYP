using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Simple move
public class SimpleMove : MonoBehaviour
{
    public Vector3 moveAxis;
    public float moveSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.TransformDirection(moveAxis * moveSpeed * Time.deltaTime); // Move along an axis with a given speed
                                                                                                // Either positive, or negative
    }
}

