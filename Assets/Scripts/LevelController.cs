using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Controls individual levels

public class LevelController : MonoBehaviour
{
    public static LevelController instance; // Instance

    // Various events
    public UnityEvent OnLevelBegin;
    public UnityEvent OnGoalReach;
    public UnityEvent OnLoseCondition;

    // Progress
    public float currentProgress = 0;
    protected float goalProgress = 1.0f;

    // Were events invoked?
    protected bool invoked = false;

    // Some UI
    public Image bar;
    public Text timeText;

    private void Start()
    {
        // Init
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        // Invoke LevelBegin events
        OnLevelBegin.Invoke();
    }

    // Update is called once per frame
    public void Update()
    {
        float progress = currentProgress / goalProgress; // Check if goal progress has been reached

        if(progress >= 1f && invoked == false) // If goal progress has been reached...
        {
            OnGoalReach.Invoke();
            invoked = true;
        }
    }

    public void IncrementProgress(float value) // Increments current progress
    {
        currentProgress += value;
    }

    public void UpdateBarUI(float amount) // Updates bar's fill amount
    {
        bar.fillAmount += 1.0f / amount;
    }

    public void SetTimeText() // Sets level completion time text
    {
        timeText.text = Time.timeSinceLevelLoad.ToString("0") + "s";
    }

    public void SetTimescale(float scale) // Sets time scale
    {
        Time.timeScale = scale;
    }

    public void InvokeLose() // Triggered when a player loses
    {
        OnLoseCondition.Invoke();
    }
}
