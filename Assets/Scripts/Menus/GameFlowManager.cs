using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameFlowManager
{
    private static bool isMainMenuActive = true;
    static GameObject mainMenu;

    static GameFlowManager()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the event
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if (scene.name == "MainMenu")
        {
            if (!isMainMenuActive)
            {
               // mainMenu.SetActive(true);
                isMainMenuActive = true;
            }
        }

    }

    public static void GoTo(GameFlowName gameFlowName)
    {
        switch (gameFlowName)
        {
            case GameFlowName.MainMenu:
                SoundManager.StopBackgroundMusic();
                SceneManager.LoadScene("MainMenu");
                break;
            case GameFlowName.Levels:
                if (mainMenu == null)
                {
                    mainMenu = GameObject.Find("MenuPanel");
                }
                Object.Instantiate(Resources.Load("LevelsPopup"));
                mainMenu.SetActive(false);
                isMainMenuActive = false;
                break;
            case GameFlowName.GamePlay:
                SoundManager.InitialState();
                SceneManager.LoadScene("GameScene");
                break;
            case GameFlowName.LevelCompleted:
                Object.Instantiate(Resources.Load("LevelCompleted"));
                break;
            case GameFlowName.LevelFailed:
                Object.Instantiate(Resources.Load("LevelFailed"));
                break;
        }
    }

}
