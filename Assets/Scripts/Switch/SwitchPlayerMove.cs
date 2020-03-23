using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MovePlayer : MonoBehaviour
{ 
    public abstract float MoveTo(GameObject t);
}

public class SwitchPlayerMove : PlayerMove
{
    public MovePlayer scriptMove;
    public GameObject goalReachedTarget;

    GameObject target;
    bool goalReached;

    public Text move2;
    public Text moveToBase;

    protected override void Start()
    {
        base.Start();
        target = GameObject.FindGameObjectWithTag("Target");
        goalReached = false;
    }

    protected override void Update()
    {
        if (scriptMove != null)
        {
            if (goalReached == false)
            {
                moveSpeed = scriptMove.MoveTo(target);
                base.Update();
            }
            else
            {
                moveSpeed = 0.0f;
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 2.0f * Time.deltaTime);

                moveText.color = new Color(moveText.color.r, moveText.color.g, moveText.color.b, 0.2f);
                move2.color = new Color(move2.color.r, move2.color.g, move2.color.b, 0.2f);
                idleText.color = new Color(idleText.color.r, idleText.color.g, idleText.color.b, 0.2f);
                moveToBase.color = new Color(moveToBase.color.r, moveToBase.color.g, moveToBase.color.b, 1.0f);
            }

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

            transform.LookAt(target.transform.position);
        }      
    }

    public void GoalReached()
    {
        goalReached = true;
        target = goalReachedTarget;
    }
}
