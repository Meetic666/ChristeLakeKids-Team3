using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    Dictionary<EventType, List<EventListener>> m_EventListeners = new Dictionary<EventType, List<EventListener>>();

    public void RegisterListener(EventType eventType, EventListener eventListener)
    {
        if(!m_EventListeners.ContainsKey(eventType))
        {
            m_EventListeners.Add(eventType, new List<EventListener>());
        }

        m_EventListeners[eventType].Add(eventListener);
    }

    public void UnregisterListener(EventType eventType, EventListener eventListener)
    {
        if (m_EventListeners.ContainsKey(eventType))
        {
            m_EventListeners[eventType].Remove(eventListener);

            if(m_EventListeners[eventType].Count == 0)
            {
                m_EventListeners.Remove(eventType);
            }
        }
    }

    public void PostEvent(GameEvent gameEvent)
    {
        EventType eventType = gameEvent.GetEventType();
        if(m_EventListeners.ContainsKey(eventType))
        {
            foreach(EventListener eventListener in m_EventListeners[eventType])
            {
                eventListener.OnEventReceived(gameEvent);
            }
        }
    }
}
