using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.GetComponent<PlayerController>() != null)
        {
            GameEvent gameEvent = new GameEventObstacleCollided();
            EventManager.Instance.PostEvent(gameEvent);
        }
    }
}
