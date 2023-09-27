using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class LevelCompletedAnimation : BaseAnimation
{
    [SerializeField] GameObject label, mainPanel, scoreLabel, goalLabel, shadePanel;

    public override void StartAnim(int a)
    {
        TMP_Text scoreLabelText = scoreLabel.GetComponent<TMP_Text>();
        scoreLabelText.text = "Score: " + LevelManager.Instance.FinalScore.ToString();
        TMP_Text goalLabelText = goalLabel.GetComponent<TMP_Text>();
        goalLabelText.text = "Goal: " + LevelManager.Instance.Goal.ToString();

        AnimateMenu();
    }
    // Start is called before the first frame update
    public void AnimateMenu()
    {
        LeanTween.alpha(shadePanel.GetComponent<RectTransform>(), 0.8f, .5f);
        LeanTween.scale(label, new Vector3(1.2f, 1.2f, 1.2f), 1f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.moveLocal(label, new Vector3(0f, 250f, 2f), 0.3f).setDelay(1f).setEase(LeanTweenType.easeInOutCubic);
        LeanTween.scale(label, new Vector3(1f, 1f, 1f), 1f).setDelay(1f).setEase(LeanTweenType.easeInOutCubic).setOnComplete(LevelComplete);
        
    }

    void LevelComplete()
    {

        LeanTween.moveLocal(mainPanel, Vector3.zero, 0.8f).setEase(LeanTweenType.easeOutCirc);
        LeanTween.scale(scoreLabel, new Vector3(1f, 1f, 1f), 0.6f).setDelay(0.8f);
        LeanTween.scale(goalLabel, new Vector3(1f, 1f, 1f), 0.6f).setDelay(1f);
        LeanTween.scale(goalLabel, new Vector3(1f, 1f, 1f), 1f).setDelay(3.2f).setOnComplete(HidePanel);
    }

    void HidePanel()
    {
        events[EventName.AnimationCompleted].Invoke((int)AnimationName.LevelCompleted);
    }
}
