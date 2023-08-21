using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameFlowManager
{

    public static void GoTo(GameFlowName gameFlowName)
    {
        switch (gameFlowName)
        {
            case GameFlowName.MainMenu:
                SoundManager.StopBackgroundMusic();
                SceneManager.LoadScene("MainMenu");
                break;
            case GameFlowName.Levels:
                GameObject mainMenu = GameObject.Find("MenuPanel");
                if (mainMenu != null)
                {
                    mainMenu.SetActive(false);
                }
                Object.Instantiate(Resources.Load("LevelsPopup"));
                break;
            case GameFlowName.GamePlay:
                SoundManager.InitialState();
                SceneManager.LoadScene("GameScene");
                break;
            
        }
    }
}
