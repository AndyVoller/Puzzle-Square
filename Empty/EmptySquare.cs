using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptySquare : Square
{
    public override void CreateGameMatrix()
    {
        base.CreateGameMatrix();

        bool emptyCenter = (SquareManager as EmptySquareManager).emptyCenter;
        CreateEmptySpace(emptyCenter);
    }

    protected void CreateEmptySpace(bool EmptyCenter)
    {
        if (EmptyCenter)            // Empty space is in the center
        {
            GameMatrix[Side / 2, Side / 2] = -1;
        }
        else                        // Empty spaces are in the internal corners
        {
            GameMatrix[1, 1] = -1;
            GameMatrix[1, Side - 2] = -1;
            GameMatrix[Side - 2, 1] = -1;
            GameMatrix[Side - 2, Side - 2] = -1;
        }
    }

    public override void Rotate()
    {
        int rotationNum = 100;

        for (int i = 0; i < rotationNum; i++)
        {
            bool row = Random.Range(0, 2) == 1;
            int position = Random.Range(0, Side);       // Num of row/column to rotate
            int offset = Random.Range(1, Side);

            bool emptyCenter = (SquareManager as EmptySquareManager).emptyCenter;
            if (emptyCenter)
            {
                if (position == Side / 2)           // Can't rotate middle column and row
                    continue;
            }
            else
            {
                if (position == 1 || position == Side - 2)
                    continue;
            }

            RotateLine(row, position, offset);
        }

        SquareManager.Rotation();
    }
}
