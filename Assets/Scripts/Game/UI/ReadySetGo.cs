using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReadySetGo : MonoBehaviour, EventListener
{
	[System.Serializable]
	public struct MessageData
	{
		public string m_Message;
		public float m_Duration;
		public Color m_Color;
	}

	TextMeshProUGUI m_TextComponent;
	RectTransform m_RectTransform;

    public MessageData[] m_MessageData;
	public float m_ScaleTarget = 2.0f;

	private float m_StartTime;
	private int m_MessageIndex;

    // Start is called before the first frame update
    void Start()
    {
		m_TextComponent = gameObject.GetComponent<TextMeshProUGUI>();
		m_RectTransform = gameObject.GetComponent<RectTransform>();
		m_MessageIndex = -1;
		m_TextComponent.enabled = false;

		if (EventManager.Instance)
		{
			EventManager.Instance.RegisterListener(EventType.e_FadeInComplete, this);
		}
    }
	
	void OnDestroy()
	{
		if (EventManager.Instance)
		{
			EventManager.Instance.UnregisterListener(EventType.e_FadeInComplete, this);
		}
	}

    public void OnEventReceived(GameEvent gameEvent)
    {
		if (gameEvent.GetEventType() == EventType.e_FadeInComplete)
		{
			StartMessage(0);
		}
    }

	void StartMessage(int index)
	{
		if (index >= 0 && index < m_MessageData.Length)
		{
			m_TextComponent.enabled = true;
			m_MessageIndex = index;
			m_StartTime = Time.time;
			m_TextComponent.text = m_MessageData[index].m_Message;
			m_TextComponent.color = m_MessageData[index].m_Color;
			m_RectTransform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

			if (index == 0 && EventManager.Instance)
            {
				EventManager.Instance.PostEvent(new GameEventGetReady());
			}

			if (index == 1 && EventManager.Instance)
			{
				EventManager.Instance.PostEvent(new GameEventGetSet());
			}

			if (index == m_MessageData.Length - 1
				&& EventManager.Instance)
			{
				// special case: start the race
				EventManager.Instance.PostEvent(new GameEventRaceStarted());
			}
		}
		else
		{
			m_TextComponent.enabled = false;
		}
	}

    // Update is called once per frame
    void Update()
    {
        if (m_MessageIndex >= 0 && m_MessageIndex < m_MessageData.Length)
		{
			float elapsedTime = Time.time - m_StartTime;
			float duration = m_MessageData[m_MessageIndex].m_Duration;
			if (elapsedTime > duration)
			{
				StartMessage(m_MessageIndex + 1);
			}
			else
			{
				float newScale = 1.0f + (m_ScaleTarget - 1.0f)*(elapsedTime/duration);
				m_RectTransform.localScale = new Vector3(newScale, newScale, newScale);

				float alpha = 1.0f - (elapsedTime/duration);
				Color c = m_MessageData[m_MessageIndex].m_Color;
				m_TextComponent.color = new Color(c.r, c.g, c.b, c.a*alpha);
			}
		}
	}
}
