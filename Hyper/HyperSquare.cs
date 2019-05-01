using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperSquare : Square
{
    public override void CreateGameMatrix()
    {
        // Result:
        //      -1 -1  0  0  -1 -1
        //      -1 -1  0  0  -1 -1
        //       1  1  2  2   3  3
        //       1  1  2  2   3  3
        //      -1 -1  4  4  -1 -1
        //      -1 -1  4  4  -1 -1
        // -1 means empty spaces

        SquareManager = SquareManager.instance;
        Side = SquareManager.instance.Side;
        GameMatrix = new int[Side, Side];
        int side2 = Side / 3;               // Side of small squares

        for (int i = 0; i < Side; i++)
        {
            for (int j = 0; j < Side; j++)
            {
                if (i < side2)
                {
                    if ((j >= side2) && (j < 2 * side2))       // First square
                        GameMatrix[i, j] = SquareManager.Colors[0];
                    else
                        GameMatrix[i, j] = -1;      // Empty space
                }
                else if ((i >= side2) && (i < 2 * side2))
                {
                    if (j < side2)                  // Second square
                        GameMatrix[i, j] = SquareManager.Colors[1];
                    else if ((j >= side2) && (j < 2 * side2))       // Third (central) square
                        GameMatrix[i, j] = SquareManager.Colors[2];
                    else                            // Fourth square
                        GameMatrix[i, j] = SquareManager.Colors[3];
                }
                else
                {
                    if ((j >= side2) && (j < 2 * side2))        // Fifth square
                        GameMatrix[i, j] = SquareManager.Colors[4];
                    else
                        GameMatrix[i, j] = -1;      // Empty space
                }
            }
        }
    }

    public override void Rotate()
    {
        int rotationNum = 50;

        for (int i = 0; i < rotationNum; i++)
        {
            bool row = Random.Range(0, 2) == 1;
            int position = Random.Range(Side/3, 2*Side/3);
            int offset = Random.Range(1, Side);

            RotateLine(row, position, offset);
        }

        SquareManager.Rotation();
    }
}
