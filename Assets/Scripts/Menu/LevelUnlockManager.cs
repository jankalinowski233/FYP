using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Unlocks levels
public class LevelUnlockManager : MonoBehaviour
{
    public Button[] levelButtons; // Array of buttons

    [Space(7f)]
    public Text solutionPanelText;
    public Text solutionTimeText;

    // Start is called before the first frame update
    void Start()
    {
        int currentLevel = PlayerPrefs.GetInt("levelUnlocked", 1);  // Get int from player prefs

        // Check if i does not exceed current level. Unlock all levels between range <0; currentLevel>.
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

    // Sets solution text
    public void SetSolutionText(TextContainer c)
    {
        solutionPanelText.text = c.loadText;
    }

    // Sets solution time text
    public void SetSolutionTimeText(int i)
    {
        int x = PlayerPrefs.GetInt("time" + i.ToString()); // Get time associated with a level in player prefs
        solutionTimeText.text = x.ToString();
    }
}
