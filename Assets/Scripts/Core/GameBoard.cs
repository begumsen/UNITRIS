using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : EventInvoker
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
    public Transform [,] colorBoard;

    ParticleController particleController;

    LevelData level;

    void Awake()
    {
        particleController = new ParticleController();
        level = LevelManager.Instance.LevelData;
        width = level.width;
        height = level.height + marginBlockY;
        
    }


    void Start()
    {
        events.Add(EventName.PointsAdded, new PointsAddedEvent());
        EventManager.AddInvoker(EventName.PointsAdded, this);
        events.Add(EventName.Damage, new DamageEvent());
        EventManager.AddInvoker(EventName.Damage, this);
        events.Add(EventName.GameOver, new GameOverEvent());
        EventManager.AddInvoker(EventName.GameOver, this);
        board = new Transform[width, height];
        colorBoard = new Transform[level.width, level.height];
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
            spriteRenderer.size = new Vector2(width + 0.5f, height - marginBlockY + 4 + 0.5f);
            //ColorTheBlock(border.transform, "#282828", 0.4f);

            for (int y = 0; y < height-marginBlockY; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    GameObject newSquare = Instantiate(roundedSquare, new Vector3(x, y, 8), Quaternion.identity) as GameObject;
                    colorBoard[x, y] = newSquare.transform;
                    Debug.Log("y: " + y + " x: " + x);
                    if (level.colors[y,x] == "-1" || level.colors[y, x] == "#484848")
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
        Debug.Log("create listener");
        EventManager.TriggerEvent(EventName.BoardIsFinalized,0);

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
            if (block.position.y >= height - marginBlockY)
            {
                events[EventName.GameOver].Invoke(1);
                return true;
            }
        }
        if (isPatternFull())
        {
            events[EventName.GameOver].Invoke(1);
            return true;
        }
        
        return false;
    }


    public bool isPatternFull()
    {
        for (int y = 0; y < height - marginBlockY; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (board[x, y] == null && level.colors[y, x] != "-1")
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
            CheckIfCorrectlyPlaced(block);
        }
        
    }

    public void CheckIfCorrectlyPlaced (Transform block)
    {
        Vector3 blockPosition = block.transform.position;
        int x = Mathf.RoundToInt(blockPosition.x);
        int y = Mathf.RoundToInt(blockPosition.y);
        board[x, y] = block;

        if (!(y >= height - marginBlockY) && level.colors[y, x] != "-1" && board[x, y] != null)
        {
            particleController.PlaceParticleFX(x, y, true);
            ColorTheBlock(board[x, y], level.colors[y, x], 1f);
            events[EventName.PointsAdded].Invoke(1);
            //gain point
        } else
        {
            if(!(y >= height - marginBlockY))
            {
                particleController.PlaceParticleFX(x, y, false);
                events[EventName.Damage].Invoke(1);

                // lose point
            }

        }
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
