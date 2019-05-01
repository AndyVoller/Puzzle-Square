using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderGameManager : GameManager
{
    public override bool Win()
    {
        Vector3[,] pos = SquareManager.instance.TilesPositions;
        int side = SquareManager.instance.Side;
        RaycastHit2D hit = new RaycastHit2D();

        for (int i = 0; i < side; i++)
        {
            for (int j = 1; j < side; j++)
            {
                Vector2 position = new Vector2(pos[i, j].x, pos[i, j].y);
                hit = Physics2D.Raycast(position, Vector2.up, 0.1f);

                if (hit.collider == null)                                   // Empty space
                    continue;

                int color = hit.collider.GetComponent<Sticker>().Color;

                if (color != i * SquareManager.instance.Side + j + 1)
                {
                    return false;
                }
            }
        }

        return true;
    }

}
