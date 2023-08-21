using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void HandleLevelsButtonClicked()
    {
        GameFlowManager.GoTo(GameFlowName.Levels);
        SoundManager.PlayFX(SoundName.MenuSelectSound);

    }

}
