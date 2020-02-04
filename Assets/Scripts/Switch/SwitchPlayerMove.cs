using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovePlayer : MonoBehaviour
{
    public abstract float MoveTo(GameObject t);
}

public class SwitchPlayerMove : MonoBehaviour
{
    public float moveSpeed;
    public Vector3 axis;

    Animator anim;
    Rigidbody rb;

    GameObject target;
    MovePlayer move;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        target = GameObject.FindGameObjectWithTag("Target");
    }

    public void Attach()
    {
        move = GetComponent<MovePlayer>();
    }

    void Update()
    {
        if(move != null)
        {
            moveSpeed = move.MoveTo(target);

            Vector3 temp = transform.position;

            if (axis.x > 0)
                temp.x += axis.x * moveSpeed * Time.deltaTime;
            else if (axis.y > 0)
                temp.y += axis.y * moveSpeed * Time.deltaTime;
            else if (axis.z > 0)
                temp.z += axis.z * moveSpeed * Time.deltaTime;

            transform.position = temp;
            transform.LookAt(target.transform.position);
            anim.SetFloat("speed", moveSpeed);
        }
            
    }
}
