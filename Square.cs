using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : ISquare
{
    public int[,] GameMatrix { get; set; }
    public SquareManager SquareManager { get; set; }
    public int Side { get; set; }

    public virtual void CreateGameMatrix()
    {
        SquareManager = SquareManager.instance;
        Side = SquareManager.Side;
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
            bool row = Random.Range(0, 2)==1;
            int position = Random.Range(0, Side);
            int offset= Random.Range(1, Side);

            RotateLine(row, position, offset);
        }

        SquareManager.Rotation();
    }

    public void RotateLine(bool row, int position, int offset)
    {
        int[] line = new int[Side];

        if (row)
        {
            for (int i = 0; i < Side; i++)
            {
                line[i] = GameMatrix[position, (offset + i) % Side];
            }
            for (int i = 0; i < Side; i++)
            {
                GameMatrix[position, i] = line[i];
            }
        }
        else
        {
            for (int i = 0; i < Side; i++)
            {
                line[i] = GameMatrix[(offset + i) % Side, position];
            }
            for (int i = 0; i < Side; i++)
            {
                GameMatrix[i, position] = line[i];
            }
        }
    }

}
