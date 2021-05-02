
public interface EventListener
{
    public abstract void OnEventReceived(GameEvent gameEvent);
}

public class SpecificEventListener : EventListener
{
    public void OnEventReceived(GameEvent gameEvent)
    {

    }
}
