using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventInvoker : MonoBehaviour
{
    protected Dictionary<EventName, UnityEvent<int>> events = new Dictionary<EventName, UnityEvent<int>>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddListener(EventName eventName, UnityAction<int> listener)
    {
        if (events.ContainsKey(eventName))
        {
            events[eventName].AddListener(listener);
        }

    }

}
