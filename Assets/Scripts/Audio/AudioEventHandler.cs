using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEventHandler : MonoBehaviour, EventListener
{
    public AudioInterface audioInterface;

    void Start()
    {
        EventManager.Instance.RegisterListener(EventType.e_RaceFinished, this);
        EventManager.Instance.RegisterListener(EventType.e_RaceStarted, this);
        EventManager.Instance.RegisterListener(EventType.e_Paddled, this);
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
                case EventType.e_RaceStarted:
                    audioInterface.SetBGMHi();
                    break;
                case EventType.e_Paddled:
                    audioInterface.Splash();
                    break;
                case EventType.e_PickupCollected:
                    audioInterface.Coin();
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
