using UnityEngine;
using System.Collections;

using UnityEngine.Events;

public class FadeAnimation : BaseAnimation
{
    [SerializeField]
    private Canvas fadeCanvas;
    [SerializeField]
    private GameObject fadeImage;
    // Use this for initialization

	public override void StartAnim(int a)
	{
        if (a == 0)
        {
            LeanTween.alpha(fadeImage.GetComponent<RectTransform>(), 0f, 0f);
            LeanTween.alpha(fadeImage.GetComponent<RectTransform>(), 1f, 0.5f).setOnComplete(HandleFadeOut);
        }
        else
        {
            LeanTween.alpha(fadeImage.GetComponent<RectTransform>(), 1f, 0f);
            LeanTween.alpha(fadeImage.GetComponent<RectTransform>(), 0f, 0.5f).setOnComplete(HandleFadeIn);;
        }
    }

    void HandleFadeOut()
    {
        events[EventName.AnimationCompleted].Invoke((int)AnimationName.FadeOut);
    }

    void HandleFadeIn()
    {
        Destroy(transform.gameObject);
    }
}

