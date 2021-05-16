using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour, EventListener
{
	public TextMeshProUGUI m_TextComponent;

	// Start is called before the first frame update
	void Start()
	{
		m_TextComponent = gameObject.GetComponent<TextMeshProUGUI>();

		m_TextComponent.enabled = false; // wait for race start

		if (EventManager.Instance)
		{
			EventManager.Instance.RegisterListener(EventType.e_PickupCollected, this);
		}
	}

	public void OnEventReceived(GameEvent gameEvent)
	{
		m_TextComponent.enabled = true;
		m_TextComponent.text = GetScoreString();
	}

	public void OnDestroy()
	{
		if (EventManager.Instance)
		{
			EventManager.Instance.UnregisterListener(EventType.e_PickupCollected, this);
		}
	}

	public string GetScoreString()
	{
		int score = GameController.Instance.GetScore();
		return score.ToString() + " pts";
	}
}
