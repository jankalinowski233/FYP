using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SceneController : MonoBehaviour
{
    CompileCode c;

    private void Awake()
    {
        c = GetComponent<CompileCode>();
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void RestartLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }

    public void UnlockLevel()
    {
        PlayerPrefs.SetInt("levelUnlocked", SceneManager.GetActiveScene().buildIndex + 1);
        PlayerPrefs.SetInt("time" + SceneManager.GetActiveScene().buildIndex.ToString(), (int)Time.timeSinceLevelLoad);
    }

    public void SaveCode()
    {
        // Saved to: C:/Users/name/AppData/LocalLow/DefaultCompany/ProjectName/LevelX.txt
        string directory = Application.persistentDataPath + "/Level" + SceneManager.GetActiveScene().buildIndex.ToString() + ".txt";

        string saveStr = c.codeString;

        c.container.visibleText = saveStr;
        StreamWriter writer = new StreamWriter(directory);
        writer.Write(saveStr);

        writer.Close();
    }

    public void LoadCode()
    {      
        string directory = Application.persistentDataPath + "/Level" + SceneManager.GetActiveScene().buildIndex.ToString() + ".txt";
        if(File.Exists(directory))
        {
            StreamReader reader = new StreamReader(directory);
            string actualCode = reader.ReadToEnd();
            reader.Close();

           c.container.visibleText = actualCode;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
