using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcentricTwistSquareManager : TwistSquareManager
{
    public override void CreateSquare()
    {
        Square = new ConcentricTwistSquare();
        Square.CreateGameMatrix();
        StickersController.Paused = true;
    }

}
