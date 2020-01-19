using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;

    public UnityEvent OnGoalReach;

    public float currentProgress = 0;
    float goalProgress = 1.0f;

    bool invoked = false;

    public Image healthbar;

    private void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
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

    public void UpdateHealthUI(float amount)
    {
        healthbar.fillAmount += 1.0f / amount;
    }
}
