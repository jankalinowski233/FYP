using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed;

    void Update()
    {
        transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
    }
}