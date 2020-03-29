using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;

    public UnityEvent OnLevelBegin;
    public UnityEvent OnGoalReach;
    public UnityEvent OnLoseCondition;

    public float currentProgress = 0;
    protected float goalProgress = 1.0f;

    protected bool invoked = false;

    public Image bar;
    public Text timeText;

    private void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        OnLevelBegin.Invoke();
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

    public void SetTimeText()
    {
        timeText.text = Time.timeSinceLevelLoad.ToString("0") + "s";
    }

    public void SetTimescale(float scale)
    {
        Time.timeScale = scale;
    }

    public void InvokeLose()
    {
        OnLoseCondition.Invoke();
    }
}
