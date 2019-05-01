using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwistSquareManager : SquareManager
{
    public override void CreateSquare()
    {
        Square = new TwistSquare();
        Square.CreateGameMatrix();
        StickersController.Paused = true;
    }

}
