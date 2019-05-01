using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderSquareManager : SquareManager
{
    public GameObject textStickerPrefab;

    public override void CreateSquare()
    {
        Square = new OrderSquare();
        Square.CreateGameMatrix();
    }

    public override void CreateStickers()
    {
        for (int i = 0; i < Side; i++)
        {
            for (int j = 0; j < Side; j++)
            {
                GameObject stickerObject = Instantiate
                    (textStickerPrefab, TilesPositions[i, j], Quaternion.identity);
                Sticker sticker = stickerObject.GetComponent<Sticker>();
                stickers.Add(sticker);
                sticker.StartPosition = sticker.transform.position;
                sticker.Color = Square.GameMatrix[i, j];
            }
        }
    }

    public override void CreateAdditionalSticker(float x, float y, int color, Vector3 startPos)
    {
        GameObject sticker = Instantiate
            (textStickerPrefab, new Vector3(x, y, 0f), Quaternion.identity);
        sticker.GetComponent<Sticker>().Color = color;
        sticker.GetComponent<Sticker>().StartPosition = startPos;
    }
}
