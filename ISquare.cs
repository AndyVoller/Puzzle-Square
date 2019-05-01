using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISquare
{
    int[,] GameMatrix { get; set; }
    SquareManager SquareManager { get; set; }
    int Side { get; set; }

    void CreateGameMatrix();
    void Rotate();
}
