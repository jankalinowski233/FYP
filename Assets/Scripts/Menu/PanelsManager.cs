using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Manages Getting started section panels
public class PanelsManager : MonoBehaviour
{
    public GameObject[] panels; // Array of all panels
    public int current; // current panel

    private void Start()
    {
        current = 0; // init current
    
        // Deactivate all panels
        foreach(GameObject p in panels)
        {
            p.SetActive(false);
        }

        // Activate first panel
        panels[0].SetActive(true);
    }

    // Shows next panel
    public void ShowNext()
    {
        // Check if current does not exceed number of elements in the array
        if (current < panels.Length-1)
            current += 1;
        else
            current = 0;

        panels[current].SetActive(true); // Activate current panel

        // Deactivate previous panel
        if(current > 0)
            panels[current - 1].SetActive(false);
        else
            panels[panels.Length-1].SetActive(false);
    }

    // Shows previous panel
    public void ShowPrevious()
    {
        // Check if current does not is greater than 0
        if (current > 0)
            current -= 1;
        else
            current = panels.Length-1;

        panels[current].SetActive(true); // Activate current panel

        // Deactivate next (previous) panel
        if (current < panels.Length-1)
            panels[current + 1].SetActive(false);
        else
            panels[0].SetActive(false);
    }
}
