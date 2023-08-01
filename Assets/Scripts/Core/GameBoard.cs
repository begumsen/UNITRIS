using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{

    int height = 18;
    int width = 10;
    int marginBlockY = 6;

    //1x1 empty square 
    public GameObject emptySquare;
    //1x1 filled square 
    public GameObject filledSquare;
    //1x1 rounded square 
    public GameObject roundedSquare;

    // grid of Objects
    public Transform [,] board;

    LevelData level;

    //example pattern of House
    int[,] housePattern =
        {   
            {-1,  0,  0,  0, -1, -1,  0,  0,  0, -1},
            {-1,  0,  0,  0, -1, -1,  0,  0,  0, -1},
            {-1,  0,  0,  0, -1, -1,  0,  0,  0, -1},
            {-1,  0,  0,  0,  0,  0,  0,  0,  0, -1},
            {-1,  0, -1, -1,  0,  0, -1, -1,  0, -1},
            {-1,  0, -1, -1,  0,  0, -1, -1,  0, -1},
            {-1,  0,  0,  0,  0,  0,  0,  0,  0, -1},
            {-1, -1,  0,  0,  0,  0,  0,  0, -1, -1},
            {-1, -1, -1,  0,  0,  0,  0, -1, -1, -1},
            {-1, -1, -1, -1,  0,  0, -1, -1, -1, -1}
        };

    void Awake()
    {
        level = LevelDataManager.Instance.LevelData;
        width = level.width;
        height = level.height + marginBlockY;
        
    }


    void Start()
    {
        
        board = new Transform[width, height];
        Debug.Log("width " + width + "height " + height);
        BuildBoard();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    void BuildBoard()
    {
       
        if (emptySquare != null && filledSquare != null && roundedSquare != null)
        {
            // the background of the board
            GameObject background = Instantiate(filledSquare, new Vector3(width/2f - 0.5f, (height - marginBlockY + 4) / 2f -0.5f, 10), Quaternion.identity) as GameObject;
            background.transform.localScale = new Vector3(width + 0.3f, height - marginBlockY + 4 + 0.3f, 1);
            ColorTheBlock(background.transform, "#282828", 1f);
            // the border of the board
            GameObject border = Instantiate(emptySquare, new Vector3(width / 2f - 0.5f, (height - marginBlockY + 4) / 2f - 0.5f, 10), Quaternion.identity) as GameObject;
            SpriteRenderer spriteRenderer = border.GetComponent<SpriteRenderer>();
            spriteRenderer.size = new Vector2(width + 0.3f, height - marginBlockY + 4 + 0.3f);
            //ColorTheBlock(border.transform, "#282828", 0.4f);

            for (int y = 0; y < height-marginBlockY; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    GameObject newSquare = Instantiate(roundedSquare, new Vector3(x, y, 8), Quaternion.identity) as GameObject;
                    if (level.colors[x,y] == "-1" )
                    {
                        ColorTheBlock(newSquare.transform, "#484848", 1f);
                    } else
                    {
                        ColorTheBlock(newSquare.transform, "#181818", 1f);
                    }
                    // make the Board object parent

                    newSquare.transform.parent = transform;

                }
            }
        }
       

    }

    void ColorTheBlock(Transform block, string color, float alpha)
    {
        
        SpriteRenderer spriteRenderer = block.GetComponent<SpriteRenderer>();
        Color newColor;
        ColorUtility.TryParseHtmlString(color, out newColor);
        newColor.a = alpha;
        if (spriteRenderer != null)
        {
            // Change the color of the sprite
            spriteRenderer.color = newColor;
        }
        
    }

    public bool IsValidMove(Block blocks, Vector2 direction)
    {
        
        foreach (Transform block in blocks.transform)

        {
            Vector3 blockPosition = block.position;
            Vector2Int pos = new Vector2Int(Mathf.RoundToInt(blockPosition.x), Mathf.RoundToInt(blockPosition.y));
            // check if the block will be in the boundries of the board
            if (!((int)(pos.x + direction.x) >= 0 && (int)(pos.x + direction.x) < width && (int)(pos.y + direction.y) >= 0))
            {
                return false;

            }
            
            if (board[(int)(pos.x + direction.x), (int)(pos.y + direction.y)] != null && board[(int)(pos.x + direction.x), (int)(pos.y + direction.y)].parent != block.transform)
            {
                return false;
            }

        }

        return true;

    }

    public bool IsGameOver(Block blocks)
    {
        
        foreach (Transform block in blocks.transform)

        {
            if (block.position.y >= height-marginBlockY) return true;
        }
        if (isPatternFull()) return true;
        
        return false;
    }


    public bool isPatternFull()
    {
        for (int y = 0; y < height - marginBlockY; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (board[x, y] == null && level.colors[x, y] != "-1")
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void PlaceBlockToBoard(Block blocks)
    {
        foreach (Transform block in blocks.transform)

        {
            Vector3 blockPosition = block.transform.position;
            int x = Mathf.RoundToInt(blockPosition.x);
            int y = Mathf.RoundToInt(blockPosition.y);

            board[x,y] = block;
            //y < level.colors[x,0].Length && 
            if ( !(y >= height - marginBlockY) && level.colors[x,y] != "-1" && board[x,y] != null)
            {
                ColorTheBlock(board[x, y], level.colors[x, y], 1f);
            }
        }
        
    }

    public void CheckIfCorrectlyPlaced (Transform block)
    { 

    }

    public int Height
    {
        get
        {
            return height - marginBlockY; 
        }
        set
        {
            height = value; 
        }
    }

    public int Width
    {
        get
        {
            return width;
        }
        set
        {
            width = value;
        }
    }
}
