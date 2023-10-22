using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class DataManager
{
    private LevelList levelList;
    private string filePath;

    public DataManager()
    {
        filePath = Path.Combine(Application.persistentDataPath, "levelJson.txt");
        LoadLevelList();
    }

    private void LoadLevelList()
    {
        string levelJson = File.ReadAllText(this.filePath);
        this.levelList = JsonUtility.FromJson<LevelList>(levelJson);
    }

    public LevelList levelsList
    {
        get {
            return this.levelList;
        }
    
    }

    public int initialLockedLevel
    {
        get
        {
            return levelList.initialLockedLevel;
        }
    }

    public int getGoal(int levelNo)
    {
       return levelList.levels[levelNo].goal;
    }

    public int getPreviousHighScore(int levelNo)
    {
        return levelList.levels[levelNo].highScore;
    }

    public void ChangeAndSaveHighScore(int levelNo, int newHighScore)
    {
        if (levelNo >= 0 && levelNo < levelList.levels.Count)
        {
            levelList.levels[levelNo].highScore = newHighScore;
        }
        string updatedJson = JsonUtility.ToJson(levelList, true);
        File.WriteAllText(filePath, updatedJson);
    }

    public void IncrementInitialLockedLevel()
    {
        levelList.initialLockedLevel++;
        string updatedJson = JsonUtility.ToJson(levelList, true);
        File.WriteAllText(filePath, updatedJson);
    }

    public LevelData getLevelData(string fileName)
    {
        return ReadLevelDataFromFile(fileName);
    }

    public LevelData ReadLevelDataFromFile(string fileName)
    {
        LevelData levelData = new LevelData();


        try
        {
            Debug.Log(fileName);
            // Load the text file from Resources folder (make sure the file is in Assets/Resources)
            TextAsset textAsset = Resources.Load<TextAsset>("Levels/"+fileName);
            if (textAsset != null) Debug.Log("document "+ fileName +"found");
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
            Debug.Log(levelData.level);
            Debug.Log(levelData.width);
            Debug.Log(levelData.height);
            Debug.Log(levelData.colors);


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

}