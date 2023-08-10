using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{

    public Block[] shapes;
    GameBoard gameBoard;
    int numberOfBlocksSpawned = 0;
    int[] blocks;
    // Start is called before the first frame update
    void Awake()
    {
        
        gameBoard = GameObject.FindWithTag("GameBoard").GetComponent<GameBoard>();
        Vector3 newPosition = new Vector3Int((LevelManager.Instance.LevelData.width / 2), LevelManager.Instance.LevelData.height + 1, 0);
        // Set the position of the "BlockSpawner" GameObject.
        Debug.Log("newposition" + newPosition);
        transform.position = newPosition;
        blocks = LevelManager.Instance.LevelData.blocks;
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Block SpawnRandomBlock()
    {
        
        Block randomShape = null;
        if (blocks != null && blocks.Length  > numberOfBlocksSpawned && shapes[blocks[numberOfBlocksSpawned]] != null)
        {
            randomShape = Instantiate(shapes[blocks[numberOfBlocksSpawned]], transform.position, Quaternion.identity) as Block;
        }
        else
        {  
            int random = Random.Range(0, shapes.Length);
            if(shapes[random] != null)
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
