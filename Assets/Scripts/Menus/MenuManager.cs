using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class MenuManager
{

    public static void GoTo(MenuName menuName)
    {
        switch (menuName)
        {
            case MenuName.MainMenu:
                SceneManager.LoadScene("MainMenu");
                break;
            case MenuName.Levels:
                GameObject mainMenu = GameObject.Find("MenuPanel");
                if (mainMenu != null)
                {
                    mainMenu.SetActive(false);
                }
                Object.Instantiate(Resources.Load("LevelsPopup"));
                break;
            case MenuName.PauseMenu:
                Object.Instantiate(Resources.Load("PauseMenu"));
                break;
        }
    }
}
