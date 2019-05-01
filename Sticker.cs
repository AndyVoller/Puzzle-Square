using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticker : MonoBehaviour
{
    [SerializeField] protected int color;
    public int Color { get { return color; } set { color = value; } }
    public Vector3 StartPosition { get; set; }          // Position before drag
    public bool IsDestroyed { get; set; }

    private static float minDrag = 0.15f;              // For angle calculation 
    private static float maxSpeed = 0.4f;             // Limit stickers speed 

    private Vector2 targetPosition;
    private Vector2 prevMousePos;                     // Last frame mouse position
    private Vector2 startMousePos;                    // Mouse position when mouse drag starts

    // Remember mouse position
    void OnMouseDown()
    {
        if (StickersController.Paused)
            return;

        if (StickersController.AnimationRun)
            return;

        if (StickersController.HasAngle)        
            return;

        StickersController.CanDrag = true;
        startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        prevMousePos = startMousePos;
    }

    // Forget angle and fix position
    void OnMouseUp()
    {
        if (!StickersController.HasAngle)
            return;

        StickersController.AnimationRun = true;                        // Make some pause for animation
        StickersController.CanDrag = false;
        StickersController.HasAngle = false;

        // Start animation to line up stickers
        StartCoroutine(StickersController.FixPosition(StartPosition, transform.position));
    }

    // Calculate direction and move
    void OnMouseDrag()
    {
        if (StickersController.Paused)
            return;

        if (!StickersController.CanDrag)
            return;

        Vector2 currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 deltaMousePos = currentMousePos - startMousePos;

        // Calculate angle and direction
        if (!StickersController.HasAngle)           
        {
            if (deltaMousePos.magnitude < minDrag)
                return;

            CalculateAngle(deltaMousePos);
        }

        // Limit target position
        targetPosition = currentMousePos;
        CalculateTargetPosition();

        // Limit stickers speed
        Vector2 offset = targetPosition - prevMousePos;
        offset.x = Mathf.Clamp(offset.x, -maxSpeed, maxSpeed);
        offset.y = Mathf.Clamp(offset.y, -maxSpeed, maxSpeed);

        deltaMousePos = prevMousePos - startMousePos;
        deltaMousePos += offset;
        prevMousePos = startMousePos + deltaMousePos;

        StickersController.MoveLine(deltaMousePos, transform.position);
    }

    private void CalculateAngle(Vector2 deltaMousePos)
    {
        float angle = Mathf.Atan2(deltaMousePos.y, deltaMousePos.x);
        angle = angle * 180 / Mathf.PI;
        StickersController.Horizontal =
            (angle > -45 && angle < 45) || (angle > 135) || (angle < -135);
        StickersController.Direction = StickersController.Horizontal ? Vector3.right : Vector3.up;
        StickersController.HasAngle = true;
    }

    private void CalculateTargetPosition()
    {
        SquareManager squareManager = SquareManager.instance;
        float reserve = StickersController.TileSize / 3;

        if (StickersController.Horizontal)
        {
            float minX = squareManager.TilesPositions[0, 0].x - reserve;
            float maxX = squareManager.TilesPositions[0, squareManager.Side - 1].x + reserve;

            if (targetPosition.x < minX)
                targetPosition.x = minX;
            if (targetPosition.x > maxX)
                targetPosition.x = maxX;
        }
        else
        {
            float minY = squareManager.TilesPositions[squareManager.Side - 1, 0].y - reserve;
            float maxY = squareManager.TilesPositions[0, 0].y + reserve;

            if (targetPosition.y < minY)
                targetPosition.y = minY;
            if (targetPosition.y > maxY)
                targetPosition.y = maxY;
        }
    }

    public void Move(Vector2 deltaPosition)
    {
        if (StickersController.Horizontal)
        {
            transform.position = StartPosition + Vector3.right * (deltaPosition.x);
        }
        else
        {
            transform.position = StartPosition + Vector3.up * (deltaPosition.y);
        }
    }
}