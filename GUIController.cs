using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIController : MonoBehaviour
{
    public GameObject InfoUI;

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void Info()
    {
        InfoUI.SetActive(!InfoUI.activeSelf);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LevelSelector()
    {
        SceneManager.LoadScene("LevelSelector");
    }

    public void ClockwiseRotation()
    {
        TwistSquare square = SquareManager.instance.Square as TwistSquare;
        square.Clockwise();
        Rotation();
    }

    public void AntiClockwiseRotation()
    {
        TwistSquare square = SquareManager.instance.Square as TwistSquare;
        square.AntiClockwise();
        Rotation();
    }

    public void LargeClockwiseRotation()
    {
        TwistSquare square = SquareManager.instance.Square as TwistSquare;
        square.LargeClockwise();
        Rotation();
    }

    public void LargeAntiClockwiseRotation()
    {
        TwistSquare square = SquareManager.instance.Square as TwistSquare;
        square.LargeAntiClockwise();
        Rotation();
    }

    public void RowRotation()
    {
        TwistSquare square = SquareManager.instance.Square as TwistSquare;
        square.Row();
        Rotation();
    }

    public void ColumnRotation()
    {
        TwistSquare square = SquareManager.instance.Square as TwistSquare;
        square.Column();
        Rotation();
    }

    void Rotation()
    {
        SquareManager.instance.Rotation();
        GameManager.instance.CheckWinCondition();
    }
}
