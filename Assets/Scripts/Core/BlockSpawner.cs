using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{

    public Block[] shapes;
    GameBoard gameBoard;
    int numberOfBlocksSpawned = 0;
    int[] blocks;
    public Transform nextBlockHolder;
    Block nextBlock;
    Vector3 holderPosition;
    private float initialCameraSize;
    float scaleFactor;

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("here0");
        gameBoard = GameObject.FindWithTag("GameBoard").GetComponent<GameBoard>();
        Vector3 newPosition = new Vector3Int((LevelManager.Instance.LevelData.width / 2), LevelManager.Instance.LevelData.height + 1, 0);
        transform.position = newPosition;
        blocks = LevelManager.Instance.LevelData.blocks;
        Camera mainCamera = Camera.main;
        initialCameraSize = mainCamera.orthographicSize;

    }

    void Start()
    {
        Debug.Log("here1");
        Vector3 screenPosition = nextBlockHolder.transform.position;
        Camera mainCamera = Camera.main;
        holderPosition = mainCamera.ScreenToWorldPoint(screenPosition);
        scaleFactor = mainCamera.orthographicSize / initialCameraSize;
        holderPosition.z = 7;
        holderPosition.x -= 1* scaleFactor;
        InitNextBlock();
    }

    void InitNextBlock()
    {
        nextBlock = GetARandomShape();
        nextBlock.transform.position = holderPosition;
       
        // Apply the scaling factor to the object's scale
        nextBlock.transform.localScale = new Vector3(0.5f * scaleFactor, 0.5f * scaleFactor, 1f);

    }

    Block GetNextBlock()
    {
        Block firstBlock = null;
        if (nextBlock)
        {
            firstBlock = nextBlock;
        }
        InitNextBlock();
        return firstBlock;
    }
    
    public Block SpawnRandomBlock()
    {
        
        Block randomBlock = null;
        randomBlock = GetNextBlock();
        randomBlock.transform.position = transform.position;
        randomBlock.transform.localScale = Vector3.one;
        return randomBlock;

    }

    Block GetARandomShape()
    {
        Block randomShape = null;
        if (blocks != null && blocks.Length > numberOfBlocksSpawned && shapes[blocks[numberOfBlocksSpawned]] != null)
        {
            randomShape = Instantiate(shapes[blocks[numberOfBlocksSpawned]], transform.position, Quaternion.identity) as Block;
        }
        else
        {
            int random = Random.Range(0, shapes.Length);
            if (shapes[random] != null)
            {
                randomShape = Instantiate(shapes[random], transform.position, Quaternion.identity) as Block;
            }
        }

        if (randomShape)
        {
            numberOfBlocksSpawned++;
            return randomShape;
        }
        else return null;
    }
}
