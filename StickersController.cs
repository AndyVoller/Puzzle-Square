using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickersController : MonoBehaviour
{
    public static bool Horizontal { get; set; }
    public static Vector3 Direction { get; set; }
    public static float TileSize { get; set; }
    public static bool HasAngle { get; set; }
    public static bool AnimationRun { get; set; }           // Make pause for animation
    public static bool CanDrag { get; set; }
    public static bool Paused { get; set; }

    static SquareManager squareManager;
    static GameManager gameManager;

    void Start()
    {
        squareManager = SquareManager.instance;
        gameManager = GameManager.instance;

        HasAngle = false;
        AnimationRun = false;
        CanDrag = true;
        Paused = false;
    }

    // Move the line
    public static void MoveLine(Vector2 deltaPos, Vector3 position)
    {
        List<Sticker> line = GetMovingLine(position);

        if (line.Count < squareManager.Side)        // No way to move line with empty space in it
            return;                                     // Used in some square variations

        squareManager.DestroyExcessiveStickers(line);
        squareManager.AddStickers(line);                        // Fill empty space
        line = GetMovingLine(position);                         // Renew moving line

        foreach (Sticker sticker in line)
        {
            sticker.Move(deltaPos);
        }
    }

    // Get all stickers in current line
    private static List<Sticker> GetMovingLine(Vector3 position)
    {
        List<Sticker> stickers = new List<Sticker>();
        int side = squareManager.Side;

        for (int i = -side - 1; i < side + 1; i++)
        {
            RaycastHit2D hit = new RaycastHit2D();
            hit = Physics2D.Raycast(position + Direction * i, -Direction, 0.1f, 1, -1f, 1f);

            if (hit.collider != null)
            {
                Sticker sticker = hit.collider.GetComponent<Sticker>();
                if (sticker != null)
                {
                    stickers.Add(sticker);
                }
            }
        }

        return stickers;
    }

    // Move stickers to nearest tile (line up)
    public static IEnumerator FixPosition(Vector3 startPosition, Vector3 position)
    {
        List<Sticker> movingLine = GetMovingLine(position);

        // Finding distance to nearest tile
        int nearestTileDist = GetNearestTileDist(startPosition, position);
                                     
        // Start move stickers animation
        float t = 0.2f;                                         // interpolant for Lerp()
        for (int i = 0; i < 5; i++)
        {
            foreach (Sticker sticker in movingLine)
            {
                sticker.transform.position = Vector3.Lerp
                    (sticker.transform.position, sticker.StartPosition + Direction * TileSize * nearestTileDist, t);
            }

            t += 0.02f;
            yield return new WaitForSeconds(0.01f);
        }

        // Line up stickers
        foreach (Sticker sticker in movingLine)
        {
            sticker.transform.position = sticker.StartPosition + Direction * TileSize * nearestTileDist;
            sticker.StartPosition = sticker.transform.position;
        }

        squareManager.DestroyExcessiveStickers(movingLine);
        AnimationRun = false;
        gameManager.CheckWinCondition();
    }

    private static int GetNearestTileDist(Vector3 startPosition, Vector3 position)
    {
        float movedDistance;                                    // Distance moved by stickers
        if (Horizontal)
            movedDistance = position.x - startPosition.x;
        else
            movedDistance = position.y - startPosition.y;

        float tileCountDist = movedDistance / TileSize;         // distance measured in tiles
        int nearestTileDist = Mathf.CeilToInt(tileCountDist);   // distance in tiles from start position
        if (nearestTileDist - tileCountDist > 0.5f)                 // to nearest tile
            nearestTileDist--;

        return nearestTileDist;
    }
}
