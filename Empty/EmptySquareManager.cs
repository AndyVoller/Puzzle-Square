using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptySquareManager : HyperSquareManager
{
    public bool emptyCenter;        // Two options for empty pieces:
                                        // empty center or empty corners

    public override void CreateSquare()
    {
        Square = new EmptySquare();
        Square.CreateGameMatrix();
    }
}
