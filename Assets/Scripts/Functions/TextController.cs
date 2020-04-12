using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    // Texts
    public Text idle;
    public Text running;

    // Reference to a script written by users of the game
    public PlayerMove move;

    // Update is called once per frame
    void Update()
    {
        // If the script exists
        if(move != null)
        {
            // Change texts colors based on in-game events
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

    // Stop moving
    // It may seem a bit unreasonable to leave it here, like - what kind of association do those scripts have?
    // The point is, though, that the system works because of various tricks and hiding data from the user.
    // I have no direct access to scripts in the game, hence, I have to leave a reference here, and I can call this 
    // function when a player reaches the target
    public void DisableMove() 
    {
        move.moveSpeed = 0.0f;
    }
}
