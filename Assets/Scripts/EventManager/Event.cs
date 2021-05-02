
public class GameEvent
{
    protected EventType m_EventType;
    protected EventData m_EventData;

    public EventType GetEventType() { return m_EventType; }
    public EventData GetEventData() { return m_EventData; }
}
