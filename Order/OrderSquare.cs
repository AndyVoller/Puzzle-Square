using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderSquare : Square
{
    public override void CreateGameMatrix()
    {
        SquareManager = SquareManager.instance;
        Side = SquareManager.instance.Side;
        GameMatrix = new int[Side, Side];

        for (int i = 0; i < Side; i++)
        {
            for (int j = 0; j < Side; j++)
            {
                GameMatrix[i, j] = Side * i + j + 1;
            }
        }
    }

}
