using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Vector3 axis;
    public float moveSpeed;

    bool transitioned = false;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        transform.Translate(axis * moveSpeed * Time.deltaTime);

        if(transitioned == false)
            Transition();
    }

    void Transition()
    {
        anim.SetFloat("speed", moveSpeed);
    }
}