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
	private bool m_BeatSynced;

    // Start is called before the first frame update
    void Start()
    {
		m_BeatActive = false; // wait for race to start
		m_BeatSynced = false; // need to capture a beat from the song

		if (EventManager.Instance)
		{
			EventManager.Instance.RegisterListener(EventType.e_RaceStarted, this);
			EventManager.Instance.RegisterListener(EventType.e_RaceFinished, this);
		}

		AudioInterface audioInterface = GameObject.FindObjectOfType<AudioInterface>();
		if (audioInterface)
		{
			audioInterface.OnBeat.AddListener(SynchBeat);
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

	public void SynchBeat()
	{
		if (!m_BeatSynced)
		{
			Debug.Log("Synced to beat");
			m_BeatSynced = true;
			m_StartTime = Time.time;
			m_LastBeat = Time.time;
			m_NextBeat = Time.time;
		}
	}

    // Update is called once per frame
    void Update()
    {
		Debug.Log("m_BeatSynced = " + m_BeatSynced.ToString());
		
		Debug.Log("Time.time = " + Time.time.ToString() + " / NextBeat: " + m_NextBeat.ToString());
		if (m_BeatSynced && Time.time >= m_NextBeat)
		{
			Debug.Log("Beat arrived");
			m_LastBeat = m_NextBeat;
			m_NextBeat += m_BeatInterval;
			if (m_BeatActive)
			{
				MakePulse(m_NextBeat, m_BeatColor);
			}
		}
	}

	void StartTheBeat()
	{
		m_BeatActive = true;
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

	CirclePulse FindClosestPulse()
	{
		// Find all existing pulse objects
		CirclePulse[] pulses = GameObject.FindObjectsOfType<CirclePulse>();

		// Search for the closest one to the current moment
		float bestDifference = 100000.0f; // sure
		CirclePulse closestPulse = null;
		foreach (CirclePulse pulse in pulses)
		{
			float difference = Mathf.Abs(pulse.m_BeatTime - Time.time);
			if (difference < bestDifference)
			{
				bestDifference = difference;
				closestPulse = pulse;
			}
		}
		
		return closestPulse;
	}
	
	// Gets how far off the current moment is from the nearest beat.
	float CheckError()
	{
		CirclePulse pulse = FindClosestPulse();
		if (pulse)
		{
			return Mathf.Abs(Time.time - pulse.m_BeatTime);
		}
		else
		{
			return m_BeatInterval * 0.5f;
		}
	}

	// Returns the amount of error as a percentage of a beat.
	// Result will be in the range from 0 to 0.5.
	// 0 is a perfect hit, 0.5 is exactly halfway between beats.
	public float CheckErrorPercent()
	{
		return CheckError()/m_BeatInterval;
	}
	
	public void OnHit()
	{
		CirclePulse pulse = FindClosestPulse();
//		if (pulse.m_Color == m_BeatColor)
//		{
		pulse.SetColor(m_HitColor);
//		}
	}

	public void OnMiss()
	{
		CirclePulse pulse = FindClosestPulse();
	//	if (pulse.m_Color == m_BeatColor)
	//	{
		pulse.SetColor(m_MissColor);
	//	}
	}
}
