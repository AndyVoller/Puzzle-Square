using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcentricSquareManager : SquareManager
{
    public override void CreateSquare()
    {
        Square = new ConcentricSquare();
        Square.CreateGameMatrix();
    }

}
