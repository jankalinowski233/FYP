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
        c.container.loadText = saveStr;
        StreamWriter writer = new StreamWriter(directory);
        writer.Write(saveStr);

        writer.Close();
    }

    public string LoadCode(int i)
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

    public void SetCode()
    {
        int i = SceneManager.GetActiveScene().buildIndex;
        if(LoadCode(i) != "")
        {
            c.container.visibleText = LoadCode(i);
        }
        else
        {
            c.container.visibleText = c.container.defaultText;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
