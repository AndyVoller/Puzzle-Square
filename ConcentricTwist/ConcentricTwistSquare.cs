using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcentricTwistSquare : TwistSquare
{
    public override void CreateGameMatrix()
    {
        SquareManager = SquareManager.instance;
        Side = SquareManager.instance.Side;
        GameMatrix = new int[Side, Side];

        for (int k = 0; k <= Side / 2; k++)         // Num of circle
        {
            for (int i = k; i < Side - k; i++)
            {
                for (int j = k; j < Side - k; j++)
                {
                    if (i == k || j == k || i == Side - k - 1 || j == Side - k - 1)
                    {
                        GameMatrix[i, j] = SquareManager.Colors[k];
                    }
                }
            }
        }
    }

}
