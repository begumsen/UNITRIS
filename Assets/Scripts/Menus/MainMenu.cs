using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void HandleLevelsButtonClicked()
    {
        MenuManager.GoTo(MenuName.Levels);
        SoundManager.Play(SoundName.MenuSelectSound);

    }

}
