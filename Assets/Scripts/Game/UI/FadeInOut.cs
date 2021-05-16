using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour, EventListener
{
	public enum FadeDirection
	{
		None,
		FadeIn,
		FadeOut
	};

	public bool m_FadeInOnStart;
	public float m_FadeTime = 1.0f;

	private FadeDirection m_FadeDirection;
	private float m_Alpha;

	private Image m_Image;
	private Color m_BaseColor;

    // Start is called before the first frame update
    void Start()
    {
		if (EventManager.Instance)
		{
			EventManager.Instance.RegisterListener(EventType.e_StartFadeOut, this);
		}

		RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
		rectTransform.sizeDelta = new Vector2(Screen.width * 2.0f, Screen.height * 2.0f);
		rectTransform.anchoredPosition = new Vector2(0, 0);

        m_Image = gameObject.GetComponent<Image>();
		m_BaseColor = m_Image.color;

		if (m_FadeInOnStart)
		{
			m_Alpha = 1.0f;
			m_FadeDirection = FadeDirection.FadeIn;
		}
		else
		{
			m_Alpha = 0.0f;
		}

		m_Image.color = new Color(m_BaseColor.r, m_BaseColor.g, m_BaseColor.b, m_Alpha);
    }
	
	void OnDestroy()
	{
		// Unsubscribe!
		if (EventManager.Instance)
		{
			EventManager.Instance.UnregisterListener(EventType.e_StartFadeOut, this);
		}
	}

    public void OnEventReceived(GameEvent gameEvent)
    {
		if (gameEvent.GetEventType() == EventType.e_StartFadeOut)
		{
			m_FadeDirection = FadeDirection.FadeOut;
		}
    }

    // Update is called once per frame
    void Update()
    {
		if (m_FadeDirection != FadeDirection.None)
		{
			float fadeDelta = Time.deltaTime * (1.0f / m_FadeTime);
			if (m_FadeDirection == FadeDirection.FadeIn)
			{
				UpdateFadeIn(fadeDelta);
			}
			else
			{
				UpdateFadeOut(fadeDelta);
			}
			m_Image.color = new Color(m_BaseColor.r, m_BaseColor.g, m_BaseColor.b, m_Alpha);
		}
    }
	
	void UpdateFadeIn(float fadeDelta)
	{
		m_Alpha -= fadeDelta;
		if (m_Alpha <= 0.0f)
		{
			m_Alpha = 0.0f;
			m_FadeDirection = FadeDirection.None;

			if (EventManager.Instance)
			{
				EventManager.Instance.PostEvent(new GameEventFadeInComplete());
			}
		}
	}
	
	void UpdateFadeOut(float fadeDelta)
	{
		m_Alpha += fadeDelta;
		if (m_Alpha >= 1.0f)
		{
			m_Alpha = 1.0f;
			m_FadeDirection = FadeDirection.None;

			if (EventManager.Instance)
			{
				EventManager.Instance.PostEvent(new GameEventFadeOutComplete());
			}
		}
	}
}
