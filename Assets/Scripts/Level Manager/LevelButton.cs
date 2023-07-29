using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelButton : EventInvoker
{
    public int levelNo;
    // Start is called before the first frame update
    void Start()
    {
        events.Add(EventName.LevelSelected, new LevelSelectedEvent());
        EventManager.AddInvoker(EventName.LevelSelected, this);
    }

    public void LevelSelectedfromButton()
    {
        events[EventName.LevelSelected].Invoke(levelNo);
    }
}
