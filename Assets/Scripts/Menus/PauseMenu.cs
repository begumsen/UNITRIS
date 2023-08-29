using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
   public void PauseTheGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        Resume();
        GameFlowManager.GoTo(GameFlowName.GamePlay);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        GameFlowManager.GoTo(GameFlowName.MainMenu);
    }
}
