using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMove : MonoBehaviour
{
    public Vector3 moveAxis;
    public float moveSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.TransformDirection(moveAxis * moveSpeed * Time.deltaTime);
    }
}

