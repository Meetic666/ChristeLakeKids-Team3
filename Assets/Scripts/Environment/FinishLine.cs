using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.GetComponent<PlayerController>() != null)
        {
            GameEvent gameEvent = new GameEventRaceFinished();
            EventManager.Instance.PostEvent(gameEvent);
        }
    }
}
