using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomDropdownController : MonoBehaviour
{
    public GameObject buttonHolder;
    bool show = false;

    public void ShowButtons()
    {
        
        show = !show;
        buttonHolder.SetActive(show);
    }
}
