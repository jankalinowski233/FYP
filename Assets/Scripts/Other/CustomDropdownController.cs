using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Controls custom dropdown
public class CustomDropdownController : MonoBehaviour
{
    public GameObject buttonHolder;
    bool show = false;

    public void ShowButtons()
    {        
        // Show a dropdown
        show = !show;
        buttonHolder.SetActive(show);
    }
}
