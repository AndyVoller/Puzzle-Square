using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlay : MonoBehaviour
{
    public GameObject[] infoPanels;
    
    public void Next(int i)
    {
        if (i >= infoPanels.Length - 1)
            return;

        infoPanels[i].SetActive(false);
        infoPanels[i + 1].SetActive(true);
    }

    public void Prev(int i)
    {
        if (i <= 0)
            return;

        infoPanels[i].SetActive(false);
        infoPanels[i - 1].SetActive(true);
    }
}
