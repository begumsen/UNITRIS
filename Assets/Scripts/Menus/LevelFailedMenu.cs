using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelFailedMenu : MonoBehaviour
{
    [SerializeField] GameObject label, mainPanel, score, goal, shadePanel, mainMenuButton, replayButton;

    private void Start()
    {
        AnimateMenu();
    }
    // Start is called before the first frame update
    public void AnimateMenu()
    {
        LeanTween.alpha(shadePanel.GetComponent<RectTransform>(), 0.8f, .5f);
        LeanTween.scale(label, new Vector3(1.2f, 1.2f, 1.2f), 1f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.moveLocal(label, new Vector3(0f, 220f, 2f), 0.3f).setDelay(1f).setEase(LeanTweenType.easeInOutCubic);
        LeanTween.scale(label, new Vector3(1f, 1f, 1f), 1f).setDelay(1f).setEase(LeanTweenType.easeInOutCubic).setOnComplete(LevelFailed);

    }

    void LevelFailed()
    {

        LeanTween.moveLocal(mainPanel, Vector3.zero, 0.8f).setEase(LeanTweenType.easeOutCirc);
        LeanTween.scale(score, new Vector3(1f, 1f, 1f), 0.6f).setDelay(0.8f);
        LeanTween.scale(goal, new Vector3(1f, 1f, 1f), 0.6f).setDelay(1f);
        LeanTween.scale(mainMenuButton, new Vector3(1f, 1f, 1f), 0.6f).setDelay(1.2f);
        LeanTween.scale(replayButton, new Vector3(1f, 1f, 1f), 0.6f).setDelay(1.4f);
        LeanTween.rotateZ(mainMenuButton, 0f, 0.6f).setDelay(1.2f);
        LeanTween.rotateZ(replayButton, 0f, 0.6f).setDelay(1.4f);
        LeanTween.scale(goal, new Vector3(1f, 1f, 1f), 1f).setDelay(3.8f);

    }

    public void Replay()
    {
        GameFlowManager.GoTo(GameFlowName.GamePlay);
    }

    public void MainMenu()
    {
        GameFlowManager.GoTo(GameFlowName.MainMenu);
    }

}
