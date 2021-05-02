using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinishMessage : MonoBehaviour, EventListener
{
	public float m_MessageDuration;
	public float m_ScaleTarget = 2.0f;

	private Color m_BaseColor;
	private float m_StartTime;
	private TextMeshProUGUI m_TextComponent;
	private RectTransform m_RectTransform;

    // Start is called before the first frame update
    void Start()
    {
		m_TextComponent = gameObject.GetComponent<TextMeshProUGUI>();
		m_RectTransform = gameObject.GetComponent<RectTransform>();

		m_BaseColor = m_TextComponent.color;

		m_TextComponent.enabled = false;

		if (EventManager.Instance)
		{
			EventManager.Instance.RegisterListener(EventType.e_RaceFinished, this);
		}
    }

	void OnDestroy()
	{
		if (EventManager.Instance)
		{
			EventManager.Instance.UnregisterListener(EventType.e_RaceFinished, this);
		}
	}
	

    public void OnEventReceived(GameEvent gameEvent)
    {
		if (gameEvent.GetEventType() == EventType.e_RaceFinished)
		{
			if (m_TextComponent.enabled == false)
			{
				m_TextComponent.enabled = true;
				m_StartTime = Time.time;
			}
		}
    }

    // Update is called once per frame
    void Update()
    {
        if (m_TextComponent.enabled)
		{
			float elapsedTime = Time.time - m_StartTime;
			if (elapsedTime > m_MessageDuration)
			{
				m_TextComponent.enabled = false;
				if (GameController.Instance)
				{
					GameController.Instance.FadeToScene("EndScene");
				}
			}
			else
			{
				float newScale = 1.0f + (m_ScaleTarget - 1.0f)*(elapsedTime/m_MessageDuration);
				m_RectTransform.localScale = new Vector3(newScale, newScale, newScale);

				float alpha = 1.0f - (elapsedTime/m_MessageDuration);
				Color c = m_BaseColor;
				m_TextComponent.color = new Color(c.r, c.g, c.b, c.a*alpha);
			}
		}
	}
}
