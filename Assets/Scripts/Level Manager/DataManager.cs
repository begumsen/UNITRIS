using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using Newtonsoft.Json;
using System;

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


    void SaveLevelList()
    {
        string updatedJson = JsonUtility.ToJson(levelList, true);
        File.WriteAllText(filePath, updatedJson);
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

    Level GetTheLevelWithNo(int levelNo)
    {
        LoadLevelList();
        Level level = levelList.levels.Find(level => level.levelNo == levelNo.ToString());
        return level;
    }
    public int getGoal(int levelNo)
    {
        Level level = GetTheLevelWithNo(levelNo);
        return level.goal;
    }

    public int getPreviousHighScore(int levelNo)
    {

        Level level = GetTheLevelWithNo(levelNo);
        return level.highScore;
    }

    public void ChangeAndSaveHighScore(int levelNo, int newHighScore)
    {
        Level levelToUpdate = levelList.levels.Find(level => level.levelNo == levelNo.ToString());
        levelToUpdate.highScore = newHighScore;
        SaveLevelList();
    }

    public void IncrementInitialLockedLevel()
    {
        levelList.initialLockedLevel++;
        SaveLevelList();
    }

    public LevelData getLevelData(int levelNo)
    {
        string fileName = "Level" + levelNo;
        if(levelNo < 0)
        {
            return ReadLevelDataFromPersistentPath(fileName);
        }
        return ReadLevelDataFromResources(fileName);
    }


    LevelData ReadLevelDataFromPersistentPath(string fileName)
    {
        string levelFilePath = Path.Combine(Application.persistentDataPath, fileName);
        string levelDataJson = File.ReadAllText(levelFilePath);
        LevelData levelData = JsonConvert.DeserializeObject<LevelData>(levelDataJson);
        Debug.Log("levelData " + levelData.name);
        Debug.Log("levelData " + levelData.colors.Length);
        return levelData;
    }

    LevelData ReadLevelDataFromResources(string fileName)
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Levels/" + fileName);
        LevelData levelData = new LevelData();
        if (textAsset != null)
        {
            levelData = JsonConvert.DeserializeObject<LevelData>(textAsset.text);
        }

        return levelData;
    }

    public void SaveTheNewCustomLevel(LevelData levelData, int customGoal)
    {
        Level customLevel = new Level
        {
            levelNo = levelData.level.ToString(),
            goal = customGoal,
            name = levelData.name,
            highScore = 0
        };

        levelList.levels.Insert(0, customLevel);
        SaveLevelList();

        string customPath = Path.Combine(Application.persistentDataPath, "Level" + levelData.level);
        string updatedJson = JsonConvert.SerializeObject(levelData, Formatting.Indented);
        File.WriteAllText(customPath, updatedJson);
    }


}