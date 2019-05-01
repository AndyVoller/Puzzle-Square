using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwistSquare : ITwistSquare
{
    public int[,] GameMatrix { get; set; }
    public SquareManager SquareManager { get; set; }
    public int Side { get; set; }

    protected int[] a = new int[8] { 0, 0, 0, 1, 2, 2, 2, 1};
    protected int[] b = new int[8] { 0, 1, 2, 2, 2, 1, 0, 0};
    protected int[] largeA = new int[16] { 0, 0, 0, 0, 0, 1, 2, 3, 4, 4, 4, 4, 4, 3, 2, 1 };
    protected int[] largeB = new int[16] { 0, 1, 2, 3, 4, 4, 4, 4, 4, 3, 2, 1, 0, 0, 0, 0 };

    public virtual void CreateGameMatrix()
    {
        SquareManager = TwistSquareManager.instance;
        Side = TwistSquareManager.instance.Side;
        GameMatrix = new int[Side, Side];

        for (int i = 0; i < Side; i++)
        {
            for (int j = 0; j < Side; j++)
            {
                GameMatrix[i, j] = SquareManager.Colors[i];
            }
        }
    }

    public virtual void Rotate()
    {
        int rotationNum = 50;

        for (int i = 0; i < rotationNum; i++)
        {
            int rotationVar = Side == 3 ? 4 : 6;            // Number of possible rotations
            int rotation = Random.Range(1, rotationVar + 1);

            switch (rotation)
            {
                case 1: Clockwise();
                    break;
                case 2: AntiClockwise();
                    break;
                case 3: Row();
                    break;
                case 4: Column();
                    break;
                case 5:
                    LargeClockwise();
                    break;
                case 6:
                    LargeAntiClockwise();
                    break;
            }
        }

        SquareManager.Rotation();
    }

    public void Clockwise()
    {
        int k = Side == 3 ? 0 : 1;                  // Offset
        int temp = GameMatrix[a[a.Length - 1] + k, b[b.Length - 1] + k];

        for (int i = a.Length - 1; i > 0; i--)
            GameMatrix[a[i] + k, b[i] + k] = GameMatrix[a[i - 1] + k, b[i - 1] + k];

        GameMatrix[a[0] + k, b[0] + k] = temp;
    }

    public void AntiClockwise()
    {
        int k = Side == 3 ? 0 : 1;                  // Offset
        int temp = GameMatrix[a[0]+k, b[0]+k];

        for (int i = 0; i < a.Length - 1; i++) 
            GameMatrix[a[i]+k, b[i]+k] = GameMatrix[a[i + 1]+k, b[i + 1]+k];

        GameMatrix[a[a.Length - 1]+k, b[b.Length - 1]+k] = temp;
    }

    public void LargeClockwise()
    {
        int temp = GameMatrix[largeA[largeA.Length - 1], largeB[largeB.Length - 1]];

        for (int i = largeA.Length - 1; i > 0; i--)
            GameMatrix[largeA[i], largeB[i]] = GameMatrix[largeA[i - 1], largeB[i - 1]];

        GameMatrix[largeA[0], largeB[0]] = temp;
    }

    public void LargeAntiClockwise()
    {
        int temp = GameMatrix[largeA[0], largeB[0]];

        for (int i = 0; i < largeA.Length - 1; i++)
            GameMatrix[largeA[i], largeB[i]] = GameMatrix[largeA[i + 1], largeB[i + 1]];

        GameMatrix[largeA[largeA.Length - 1], largeB[largeB.Length - 1]] = temp;
    }

    public void Row()
    {
        int line = Side / 2;
        int temp = GameMatrix[line, Side - 1];

        for (int i = Side - 1; i > 0; i--)
            GameMatrix[line, i] = GameMatrix[line, i - 1];

        GameMatrix[line, 0] = temp;
    }

    public void Column()
    {
        int column = Side / 2;
        int temp = GameMatrix[Side - 1, column];

        for (int i = Side - 1; i > 0; i--)
            GameMatrix[i, column] = GameMatrix[i - 1, column];

        GameMatrix[0, column] = temp;
    }
}
