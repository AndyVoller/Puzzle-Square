using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()                                      // Play the last unlocked level
    {
        int level = PlayerPrefs.GetInt("LevelsUnlocked", 1);
        PlayerPrefs.SetInt("CurrentLevel", level);
        SceneManager.LoadScene("Level" + level);
    }

    public void HowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene("LevelSelector");
    }

}
