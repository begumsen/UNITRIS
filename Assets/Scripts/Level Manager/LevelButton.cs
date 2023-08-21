using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelButton : EventInvoker
{
    public int levelNo;
    bool isLocked = false; 
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("added level selceted event");
        events.Add(EventName.LevelSelected, new LevelSelectedEvent());
        EventManager.AddInvoker(EventName.LevelSelected, this);
    }

    public void LevelSelectedfromButton()
    {
        if (true) //!isLocked
        {
            Debug.Log(levelNo + " is selected");
            events[EventName.LevelSelected].Invoke(levelNo);
        }
    }
}
