using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatTracker : MonoBehaviour, EventListener
{
	public GameObject m_PulsePrefab;
	public float m_BeatInterval; // seconds
	public float m_DropHeight = 100;
	public float m_PulseSize = 200;
	public float m_PulseDuration = 1;
	public Color m_BeatColor;
	public Color m_HitColor;
	public Color m_MissColor;

	private float m_StartTime;
	private float m_NextBeat;
	private float m_LastBeat;
	private GameObject m_LastBeatPulse;

	private bool m_BeatActive;

    // Start is called before the first frame update
    void Start()
    {
		m_BeatActive = false; // wait for race to start

		if (EventManager.Instance)
		{
			EventManager.Instance.RegisterListener(EventType.e_RaceStarted, this);
			EventManager.Instance.RegisterListener(EventType.e_RaceFinished, this);
		}
	}

    void OnDestroy()
    {
		if (EventManager.Instance)
		{
			EventManager.Instance.UnregisterListener(EventType.e_RaceStarted, this);
			EventManager.Instance.UnregisterListener(EventType.e_RaceFinished, this);
		}
	}

    public void OnEventReceived(GameEvent gameEvent)
    {
		switch(gameEvent.GetEventType())
		{
			case EventType.e_RaceStarted:
			{
				StartTheBeat();
			}
			break;

			case EventType.e_RaceFinished:
			{
				m_BeatActive = false;
			}
			break;
		}
	}

    // Update is called once per frame
    void Update()
    {
		if (m_BeatActive && Time.time >= m_NextBeat)
		{
			m_LastBeat = m_NextBeat;
			m_NextBeat += m_BeatInterval;
			MakePulse(m_NextBeat, m_BeatColor);
		}
	}

	void StartTheBeat()
	{
		m_BeatActive = true;
        m_StartTime = Time.time;
		m_LastBeat = Time.time;
		m_NextBeat = Time.time;
	}

	void MakePulse(float beatTime, Color pulseColor)
	{
		GameObject newPulse = GameObject.Instantiate(
			m_PulsePrefab, // prefab
			transform.position, // position
			transform.rotation, // rotate
			gameObject.transform // parent
			);

		// Move to front of UI order
		newPulse.transform.SetAsLastSibling();

		// Set up pulse component
		CirclePulse pulseComponent = newPulse.GetComponent<CirclePulse>();
		pulseComponent.m_BaseColor = pulseColor;
		pulseComponent.m_MaxRadius = m_PulseSize;
		pulseComponent.m_Lifetime = m_PulseDuration;
		pulseComponent.m_BeatTime = beatTime;
		pulseComponent.m_DropHeight = m_DropHeight;
	}

	float TimeSinceLastBeat()
	{
		return Mathf.Max(Time.time - m_LastBeat, 0.0f);
	}

	float TimeUntilNextBeat()
	{
		return Mathf.Max(m_NextBeat - Time.time, 0.0f);
	}

	// Gets how far off the current moment is from the nearest beat.
	float CheckError()
	{
		return Mathf.Min(TimeSinceLastBeat(), TimeUntilNextBeat());
	}

	// Returns the amount of error as a percentage of a beat.
	// Result will be in the range from 0 to 0.5.
	// 0 is a perfect hit, 0.5 is exactly halfway between beats.
	float CheckErrorPercent()
	{
		return CheckError()/m_BeatInterval;
	}
}
