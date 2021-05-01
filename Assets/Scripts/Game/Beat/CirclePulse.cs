using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CirclePulse : MonoBehaviour
{
	public float m_Lifetime = 1.0f;
	public float m_StartRadius = 0.0f;
	public float m_MaxRadius = 10.0f;
	public Color m_BaseColor;

	public float m_BeatTime;
	public float m_DropHeight;

	private float m_DropTime;
	private Image m_Image;
	private RectTransform m_RectTransform;
	private float m_RadiusGrowth;
	private bool m_Falling = true;

    // Start is called before the first frame update
    void Start()
    {
		m_Image = gameObject.GetComponent<Image>();
		m_RectTransform = gameObject.GetComponent<RectTransform>();

		// How long in the future?
		m_DropTime = m_BeatTime - Time.time;

		m_RadiusGrowth = m_MaxRadius - m_StartRadius;
		m_RectTransform.sizeDelta = new Vector2(m_StartRadius,m_StartRadius);
		m_Image.color = new Color(m_BaseColor.r, m_BaseColor.g, m_BaseColor.b, m_BaseColor.a);
    }
	
	void Update()
	{
		if (m_Falling)
		{
			if (Time.time >= m_BeatTime)
			{
				m_Falling = false;
				m_RectTransform.anchoredPosition = new Vector2(0.0f, 0.0f);
			}
			else
			{
				float dropLeftPercent = (m_BeatTime - Time.time) / m_DropTime;
				m_RectTransform.anchoredPosition = new Vector2(0.0f, m_DropHeight * dropLeftPercent);
				m_Image.color = new Color(m_BaseColor.r, m_BaseColor.g, m_BaseColor.b,
					m_BaseColor.a*(1.0f - dropLeftPercent));
			}
		}
		else
		{
			float pulseDonePercent = (Time.time - m_BeatTime) / m_Lifetime;
			float pulseLeftPercent = 1.0f - pulseDonePercent;
			if (pulseLeftPercent <= 0.0f)
			{
				Destroy(gameObject);
			}
			else
			{
				m_Image.color = new Color(m_BaseColor.r, m_BaseColor.g, m_BaseColor.b,
					m_BaseColor.a*pulseLeftPercent);
				
				float radius = m_StartRadius + m_RadiusGrowth*(pulseDonePercent);
				m_RectTransform.sizeDelta = new Vector2(radius,radius);
			}
		}
	}

	public void SetColor(Color newColor)
	{
		m_BaseColor = newColor;
	}
}
