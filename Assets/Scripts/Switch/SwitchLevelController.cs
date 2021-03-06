﻿ using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// A level controller for switch level
public class SwitchLevelController : LevelController
{ 
    public float timer; // Timer
    public Text timerText;

    // Update is called once per frame
    new public void Update()
    {
        base.Update();

        if(timer > 0) // Decrement & display time
        {
            timer -= Time.deltaTime;
            timerText.text = timer.ToString("0");
        }
        else if(timer <= 0 && invoked == false)
        {
            invoked = true;
            timer = 0f;
            timerText.text = "0";

            // lose condition
            OnLoseCondition.Invoke();
        }
            
    }
}
