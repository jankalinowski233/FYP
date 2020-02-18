using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            }
            else
            {
                moveSpeed = 0.0f;
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 2.0f * Time.deltaTime);
            }
            base.Update();

            transform.LookAt(target.transform.position);
        }      
    }

    public void GoalReached()
    {
        goalReached = true;
        target = goalReachedTarget;
    }
}
