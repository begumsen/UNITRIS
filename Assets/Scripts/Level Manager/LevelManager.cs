using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class LevelManager : MonoBehaviour
{
    string fileName = "Level1";
    public static LevelManager Instance { get; private set; }
    LevelData levelData;

    int previousHighScore = 0;
    public int goal = 0;
    private bool levelCompletionHandled = false;
    bool isHighScore = false;
    int initialLockedLevel;
    int currentLevel;
    public DataManager dataManager;
    bool isGameOver;

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
            dataManager = new DataManager();
        } else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu" && levelCompletionHandled && isGameOver )
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

    void HandleLevelSelectedEvent(int levelNo)
    {
        isGameOver = false;
        currentLevel = levelNo;
        levelData = dataManager.ReadLevelDataFromFile("Level"+ levelNo);
        previousHighScore = dataManager.getPreviousHighScore(levelNo);
        goal = dataManager.getGoal(levelNo);
        GameFlowManager.GoTo(GameFlowName.GamePlay);
        if (dataManager.initialLockedLevel > levelNo)
        {
            levelCompletionHandled = true;
        }
    }

    void HandleGoalNotPassedEvent(int score)
    {
        isGameOver = true;
        GameFlowManager.GoTo(GameFlowName.LevelFailed);

    }

    public void HandleGoalPassedEvent(int score)
    {
        isGameOver = true;
        GameFlowManager.GoTo(GameFlowName.LevelCompleted);
        if (score > previousHighScore) {
          dataManager.ChangeAndSaveHighScore(currentLevel, score); 
          isHighScore = true;
         }
         //GameFlowManager.GoTo(GameFlowName.MainMenu);
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
