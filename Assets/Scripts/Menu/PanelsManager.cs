using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelsManager : MonoBehaviour
{
    public GameObject[] panels;
    int current;

    private void Start()
    {
        current = 0;
    
        foreach(GameObject p in panels)
        {
            p.SetActive(false);
        }

        panels[0].SetActive(true);
    }

    public void ShowNext()
    {
        if (current < panels.Length-1)
            current += 1;
        else
            current = 0;

        panels[current].SetActive(true);

        if(current > 0)
            panels[current - 1].SetActive(false);
        else
            panels[panels.Length-1].SetActive(false);
    }

    public void ShowPrevious()
    {
        if (current > 0)
            current -= 1;
        else
            current = panels.Length-1;

        panels[current].SetActive(true);

        if (current < panels.Length-1)
            panels[current + 1].SetActive(false);
        else
            panels[0].SetActive(false);
    }
}
