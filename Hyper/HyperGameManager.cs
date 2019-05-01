using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperGameManager : GameManager
{
    public override bool Win()
    {
        Vector3[,] pos = SquareManager.instance.TilesPositions;
        int side = SquareManager.instance.Side;
        int side2 = side / 3;                                   // Side of small squares
        int[] colors = { -1, -1, -1, -1, -1 };                  // Squares' colors
        RaycastHit2D hit = new RaycastHit2D();

        for (int i = 0; i < side; i++)
        {
            for (int j = 0; j < side; j++)
            {
                Vector2 position = new Vector2(pos[i, j].x, pos[i, j].y);
                hit = Physics2D.Raycast(position, Vector2.up, 0.1f);

                if (hit.collider == null)                                   // Empty space
                    continue;

                int color = hit.collider.GetComponent<Sticker>().Color;

                if (i < side2)                      // First square
                {
                    if (colors[0] == -1)       
                        colors[0] = color;
                    else if (color != colors[0])
                        return false;
                }
                else if ((i >= side2) && (i < 2 * side2))
                {
                    if (j < side2)                  // Second square
                    {
                        if (colors[1] == -1)
                            colors[1] = color;
                        else if (color != colors[1])
                            return false;
                    }
                    else if ((j >= side2) && (j < 2 * side2))       // Third (central) square
                    {
                        if (colors[2] == -1)
                            colors[2] = color;
                        else if (color != colors[2])
                            return false;
                    }
                    else                            // Fourth square
                    {
                        if (colors[3] == -1)
                            colors[3] = color;
                        else if (color != colors[3])
                            return false;
                    }
                }
                else
                { 
                    if (colors[4] == -1)                // Fifth square
                        colors[4] = color;
                    else if (color != colors[4])
                        return false;
                }
            }
        }

        return true;
    }

}
