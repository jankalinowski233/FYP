using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitLevels : MonoBehaviour
{
    public List<TextContainer> containers;

    // Start is called before the first frame update
    void Start()
    {
        // Load saved code from completed/attempted levels
        SceneController sc = FindObjectOfType<SceneController>();

        for (int i = 0; i < containers.Count; i++)
        {
            containers[i].loadText = sc.LoadCode(i+1);
        }
    }
}
