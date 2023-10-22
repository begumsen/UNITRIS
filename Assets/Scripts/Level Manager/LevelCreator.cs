using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] GameObject inputs;
    string levelName;
    int levelWidth;
    int levelHeight;
    TMP_InputField customName;
    TMP_InputField customWidth;
    TMP_InputField customHeight;
    bool isInitialized = false;
    int customLevelIndex;
    LevelData customLevelData;

    // Start is called before the first frame update
    void Start()
    {
        if (!isInitialized)
        {
            isInitialized = true;
            DontDestroyOnLoad(gameObject);
            customName = inputs.transform.Find("CustomName").GetComponent<TMP_InputField>();
            customWidth = inputs.transform.Find("CustomWidth").GetComponent<TMP_InputField>();
            customHeight = inputs.transform.Find("CustomHeight").GetComponent<TMP_InputField>();
            
        } else
        {
            Destroy(gameObject);
        }
       
    }


    public void CreateButtonIsClicked()
    {
        levelName = customName.text;
        levelWidth = int.Parse(customWidth.text);
        levelHeight = int.Parse(customHeight.text);
        Debug.Log(levelName + " " + levelWidth + " " + levelHeight);
        CreateAndChangeLevelData();
    }

    void CreateAndChangeLevelData()
    {
        customLevelData = CreateNewLevelData();
        LevelManager.Instance.LevelData = customLevelData;
        GameFlowManager.GoTo(GameFlowName.CustomizeLevel);

    }

    LevelData CreateNewLevelData()
    {
        customLevelIndex = GetAndChangeCustomLevelIndex();
        Debug.Log(customLevelIndex);

        LevelData newLevelData = new LevelData();
        newLevelData.level = customLevelIndex;
        newLevelData.width = levelWidth;
        newLevelData.height = levelHeight;

        newLevelData.colors = new string[newLevelData.width, newLevelData.height];
        for (int x = 0; x < newLevelData.width; x++)
        {
            for (int y = 0; y < newLevelData.height; y++)
            {
                newLevelData.colors[x, y] = "-1"; 
            }
        }

        return newLevelData;
    }

    int GetAndChangeCustomLevelIndex()
    {
        int customLevelIndex = PlayerPrefs.GetInt("CustomLevelIndex", -1);
        PlayerPrefs.SetInt("CustomLevelIndex", customLevelIndex - 1);
        return customLevelIndex;
    }

    public void LevelCreationIsFinished()
    {
        GameBoard gameBoard = GameObject.FindWithTag("GameBoard").GetComponent<GameBoard>();
    }

    void SaveTheCustomLevel()
    {
        GetTheColorsFromBoard();


    }

    void GetTheColorsFromBoard()
    {
        GameBoard gameBoard = GameObject.FindWithTag("GameBoard").GetComponent<GameBoard>();

        Transform[,] board = gameBoard.board;
        int rows = board.GetLength(0);
        int cols = board.GetLength(1);
        string[,] colorsInHex = new string[rows, cols];

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                Transform objectTransform = board[row, col];
                SpriteRenderer spriteRenderer = objectTransform.GetComponent<SpriteRenderer>();

                if (spriteRenderer != null)
                {
                    // Get the color from the Renderer
                    Color color = spriteRenderer.color;

                    // Convert the color to a hex string
                    string hexColor = ColorToHex(color);

                    // Store the hex color in the string array
                    colorsInHex[row, col] = hexColor;
                }
            }
        }
        customLevelData.colors = colorsInHex;
    }

    string ColorToHex(Color color)
    {
        Color32 color32 = (Color32)color;
        return "#" + color32.r.ToString("X2") + color32.g.ToString("X2") + color32.b.ToString("X2");
    }

}


    