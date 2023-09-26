using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAnimation : EventInvoker,IAnimation
{
    // Start is called before the first frame update
    void Start()
    {
        events.Add(EventName.AnimationCompleted, new AnimationCompleted());
        EventManager.AddInvoker(EventName.AnimationCompleted, this);
    }

    public virtual void StartAnim(int a)
    {

    }

}
