using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Threading.Tasks;


public static class GameFlowManager
{

    static GameObject mainMenu;
    static string levelToLoad;

    public static void Initialize()
    {
        
    }

    public static void GoTo(GameFlowName gameFlowName)
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        switch (gameFlowName)
        {

            case GameFlowName.MainMenu:
                EventManager.TriggerEvent(EventName.Fade, 0);
                SoundManager.StopBackgroundMusic();
                levelToLoad = "MainMenu";
                break;
            case GameFlowName.Levels:
                if (mainMenu == null)
                {
                    mainMenu = GameObject.Find("MenuPanel");
                }
                Object.Instantiate(Resources.Load("Prefabs/LevelsPopup"));
                mainMenu.SetActive(false);
                break;
            case GameFlowName.GamePlay:
                EventManager.TriggerEvent(EventName.Fade, 0);
                SoundManager.InitialState();
                levelToLoad = "GameScene";
                break;
        }
    }

    public static void HandleSceneChange()
    {
        Debug.Log("HandleSceneChange");
        SceneManager.LoadScene(levelToLoad);
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        EventManager.TriggerEvent(EventName.Fade, 1);
    }


}
