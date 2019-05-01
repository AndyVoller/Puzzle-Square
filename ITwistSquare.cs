using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITwistSquare : ISquare
{
    void Clockwise();
    void AntiClockwise();
    void LargeClockwise();
    void LargeAntiClockwise();
    void Row();
    void Column();
}
