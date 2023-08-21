using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class LevelManager : MonoBehaviour
{
    public string fileName = "Level1";
    public static LevelManager Instance { get; private set; }
    LevelData levelData;
    public LevelList levelList = new LevelList();
    string levelJson;
    string filePath;
    public int previousHighScore = 0;
    public int goal = 0;
    private bool levelCompletionHandled = false;
    bool isHighScore = false;
    int initialLockedLevel;
    int currentLevel;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            EventManager.AddListener(EventName.LevelSelected,
            HandleLevelSelectedEvent);
            EventManager.AddListener(EventName.GoalPassed,
            HandleGoalPassedEvent);
            EventManager.AddListener(EventName.GoalNotPassed,
            HandleGoalNotPassedEvent);

            filePath = Path.Combine(Application.persistentDataPath, "levelJson.txt");
            levelJson = File.ReadAllText(filePath);
            levelList = JsonUtility.FromJson<LevelList>(levelJson);
            initialLockedLevel = levelList.initialLockedLevel;
        } else
        {
            Destroy(gameObject);
        }
        
        
       
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu" && levelCompletionHandled)
        {
            levelCompletionHandled = false;
            // Show celebration particles and animation
            if (isHighScore)
            {
                //celebration
                Debug.Log("new highscore");
                isHighScore = false;
                GameFlowManager.GoTo(GameFlowName.Levels);
            } else
            {
                GameFlowManager.GoTo(GameFlowName.Levels);
            }
        }
    }

    private void OnEnable()
    {
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe from the sceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void HandleLevelSelectedEvent(int levelNo)
    {
        currentLevel = levelNo;
        levelData = ReadLevelDataFromFile("Level"+ levelNo);
        previousHighScore = levelList.levels[levelNo].highScore;
        goal = levelList.levels[levelNo].goal;
        GameFlowManager.GoTo(GameFlowName.GamePlay);
    }

    void HandleGoalNotPassedEvent(int score)
    {
        GameFlowManager.GoTo(GameFlowName.GamePlay);

    }

    public void HandleGoalPassedEvent(int score)
    {

        // Only handle level completion once
        if (!levelCompletionHandled)
        {
            levelCompletionHandled = true;
            if (score > previousHighScore) {
                // change the previous highscore in json
                ChangeAndSaveHighScore(currentLevel, score); // Change high score of levelNo = 0 to 15
                isHighScore = true;
            }
            GameFlowManager.GoTo(GameFlowName.MainMenu);
        }
    }

    private void ChangeAndSaveHighScore(int levelNo, int newHighScore)
    {
        if (levelNo >= 0 && levelNo < levelList.levels.Count)
        {
            levelList.levels[levelNo].highScore = newHighScore;
        }
        string updatedJson = JsonUtility.ToJson(levelList, true);
        File.WriteAllText(filePath, updatedJson);
    }


    public LevelData ReadLevelDataFromFile(string fileName)
    {
        LevelData levelData = new LevelData();

        
        try
        {
            // Load the text file from Resources folder (make sure the file is in Assets/Resources)
            TextAsset textAsset = Resources.Load<TextAsset>(fileName);
            // Split the text asset content by newline characters to separate each line
            string[] lines = textAsset.text.Split('\n');
            int levelValue = 0;
            int widthValue = 0;
            int heightValue = 0;
            // Parse each line to extract the level information
            foreach (string line in lines)
            {
                string[] tokens = line.Split(':');
                if (tokens.Length == 2)
                {
                    string key = tokens[0].Trim().ToLower();
                    string value = tokens[1].Trim();

                    // Parse the values based on the keys
                    switch (key)
                    {
                        case "level":
                            if (int.TryParse(value, out levelValue))
                                levelData.level = levelValue;
                            break;
                        case "width":
                            if (int.TryParse(value, out widthValue))
                                levelData.width = widthValue;
                            break;
                        case "height":
                            if (int.TryParse(value, out heightValue))
                                levelData.height = heightValue;
                            break;
                        case "colors":
                            levelData.colors = CreateColorsArray(value, widthValue, heightValue);
                            break;
                        case "blocks":
                            levelData.blocks = CreateBlocksArray(value);
                            break;
                        default:
                            // Handle any other keys if necessary
                            break;
                    }
                }
            }
        }
        catch
        {
            Debug.LogWarning("Level data file not found: " + fileName);
            levelData.level = 0;
            levelData.width = 4;
            levelData.height = 7;
            string color = "-1,-1,#424242,-1,-1,-1,#f45dab,-1,-1,#f45dab,#f487c1,#f45dab,-1,#f487c1,#f45dab,#f487c1,-1,#f45dab,-1,-1,-1,#f45dab,-1,-1,#666666,#f45dab,-1,-1";
            levelData.colors = CreateColorsArray(color, levelData.width, levelData.height);
            string blocks = "2,9,8,1";
            levelData.blocks = CreateBlocksArray(blocks);
        }


        return levelData;
    }

    public string[,] CreateColorsArray(string colorsInfo, int width, int height)
    {
        // Split the colorsInfo by commas to get individual color strings
        string[] colorStrings = colorsInfo.Split(',');

        string[,] levelArray = new string[width, height];

        int index = 0;

        // Fill the levelArray based on the color strings
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // Convert the color string to an integer representation
                levelArray[x, y] = colorStrings[index];
                index++;
            }
        }

        return levelArray;
    }

    public int[] CreateBlocksArray(string blocksInfo)
    {
        // Split the blocksInfo by commas to get individual color strings
        string[] blocksStrings = blocksInfo.Split(',');

        int[] blocks = new int[blocksStrings.Length];
        for (int i = 0; i < blocksStrings.Length; i++)
        {
            int.TryParse(blocksStrings[i], out blocks[i]);
        }

        return blocks;
    }

    public LevelData LevelData
    {
        get
        {
            return this.levelData;
        }
    }

    public LevelList LevelList
    {
        get
        {
            return LevelList;
        }
    }
}
