using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public int m_ScoreBonus = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.GetComponent<PlayerController>() != null)
        {
            GameEvent gameEvent = new GameEventPickupCollected(m_ScoreBonus);
            EventManager.Instance.PostEvent(gameEvent);

            gameObject.SetActive(false);
        }
    }
}
