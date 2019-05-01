using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
                Destroy(gameObject);
        }
    }
    #endregion

    public GameObject levelCompleteUI;

    public void CheckWinCondition()
    {
        if (!Win())
            return;

        UnlockLevels();
        StickersController.Paused = true;
        levelCompleteUI.SetActive(true);
    }

    public virtual bool Win()
    {
        Vector3[,] positions = SquareManager.instance.TilesPositions;
        int side = SquareManager.instance.Side;

        for (int i = 0; i < side; i++)
        {
            Vector2 position = new Vector2(positions[i, 0].x, positions[i, 0].y);
            RaycastHit2D hit = new RaycastHit2D();
            hit = Physics2D.Raycast(position, Vector2.up, 0.1f, 1, -1f, 1f);
            int rowColor = hit.collider.GetComponent<Sticker>().Color;      // Each sticker in row has the same color

            for (int j = 1; j < side; j++)
            {
                position = new Vector2(positions[i, j].x, positions[i, j].y);
                hit = Physics2D.Raycast(position, Vector2.up, 0.1f, 1, -1f, 1f);

                if (hit.collider == null)                                   // Empty space
                    continue;

                int color = hit.collider.GetComponent<Sticker>().Color;

                if (color != rowColor)
                {
                    return false;
                }
            }
        }

        return true;
    }

    void UnlockLevels()
    {
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        int levelsUnlocked = PlayerPrefs.GetInt("LevelsUnlocked", 1);

        if (currentLevel == levelsUnlocked)
            PlayerPrefs.SetInt("LevelsUnlocked", levelsUnlocked + 1);
    }
}
