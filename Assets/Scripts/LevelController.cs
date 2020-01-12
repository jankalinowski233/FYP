using UnityEngine;
using UnityEngine.Events;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;

    public UnityEvent OnGoalReach;

    public float currentProgress = 0;
    float goalProgress = 1.0f;

    bool invoked = false;

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
}
