using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelUnlockManager : MonoBehaviour
{
    public Button[] levelButtons;

    [Space(7f)]
    public Text solutionPanelText;
    public Text solutionTimeText;

    // Start is called before the first frame update
    void Start()
    {
        int currentLevel = PlayerPrefs.GetInt("levelUnlocked", 1);

        for(int i = 0; i < levelButtons.Length; i++)
        {
            Text[] texts = levelButtons[i].GetComponentsInChildren<Text>();
            Button[] b = levelButtons[i].GetComponentsInChildren<Button>();

            if (i + 1 > currentLevel)
            {
                levelButtons[i].interactable = false;
                b[1].interactable = false;
                texts[1].enabled = true;
            }                
        }      
    }

    public void SetSolutionText(TextContainer c)
    {
        solutionPanelText.text = c.loadText;
    }

    public void SetSolutionTimeText(int i)
    {
        int x = PlayerPrefs.GetInt("time" + i.ToString());
        solutionTimeText.text = x.ToString();
    }
}
