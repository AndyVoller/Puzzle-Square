using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcentricTwistGameManager : ConcentricGameManager
{
    public override bool Win()
    {
        Vector3[,] pos = SquareManager.instance.TilesPositions;
        int side = SquareManager.instance.Side;
        RaycastHit2D hit = new RaycastHit2D();

        for (int k = 0; k <= side / 2; k++)
        {
            for (int i = k; i < side - k; i++)
            {
                for (int j = k; j < side - k; j++)
                {
                    if (i != k && j != k && i != side - k - 1 && j != side - k - 1)
                        continue;

                    Vector2 position = new Vector2(pos[i, j].x, pos[i, j].y);
                    hit = Physics2D.Raycast(position, Vector2.up, 0.1f);
                    int color = hit.collider.GetComponent<Sticker>().Color;

                    if (color != SquareManager.instance.Colors[k])
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

}
