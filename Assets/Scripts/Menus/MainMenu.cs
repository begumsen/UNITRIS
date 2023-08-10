using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void HandleLevelsButtonClicked()
    {
        MenuManager.GoTo(MenuName.Levels);
        SoundManager.PlayFX(SoundName.MenuSelectSound);

    }

    public void HandleHighScoreEvent()
    {
        //MenuManager.GoTo(MenuName.Celebration);
    }


}
