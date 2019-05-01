using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public Button[] buttons;
    public Image[] images; 

    void Start()
    {
        int levelsUnlocked = PlayerPrefs.GetInt("LevelsUnlocked", 1);
        Color shadowColor = new Color(0.5f, 0.5f, 0.5f, 1);

        for (int i = levelsUnlocked; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
            images[i].color = shadowColor;
        }
    }

    public void SelectLevel(int level)
    {
        PlayerPrefs.SetInt("CurrentLevel", level);
        SceneManager.LoadScene("Level" + level);
    }
}
