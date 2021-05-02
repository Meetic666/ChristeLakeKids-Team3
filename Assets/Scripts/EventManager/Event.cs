
public class GameEvent
{
    protected EventType m_EventType;
    protected EventData m_EventData;

    public EventType GetEventType() { return m_EventType; }
    public EventData GetEventData() { return m_EventData; }
}

public class GameEventStartFadeOut : GameEvent
{
	public GameEventStartFadeOut()
	{
		m_EventType = EventType.e_StartFadeOut;
	}
}

public class GameEventFadeOutComplete : GameEvent
{
	public GameEventFadeOutComplete()
	{
		m_EventType = EventType.e_FadeOutComplete;
	}
}

public class GameEventFadeInComplete : GameEvent
{
	public GameEventFadeInComplete()
	{
		m_EventType = EventType.e_FadeInComplete;
	}
}

public class GameEventGetReady : GameEvent
{
	public GameEventGetReady()
	{
		m_EventType = EventType.e_GetReady;
	}
}

public class GameEventGetSet : GameEvent
{
	public GameEventGetSet()
	{
		m_EventType = EventType.e_GetSet;
	}
}

public class GameEventRaceStarted : GameEvent
{
	public GameEventRaceStarted()
	{
		m_EventType = EventType.e_RaceStarted;
	}
}

public class GameEventRaceFinished : GameEvent
{
	public GameEventRaceFinished()
	{
		m_EventType = EventType.e_RaceFinished;
	}
}

public class GameEventPickupCollected : GameEvent
{
	public GameEventPickupCollected(int timeBonus)
	{
		m_EventType = EventType.e_PickupCollected;
		PickupCollectedEventData eventData = new PickupCollectedEventData();
		eventData.m_TimeBonus = timeBonus;
		m_EventData = eventData;
	}

}

public class GameEventPaddled : GameEvent
{
	public GameEventPaddled()
	{
		m_EventType = EventType.e_Paddled;
	}
}

public class GameEventObstacleCollided : GameEvent
{
	public GameEventObstacleCollided()
	{
		m_EventType = EventType.e_ObstacleCollided;
	}
}

public class GameEventSpeedChanged : GameEvent
{
	public GameEventSpeedChanged()
	{
		m_EventType = EventType.e_SpeedChanged;
		m_EventData = new SpeedChangedEventData();
	}

	public void SetSpeed(float speed)
    {
		((SpeedChangedEventData)m_EventData).m_CurrentSpeed = speed;
    }
}
