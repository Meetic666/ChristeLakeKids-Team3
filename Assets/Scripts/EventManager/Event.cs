
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
