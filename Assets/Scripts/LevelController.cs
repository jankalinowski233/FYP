using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;

    public UnityEvent OnGoalReach;
    public UnityEvent OnLoseCondition;

    public float currentProgress = 0;
    protected float goalProgress = 1.0f;

    protected bool invoked = false;

    public Image bar;
    
    private void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Update is called once per frame
    public void Update()
    {
        float progress = currentProgress / goalProgress;

        if(progress >= 1f && invoked == false)
        {
            OnGoalReach.Invoke();
            invoked = true;
        }
    }

    public void IncrementProgress(float value)
    {
        currentProgress += value;
    }

    public void UpdateBarUI(float amount)
    {
        bar.fillAmount += 1.0f / amount;
    }
}
