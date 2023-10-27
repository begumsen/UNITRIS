using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class LevelManager : EventInvoker
{
    public static LevelManager Instance { get; private set; }
    LevelData levelData;

    int previousHighScore = 0;
    int levelGoal = 0;
    int finalScore = 0;
    int currentLevel;
    public DataManager dataManager;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            EventManager.AddListener(EventName.LevelSelected,
            HandleLevelSelectedEvent);
            EventManager.AddListener(EventName.ScoreFinalized,
            HandleScoreFinalized);
            events.Add(EventName.GoalPassed, new GoalPassedEvent());
            EventManager.AddInvoker(EventName.GoalPassed, this);
            events.Add(EventName.GoalNotPassed, new GoalNotPassedEvent());
            EventManager.AddInvoker(EventName.GoalNotPassed, this);
            dataManager = new DataManager();
        } else
        {
            Destroy(gameObject);
        }
    }


    void HandleLevelSelectedEvent(int levelNo)
    {
        currentLevel = levelNo;
        levelData = dataManager.getLevelData(levelNo);
        previousHighScore = dataManager.getPreviousHighScore(levelNo);
        levelGoal = dataManager.getGoal(levelNo);
        GameFlowManager.GoTo(GameFlowName.GamePlay);
    }

    void HandleScoreFinalized (int score)
    {
        finalScore = score;
        if (score >= levelGoal)
        {
            HandleGoalPassedEvent(score);
            events[EventName.GoalPassed].Invoke(0);
        }
        else if (score < levelGoal)
        {
            events[EventName.GoalNotPassed].Invoke(0);
        }
    }

    public void HandleGoalPassedEvent(int score)
    {
        if (score > previousHighScore) {
          dataManager.ChangeAndSaveHighScore(currentLevel, score); 
        }
        if(currentLevel == InitialLockedLevel - 1)
        {
          dataManager.IncrementInitialLockedLevel();
        }
    }

    public LevelData LevelData
    {
        set
        {
            this.levelData = value;
        }
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

    public int Goal
    {
        get
        {
            return levelGoal;
        }
    }

    public int FinalScore
    {
        get
        {
            return finalScore;
        }
    }

    public int InitialLockedLevel
    {
        get
        {
            return dataManager.initialLockedLevel;
        }
    }
}
