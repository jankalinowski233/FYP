using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Base player move script
public class PlayerMove : MonoBehaviour
{
    // Properties
    public Vector3 axis;
    public float moveSpeed;

    protected Animator anim;
    protected Rigidbody rb;

    // In-game texts
    public Text idleText;
    public Text moveText;

    protected virtual void Start()
    {
        // Init
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    
    protected virtual void Update()
    {
        // Move along an axis
        Vector3 temp = transform.position;

        if(axis.x > 0)
            temp.x += axis.x * moveSpeed * Time.deltaTime;
        else if(axis.y > 0)
            temp.y += axis.y * moveSpeed * Time.deltaTime;
        else if(axis.z > 0)
            temp.z += axis.z * moveSpeed * Time.deltaTime;

        transform.position = temp;

        // Play run animation
        anim.SetFloat("speed", moveSpeed);

        // Flash text based on in-game events
        if(moveSpeed > 0)
        {
            idleText.color = new Color(idleText.color.r, idleText.color.g, idleText.color.b, 0.2f);
            moveText.color = new Color(moveText.color.r, moveText.color.g, moveText.color.b, 1.0f);
        }
        else
        {
            idleText.color = new Color(idleText.color.r, idleText.color.g, idleText.color.b, 1.0f);
            moveText.color = new Color(moveText.color.r, moveText.color.g, moveText.color.b, 0.2f);
        }
    }
}