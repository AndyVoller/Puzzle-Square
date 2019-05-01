using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareManager : MonoBehaviour
{
    #region Singleton
    public static SquareManager instance;

    void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
                Destroy(gameObject);
        }
    }
    #endregion

    [SerializeField] protected GameObject[] stickerPrefabs;

    [SerializeField] private GameObject[] tiles;            // Stickers' positions
    public Vector3[,] TilesPositions { get; private set; }

    [SerializeField] protected int[] colors;                // Stickers' colors
    public int[] Colors { get { return colors; } }

    [SerializeField] protected int side;                    // Side of square
    public int Side { get { return side; } }

    public ISquare Square { get; set; }
    protected List<Sticker> stickers;

    void Start ()
    {
        CreateSquare();
        stickers = new List<Sticker>();

        // Get positions for stickers
        TilesPositions = new Vector3[Side, Side];
        for (int i = 0; i < Side; i++)
        {
            for (int j = 0; j < Side; j++)
            {
                TilesPositions[i, j] = tiles[Side * i + j].transform.position;
            }
        }

           Square.Rotate();

        StickersController.TileSize = TilesPositions[0, 1].x - TilesPositions[0, 0].x;
	}

    public virtual void CreateSquare()
    {
        Square = new Square();
        Square.CreateGameMatrix();
    }

    public void Rotation()
    {
        // Renew stickers after start rotation

        foreach (Sticker sticker in stickers)
            if(sticker!=null)
                Destroy(sticker.gameObject);

        CreateStickers();
    }

    public virtual void CreateStickers()
    {
        for (int i = 0; i < Side; i++)
        {
            for (int j = 0; j < Side; j++)
            {
                GameObject stickerObject = Instantiate
                    (stickerPrefabs[Square.GameMatrix[i, j]], TilesPositions[i, j], Quaternion.identity);
                Sticker sticker = stickerObject.GetComponent<Sticker>();
                stickers.Add(sticker);
                sticker.StartPosition = sticker.transform.position;
                sticker.Color = Square.GameMatrix[i, j];
            }
        }
    }

    // Fill up empty space when user moves stickers
    public void AddStickers(List<Sticker> line)
    {
        float posX = 0f, posY = 0f;                                     // New sticker's position
        int color = -1;                                                 // New sticker's color
        Vector3 startPos = Vector3.zero;                                // New sticker's start position

        Vector3 firstStickerPos = line[0].gameObject.transform.position;    // First sticker in line
        Vector3 lastStickerPos = line[line.Count - 1].gameObject.transform.position;// Last sticker in line
        float tileSize = StickersController.TileSize;

        if (StickersController.Horizontal)                              // Horizontal movement
        {    
            if (firstStickerPos.x > TilesPositions[0, 0].x)              // Need new sticker at the left
            {
                posX = firstStickerPos.x - tileSize;
                posY = firstStickerPos.y;
                startPos = line[0].StartPosition - StickersController.Direction * tileSize;

                if (!line[line.Count - 1].IsDestroyed)
                    color = line[line.Count - 1].Color;
                else color = line[line.Count - 2].Color;
            }
            else
            if (lastStickerPos.x < TilesPositions[0, Side - 1].x)      // Need new sticker at the right
            {
                posX = lastStickerPos.x + tileSize;
                posY = lastStickerPos.y;
                startPos = line[line.Count - 1].StartPosition + StickersController.Direction * tileSize;

                if (!line[0].IsDestroyed)
                    color = line[0].Color;
                else color = line[1].Color;
            }

        }
        else                                                            // Vertical movement
        {
            if (firstStickerPos.y > TilesPositions[Side - 1, 0].y)       // Need new sticker at the down
            {
                posX = firstStickerPos.x;
                posY = firstStickerPos.y - tileSize;
                startPos = line[0].StartPosition - StickersController.Direction * tileSize;

                if (!line[line.Count - 1].IsDestroyed)
                    color = line[line.Count - 1].Color;
                else color = line[line.Count - 2].Color;
            }
            else
            if (lastStickerPos.y < TilesPositions[0, 0].y)                // Need new sticker at the top
            {
                posX = lastStickerPos.x;
                posY = lastStickerPos.y + tileSize;
                startPos = line[line.Count - 1].StartPosition + StickersController.Direction * tileSize;

                if (!line[0].IsDestroyed)
                    color = line[0].Color;
                else color = line[1].Color;
            }
        }

        if (color != -1)
            CreateAdditionalSticker(posX, posY, color, startPos);

    }

    public virtual void CreateAdditionalSticker(float x, float y, int color, Vector3 startPos)
    {
        GameObject sticker = Instantiate
            (stickerPrefabs[color], new Vector3(x, y, 0f), Quaternion.identity);

        sticker.GetComponent<Sticker>().StartPosition = startPos;
    }

    // Destroy stickers if they are outside the square
    public void DestroyExcessiveStickers(List<Sticker> stickers)
    {
        foreach (Sticker sticker in stickers)
        {
            Vector3 position = sticker.gameObject.transform.position;
            float tileSize = StickersController.TileSize;

            if(StickersController.Horizontal)
            {
                float minX = TilesPositions[0, 0].x - tileSize;
                float maxX = TilesPositions[0, Side - 1].x + tileSize;

                if (position.x <= minX || position.x >= maxX)
                {
                    sticker.IsDestroyed = true;
                    Destroy(sticker.gameObject);
                }
            }
            else
            {
                float minY = TilesPositions[Side - 1, 0].y - tileSize;
                float maxY = TilesPositions[0, 0].y + tileSize;

                if (position.y <= minY || position.y >= maxY)
                {
                    sticker.IsDestroyed = true;
                    Destroy(sticker.gameObject);
                }
            }
        }
    }
}
