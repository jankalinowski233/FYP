using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    public Text idle;
    public Text running;

    public PlayerMove move;

    // Update is called once per frame
    void Update()
    {
        if(move != null)
        {
            if(move.moveSpeed > 0)
            {
                idle.color = new Color(idle.color.r, idle.color.g, idle.color.b, 0.2f);
                running.color = new Color(running.color.r, running.color.g, running.color.b, 1.0f);
            }
            else
            {
                idle.color = new Color(idle.color.r, idle.color.g, idle.color.b, 1.0f);
                running.color = new Color(running.color.r, running.color.g, running.color.b, 0.2f);
            }
        }
    }

    public void DisableMove()
    {
        move.moveSpeed = 0.0f;
    }
}
