using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderTwistSquareManager : OrderSquareManager
{
    public override void CreateSquare()
    {
        Square = new OrderTwistSquare();
        Square.CreateGameMatrix();
        StickersController.Paused = true;
    }

}
