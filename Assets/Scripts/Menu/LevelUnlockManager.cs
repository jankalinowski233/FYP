using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelUnlockManager : MonoBehaviour
{
    public Button[] levelButtons;

    // Start is called before the first frame update
    void Start()
    {
        int currentLevel = PlayerPrefs.GetInt("levelUnlocked", 1);

        for(int i = 0; i < levelButtons.Length; i++)
        {
            Text[] texts = levelButtons[i].GetComponentsInChildren<Text>();

            if (i + 1 > currentLevel)
            {
                levelButtons[i].interactable = false;
                texts[1].enabled = true;
            }
                
        }
    }
}
