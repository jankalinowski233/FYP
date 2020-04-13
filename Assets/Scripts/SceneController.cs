using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

// Controls scenes
public class SceneController : MonoBehaviour
{
    public CompileCode c; // Reference to compile code script

    private void Awake()
    {
        // Init
        c = GetComponent<CompileCode>();
    }

    public void LoadLevel(string levelName) // Loads a scene
    {
        SceneManager.LoadScene(levelName);
    }

    public void RestartLevel() // Restarts a level
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }

    public void UnlockLevel() // Unlocks a next level
    {
        PlayerPrefs.SetInt("levelUnlocked", SceneManager.GetActiveScene().buildIndex + 1);
        PlayerPrefs.SetInt("time" + SceneManager.GetActiveScene().buildIndex.ToString(), (int)Time.timeSinceLevelLoad);
    }

    public void SaveCode() // Saves written code
    {
        // Saved to: C:/Users/name/AppData/LocalLow/DefaultCompany/ProjectName/LevelX.txt
        string directory = Application.persistentDataPath + "/Level" + SceneManager.GetActiveScene().buildIndex.ToString() + ".txt";
        string saveStr = c.codeString;

        c.container.visibleText = saveStr;
        c.container.loadText = saveStr;
        StreamWriter writer = new StreamWriter(directory);
        writer.Write(saveStr);

        writer.Close();
    }

    public string LoadCode(int i) // Loads saved code (if exists)
    {      
        string directory = Application.persistentDataPath + "/Level" + i.ToString() + ".txt";

        string actualCode = "";
        if (File.Exists(directory))
        {
            StreamReader reader = new StreamReader(directory);
            actualCode = reader.ReadToEnd();
            reader.Close();
        }

        return actualCode;
    }

    public void SetCode() // Sets code
    {
        int i = SceneManager.GetActiveScene().buildIndex; // Get active scene index
        if(LoadCode(i) != "") // Load code associated with it
        {
            c.container.visibleText = LoadCode(i);
        }
        else // If load code is empty
        {
            c.container.visibleText = c.container.defaultText;
        }
    }

    public void Quit() // Quits game
    {
        Application.Quit();
    }
}
