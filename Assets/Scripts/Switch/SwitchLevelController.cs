using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SwitchLevelController : MonoBehaviour
{
    public UnityEvent OnGoalReach;

    public float currentProgress = 0;
    float goalProgress = 1.0f;

    bool invoked = false;

    public Image oxygenBar;

    public float timer;
    public Text timerText;

    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
            timer -= Time.deltaTime;

        
        float progress = currentProgress / goalProgress;

        if (progress >= 1f && invoked == false)
        {
            OnGoalReach.Invoke();
            invoked = true;
        }
    }

    public void IncrementProgress(float value)
    {
        currentProgress += value;
    }

    public void UpdateOxygenUI(float amount)
    {
        oxygenBar.fillAmount += 1.0f / amount;
    }
}
