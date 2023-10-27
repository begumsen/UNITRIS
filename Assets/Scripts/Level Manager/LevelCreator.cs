using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] GameObject inputs;
    string levelName;
    int levelWidth;
    int levelHeight;
    int levelGoal;
    TMP_InputField customName;
    TMP_InputField customWidth;
    TMP_InputField customHeight;
    TMP_InputField customGoal;
    bool isInitialized = false;
    int customLevelIndex;
    LevelData customLevelData;
    DataManager dataManager;
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
            customGoal = inputs.transform.Find("CustomGoal").GetComponent<TMP_InputField>();
            dataManager = new DataManager();
            EventManager.AddListener(EventName.CustomLevelFinalized, SaveTheCustomLevel);


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
        levelGoal = int.Parse(customGoal.text);
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
        newLevelData.name = levelName;
        newLevelData.blocks = new int[] {};
        newLevelData.colors = new string[newLevelData.height, newLevelData.width];
        for (int y = 0; y < newLevelData.height; y++) 
        {
            for (int x = 0; x < newLevelData.width; x++)
            {
                newLevelData.colors[y, x] = "-1"; 
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

    public void SaveTheCustomLevel(int a)
    {
        GetAndSetTheColorsFromBoard();
        SaveTheLevelToPersisentPath();
        GameFlowManager.GoTo(GameFlowName.MainMenu);
    }

    void GetAndSetTheColorsFromBoard()
    {
        GameBoard gameBoard = GameObject.FindWithTag("GameBoard").GetComponent<GameBoard>();

        Transform[,] colorBoard = gameBoard.colorBoard;
        int rows = colorBoard.GetLength(0); //width
        int cols = colorBoard.GetLength(1); //height
        Debug.Log("rows: " + rows + " cols: " + cols);
        string[,] colorsInHex = new string[cols, rows];

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                Debug.Log("row: " + row + " col: " + col);
                Transform objectTransform = colorBoard[row, col];
                if (objectTransform == null) Debug.Log("objectTransform is null");
                SpriteRenderer spriteRenderer = objectTransform.GetComponent<SpriteRenderer>();

                if (spriteRenderer != null)
                {
                    // Get the color from the Renderer
                    Color color = spriteRenderer.color;

                    // Convert the color to a hex string
                    string hexColor = ColorToHex(color);
                    Debug.Log("hexColor"+ hexColor);

                    // Store the hex color in the string array
                    colorsInHex[col, row] = hexColor;

                }
            }
        }
        customLevelData.colors = colorsInHex;
    }

    void SaveTheLevelToPersisentPath()
    {
        dataManager.SaveTheNewCustomLevel(customLevelData, levelGoal);
    }

    string ColorToHex(Color color)
    {
        Color32 color32 = (Color32)color;
        return "#" + color32.r.ToString("X2") + color32.g.ToString("X2") + color32.b.ToString("X2");
    }

}


    