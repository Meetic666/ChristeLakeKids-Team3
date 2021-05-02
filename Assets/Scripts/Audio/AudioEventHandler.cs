using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEventHandler : MonoBehaviour, EventListener
{
    public AudioInterface audioInterface;

    void Start()
    {
        EventManager.Instance.RegisterListener(EventType.e_RaceFinished, this);
        EventManager.Instance.RegisterListener(EventType.e_GetReady, this);
        EventManager.Instance.RegisterListener(EventType.e_GetSet, this);
        EventManager.Instance.RegisterListener(EventType.e_RaceStarted, this);
        EventManager.Instance.RegisterListener(EventType.e_RaceStarted, this);
        EventManager.Instance.RegisterListener(EventType.e_Paddled, this);
        EventManager.Instance.RegisterListener(EventType.e_PickupCollected, this);
        EventManager.Instance.RegisterListener(EventType.e_ObstacleCollided, this);
    }

    public void OnEventReceived(GameEvent gameEvent)
    {
        if (audioInterface)
        {
            switch (gameEvent.GetEventType())
            {
                case EventType.e_RaceFinished:
                    audioInterface.SetBGMLo();
                    break;
                case EventType.e_GetReady:
                    audioInterface.GetReady();
                    break;
                case EventType.e_GetSet:
                    audioInterface.GetSet();
                    break;
                case EventType.e_RaceStarted:
                    audioInterface.Go();
                    break;
                case EventType.e_Paddled:
                    audioInterface.Splash();
                    break;
                case EventType.e_PickupCollected:
                    audioInterface.Coin();
                    break;
                case EventType.e_ObstacleCollided:
                    audioInterface.Crunch();
                    break;
                default:
                    // DO NOTHING
                    break;
            }
        }
        else
        {
            Debug.LogWarning("AudioEventHandler needs a reference to the AudioInterface");
        }
    }
}
