using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{

    #region Fields

    static Dictionary<EventName, List<FloatEventInvoker>> invokers = new Dictionary<EventName, List<FloatEventInvoker>>();
    static Dictionary<EventName, List<UnityAction<float>>> listeners = new Dictionary<EventName, List<UnityAction<float>>>();

    #endregion

    #region Methods

    static public void Initialize()
    {

        foreach (EventName eventName in (EventName[])Enum.GetValues(typeof(EventName)))
        {
            if (!invokers.ContainsKey(eventName))
            {
                invokers.Add(eventName, new List<FloatEventInvoker>());
                listeners.Add(eventName, new List<UnityAction<float>>());
            }
            else
            {
                invokers[eventName].Clear();
                listeners[eventName].Clear();
            }
        }
    }

    static public void AddInvoker(EventName eventName, FloatEventInvoker invoker)
    {
        invokers[eventName].Add(invoker);
        foreach (UnityAction<float> listener in listeners[eventName])
        {
            invoker.AddListener(eventName, listener);
        }
    }

    static public void AddListener(EventName eventName, UnityAction<float> listener)
    {
        listeners[eventName].Add(listener);
        foreach (FloatEventInvoker invoker in invokers[eventName])
        {
            invoker.AddListener(eventName, listener);
        }
    }

    static public void RemoveInvoker(EventName eventName, FloatEventInvoker invoker)
    {
        invokers[eventName].Remove(invoker);
    }

    #endregion
}
