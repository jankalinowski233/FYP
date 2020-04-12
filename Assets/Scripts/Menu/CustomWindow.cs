using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
// A class for custom window, that will contain everything that I need to make my life easier when developing
public class CustomWindow : EditorWindow
{
    [MenuItem("Window/Custom window")]
    public static void ShowWindow()
    {
        GetWindow<CustomWindow>("Custom window");
    }

    void OnGUI()
    {
        //Delete all of the PlayerPrefs settings by pressing this Button
        if (GUILayout.Button("Delete prefs"))
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
#endif