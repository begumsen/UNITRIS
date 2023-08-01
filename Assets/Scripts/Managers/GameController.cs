using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    // reference to game board
    GameBoard gameBoard;

    // reference to block spawner 
    BlockSpawner spawner;

    // currently block
    Block currentBlock;

    float moveDownRate = 1f;
    float deltaMoveDown = 0;
    
    //make the object directly fall
    bool forceFall = false;

    //make the game end
    bool gameOver = false;

    // Start is called before the first frame update
    void Awake()
    {
        

    }
    void Start()
    {
        SoundManager.PlayBackgroundMusic();
        gameBoard = GameObject.FindWithTag("GameBoard").GetComponent<GameBoard>();
        spawner = GameObject.FindWithTag("BlockSpawner").GetComponent<BlockSpawner>();
        currentBlock = spawner.SpawnRandomBlock();
        
    }

    // Update is called once per frame
    void Update()
    {
        deltaMoveDown += Time.deltaTime;

        if (currentBlock != null && !gameOver && gameBoard != null && spawner != null)
        {
            PlayerInput();
        }
    

    }

    void MakeTheBlockFall()
    {
        if (gameBoard.IsValidMove(currentBlock, new Vector2(0, -1)))
        {
            currentBlock.MoveDown();
            deltaMoveDown = 0;
        }
        else
        {
            gameBoard.PlaceBlockToBoard(currentBlock);
            if (gameBoard.IsGameOver(currentBlock))
            {
                gameOver = true;
                SoundManager.PlayFX(SoundName.GameOverSound);
            }
            else
            {
                forceFall = false;
                currentBlock = spawner.SpawnRandomBlock();
                deltaMoveDown = 0;
            } 
        }
    }

  

    void PlayerInput()
    {
        if (!forceFall)
        {
            if (Input.GetButtonDown("MoveRight"))
            {
                if (gameBoard.IsValidMove(currentBlock, new Vector2(1, 0)))
                {
                    currentBlock.MoveRight();
                    SoundManager.PlayFX(SoundName.MoveSound);
                }
            }
            else if (Input.GetButtonDown("MoveLeft"))
            {

                if (gameBoard.IsValidMove(currentBlock, new Vector2(-1, 0)))
                {
                    currentBlock.MoveLeft();
                    SoundManager.PlayFX(SoundName.MoveSound);
                }
            }
            else if (Input.GetButtonDown("Rotate") && currentBlock.canRotate)
            {
                currentBlock.RotateRight();
                if (!gameBoard.IsValidMove(currentBlock, new Vector2(0f, 0f)))
                {
                    currentBlock.RotateLeft();
                }else
                {
                    SoundManager.PlayFX(SoundName.MoveSound);
                }
            }
            else if (deltaMoveDown >= moveDownRate)
            {
                
                MakeTheBlockFall();

            }
            else if (Input.GetButtonDown("MoveDown") && !forceFall)
            {
                forceFall = true;
                MakeTheBlockFall();

            }
        }
        
        else if (forceFall && deltaMoveDown >= moveDownRate / 50f)
        {
            MakeTheBlockFall();
        }
    }
}
