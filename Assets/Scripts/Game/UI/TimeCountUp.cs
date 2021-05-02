using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Attach this script to an entity with a TextMeshPro - Text (UI) component.
// Use that component to configure where the text will appear.

public class TimeCountUp : MonoBehaviour, EventListener
{
    public TextMeshProUGUI m_TextComponent;
    public float m_StartTime;

	private bool m_RaceStarted;
	private bool m_RaceFinished;

	private int m_TimeBonus;

	// Start is called before the first frame update
	void Start()
    {
        m_StartTime = Time.time;
        m_TextComponent = gameObject.GetComponent<TextMeshProUGUI>();

		m_TextComponent.enabled = false; // wait for race start

		if (EventManager.Instance)
		{
			EventManager.Instance.RegisterListener(EventType.e_RaceStarted, this);
			EventManager.Instance.RegisterListener(EventType.e_RaceFinished, this);

			EventManager.Instance.RegisterListener(EventType.e_PickupCollected, this);
		}
    }

    // Update is called once per frame
    void Update()
    {
		if (m_RaceStarted && !m_RaceFinished)
		{
			m_TextComponent.text = GetElapsedTimeString();
		}
    }

    public void OnEventReceived(GameEvent gameEvent)
    {
		switch(gameEvent.GetEventType())
		{
			case EventType.e_RaceStarted:
			{
				m_RaceStarted = true;
				m_RaceFinished = false;
				m_StartTime = Time.time;
				m_TextComponent.enabled = true;
			}
			break;

			case EventType.e_RaceFinished:
			{
				m_RaceFinished = true;
				if (GameController.Instance)
				{
					GameController.Instance.RecordTime(GetSecondsElapsed());
				}
			}
			break;

			case EventType.e_PickupCollected:
            {
				PickupCollectedEventData eventData = (PickupCollectedEventData)gameEvent.GetEventData();
				m_TimeBonus += eventData.m_TimeBonus;
            }
			break;
		}
	}

	public void OnDestroy()
	{
		if (EventManager.Instance)
		{
			EventManager.Instance.UnregisterListener(EventType.e_RaceStarted, this);
			EventManager.Instance.UnregisterListener(EventType.e_RaceFinished, this);
		}
	}

	public int GetSecondsElapsed()
    {
        return (int)(Time.time - m_StartTime) - m_TimeBonus;
    }

    public string GetElapsedTimeString()
    {
        int elapsedTime = GetSecondsElapsed();
        int minutes = elapsedTime / 60;
        int seconds = elapsedTime - (60 * minutes);
        return minutes.ToString() + ":" + (seconds < 10 ? "0" : "") + seconds.ToString();
    }
}
