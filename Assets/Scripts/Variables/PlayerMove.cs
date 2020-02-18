using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Vector3 axis;
    public float moveSpeed;

    protected Animator anim;
    protected Rigidbody rb;


    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    
    public virtual void Attach()
    {

    }

    protected virtual void Update()
    {
        Vector3 temp = transform.position;

        if(axis.x > 0)
            temp.x += axis.x * moveSpeed * Time.deltaTime;
        else if(axis.y > 0)
            temp.y += axis.y * moveSpeed * Time.deltaTime;
        else if(axis.z > 0)
            temp.z += axis.z * moveSpeed * Time.deltaTime;

        transform.position = temp;

        anim.SetFloat("speed", moveSpeed);
    }
}