using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Base script for a script to be written in game
public abstract class MovePlayer : MonoBehaviour
{ 
    public abstract float MoveTo(GameObject t);
}

public class SwitchPlayerMove : PlayerMove
{
    public MovePlayer scriptMove; // Reference to a script written by users
    public GameObject goalReachedTarget; // Final target

    GameObject target;
    bool goalReached;

    // In-game text
    public Text move2;
    public Text moveToBase;

    protected override void Start()
    {
        // Init
        base.Start();
        target = GameObject.FindGameObjectWithTag("Target"); // Find a target
        goalReached = false;
    }

    protected override void Update()
    {
        if (scriptMove != null) // If users written a coad
        {
            if (goalReached == false) // As long as goal is not reached
            {
                moveSpeed = scriptMove.MoveTo(target); // Move to a target
                base.Update(); // Base update
            }
            else
            {
                moveSpeed = 0.0f; // Set moveSpeed to 0
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 2.0f * Time.deltaTime); // Move towards and end-target

                // Change text based on in-game events
                moveText.color = new Color(moveText.color.r, moveText.color.g, moveText.color.b, 0.2f);
                move2.color = new Color(move2.color.r, move2.color.g, move2.color.b, 0.2f);
                idleText.color = new Color(idleText.color.r, idleText.color.g, idleText.color.b, 0.2f);
                moveToBase.color = new Color(moveToBase.color.r, moveToBase.color.g, moveToBase.color.b, 1.0f);
            }

            // Change text based on in-game events
            if (moveSpeed > 0 && goalReached == false)
            {
                moveText.color = new Color(moveText.color.r, moveText.color.g, moveText.color.b, 1.0f);
                move2.color = new Color(move2.color.r, move2.color.g, move2.color.b, 0.2f);
                idleText.color = new Color(idleText.color.r, idleText.color.g, idleText.color.b, 0.2f);
            }
            else if (moveSpeed < 0 && goalReached == false)
            {
                moveText.color = new Color(moveText.color.r, moveText.color.g, moveText.color.b, 0.2f);
                move2.color = new Color(move2.color.r, move2.color.g, move2.color.b, 1.0f);
                idleText.color = new Color(idleText.color.r, idleText.color.g, idleText.color.b, 0.2f);
            }
            else if(moveSpeed == 0 && goalReached == false)
            {
                moveText.color = new Color(moveText.color.r, moveText.color.g, moveText.color.b, 0.2f);
                move2.color = new Color(move2.color.r, move2.color.g, move2.color.b, 0.2f);
                idleText.color = new Color(idleText.color.r, idleText.color.g, idleText.color.b, 1.0f);
            }

            // Look at target
            transform.LookAt(target.transform.position);
        }      
    }

    public void GoalReached() // Change target to be a end-goal target (off-screen)
    {
        goalReached = true;
        target = goalReachedTarget;
    }

    public GameObject GetTarget() // Returns current target
    {
        return target;
    }

    public bool GetGoalReached() // Returns bool goal reached
    {
        return goalReached;
    }
}
