using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationManager: MonoBehaviour
{
    GameObject anim;
    static bool initialized = false;
    
    // Start is called before the first frame update
    void Start()
    {
        if (initialized == false)
        {
            initialized = true;
            DontDestroyOnLoad(gameObject);
            EventManager.AddListener(EventName.GoalPassed, HandleGoalPassed);
            EventManager.AddListener(EventName.GoalNotPassed, HandleGoalNotPassed);
            EventManager.AddListener(EventName.AnimationCompleted, HandleAnimationCompleted);
            Debug.Log("added listener");
            EventManager.AddListener(EventName.Fade, HandleFade);
        } else
        {
            Destroy(gameObject);
        }
        
    }

    void HandleGoalPassed(int a)
    {
        InitializeAndStartAnim("Prefabs/LevelCompleted", a);
    }

    void HandleGoalNotPassed(int a)
    {
        InitializeAndStartAnim("Prefabs/LevelFailed", a);
    }

    void HandleFade(int a)
    {
        InitializeAndStartAnim("Prefabs/Fade", a);
    }

    void HandleAnimationCompleted(int animName)
    {
        if (animName == (int)AnimationName.FadeOut)
        {
            GameFlowManager.HandleSceneChange();
        }
        else if (animName == (int)AnimationName.LevelCompleted)
        {
            GameFlowManager.GoTo(GameFlowName.MainMenu);
        }
    }

    void InitializeAndStartAnim(string path, int a)
    {
        anim = (GameObject)Instantiate(Resources.Load(path));
        BaseAnimation script = anim.GetComponent<BaseAnimation>();
        script.StartAnim(a);
    }
}
