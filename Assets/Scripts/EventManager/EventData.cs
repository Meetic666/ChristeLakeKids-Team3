
public class EventData
{
}

public class PickupCollectedEventData : EventData
{
    public int m_ScoreBonus;
}

public class SpeedChangedEventData : EventData
{
    public float m_CurrentSpeed;
}