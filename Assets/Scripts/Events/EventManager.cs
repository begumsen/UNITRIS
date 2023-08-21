using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    static bool initialized = false;
    public static Dictionary<EventName, List<EventInvoker>> invokers = new Dictionary<EventName, List<EventInvoker>> ();
    public static Dictionary<EventName, List<UnityAction<int>>> listeners = new Dictionary<EventName, List<UnityAction<int>>>();

    public static void Initialize()
    {
        if (!initialized)
        {
            initialized = true;
            foreach (EventName name in Enum.GetValues(typeof(EventName)))
            {
                if (!invokers.ContainsKey(name))
                {
                    invokers.Add(name, new List<EventInvoker>());
                    listeners.Add(name, new List<UnityAction<int>>());
                }
                else
                {
                    invokers[name].Clear();
                    listeners[name].Clear();
                }
            }
        }

    }

    public static void AddInvoker (EventName name, EventInvoker invoker)
    {
        invokers[name].Add(invoker);
        foreach(UnityAction<int> listener in listeners[name])
        {
            if(name == EventName.LevelSelected)
            {
                Debug.Log(listener);
            }

            invoker.AddListener(name, listener);
        }
    }

    public static void AddListener (EventName name, UnityAction<int> listener)
    {
        listeners[name].Add(listener);
        foreach (EventInvoker invoker in invokers[name])
        {
            invoker.AddListener(name, listener);
        }
    }
}
