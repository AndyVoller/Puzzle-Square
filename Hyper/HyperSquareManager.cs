using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperSquareManager : SquareManager
{
    public override void CreateSquare()
    {
        Square = new HyperSquare();
        Square.CreateGameMatrix();
    }

    public override void CreateStickers()
    {
        for (int i = 0; i < Side; i++)
        {
            for (int j = 0; j < Side; j++)
            {
                if (Square.GameMatrix[i, j] == -1)
                    continue;

                GameObject stickerObject = Instantiate
                    (stickerPrefabs[Square.GameMatrix[i, j]], TilesPositions[i, j], Quaternion.identity);
                Sticker sticker = stickerObject.GetComponent<Sticker>();
                stickers.Add(sticker);
                sticker.StartPosition = sticker.transform.position;
                sticker.Color = Square.GameMatrix[i, j];
            }
        }
    }

}
