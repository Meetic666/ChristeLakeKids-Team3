using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, EventListener
{
    [SerializeField]
    GameObject[] paddles;

	[SerializeField]
	PaddlingCharacter frontPaddler;

	[SerializeField]
	PaddlingCharacter backPaddler;

    [SerializeField]
    float frontPower = 2;
    [SerializeField]
    float backPower = 1.5f;

    Rigidbody2D rb;

    bool m_RaceStarted = false;

    GameEventSpeedChanged m_SpeedChangedEvent;
    float m_PreviousSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if(EventManager.Instance != null)
        {
            EventManager.Instance.RegisterListener(EventType.e_RaceStarted, this);
        }

		frontPaddler.m_OnPaddlePushBack = OnFrontPaddlePushBack;
		backPaddler.m_OnPaddlePushBack = OnBackPaddlePushBack;

        m_SpeedChangedEvent = new GameEventSpeedChanged();
    }

    // Update is called once per frame
    void Update() 
    {
        if(m_RaceStarted)
        {
			if (frontPaddler.ReadyToPaddle())
			{
				ProcessFrontPaddlerInput();
			}
			if (backPaddler.ReadyToPaddle())
			{
				ProcessBackPaddlerInput();
			}

            float currentSpeed = rb.velocity.magnitude;
            if (currentSpeed != m_PreviousSpeed)
            {
                m_SpeedChangedEvent.SetSpeed(currentSpeed / 10.0f);
                EventManager.Instance.PostEvent(m_SpeedChangedEvent);

                m_PreviousSpeed = currentSpeed;
            }
        }
    }

	void ProcessFrontPaddlerInput()
	{
		// Front Left
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (frontPaddler.IsOnRight())
            {
				frontPaddler.SetSideOfCanoe(SideOfCanoe.Left);
            }
            else
            {
				frontPaddler.Paddle();
            }
        }

		// Front Right
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (!frontPaddler.IsOnRight())
            {
                frontPaddler.SetSideOfCanoe(SideOfCanoe.Right);
            }
            else
            {
				frontPaddler.Paddle();
            }
        }
	}

	void ProcessBackPaddlerInput()
	{
		//Back Left
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (backPaddler.IsOnRight())
            {
                backPaddler.SetSideOfCanoe(SideOfCanoe.Left);
            }
            else
            {
				backPaddler.Paddle();
            }
        }
		// Back Right
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!backPaddler.IsOnRight())
            {
				backPaddler.SetSideOfCanoe(SideOfCanoe.Right);
                //paddles[2].SetActive(true);
                //paddles[3].SetActive(false);
            }
            else
            {
				backPaddler.Paddle();
			}
		}
	}

	// Apply the impulse when the paddle goes back in the animation
	void OnFrontPaddlePushBack()
	{
		if (EventManager.Instance)
		{
			EventManager.Instance.PostEvent(new GameEventPaddled());
		}
		
		if (frontPaddler.IsOnRight())
		{
            rb.AddForceAtPosition(transform.up * frontPower,
				paddles[0].transform.position, ForceMode2D.Impulse);
		}
		else
		{
			rb.AddForceAtPosition(transform.up * frontPower,
				paddles[1].transform.position, ForceMode2D.Impulse);
		}
	}

	void OnBackPaddlePushBack()
	{
		if (EventManager.Instance)
		{
			EventManager.Instance.PostEvent(new GameEventPaddled());
		}

		if (backPaddler.IsOnRight())
		{
			rb.AddForceAtPosition(transform.up * backPower,
				paddles[2].transform.position, ForceMode2D.Impulse);
		}
		else
		{
			rb.AddForceAtPosition(transform.up * backPower,
				paddles[3].transform.position, ForceMode2D.Impulse);
		}
	}

    public void OnEventReceived(GameEvent gameEvent)
    {
        m_RaceStarted = true;
    }
}
