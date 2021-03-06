using PocketValues.Types;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : SingletonBehaviour<GameController>, EventListener
{
	//-------------------------------------------------------------------------
	// Types

	[System.Serializable]
	public struct CharacterSpriteSet
	{
		public Sprite m_LeftNeutral;
		public Sprite m_LeftForward;
		public Sprite m_LeftBack;
		public Sprite m_RightNeutral;
		public Sprite m_RightForward;
		public Sprite m_RightBack;
		public Sprite m_Splash;
	}

	//-------------------------------------------------------------------------
	// Data

    [SerializeField]
    private StringReference m_GameTitle = new StringReference();

	int m_PlayerScore;
	int m_PlayerTime;

	string m_NextScene;

	CharacterAnimal m_FrontAnimal;
	CharacterAnimal m_BackAnimal;

	public CharacterSpriteSet[] m_SpriteSet;

	//-------------------------------------------------------------------------
	// Interface

	public void ResetGame()
	{
		m_PlayerScore = 0;
		m_PlayerTime = 0;
		m_FrontAnimal = CharacterAnimal.NotSelected;
		m_BackAnimal = CharacterAnimal.NotSelected;
	}

	public void GoToCharacterSelect()
	{
		FadeToScene("CharacterSelect");
	}

	public bool CanSelectCharacter()
	{
		return m_BackAnimal == CharacterAnimal.NotSelected;
	}

	public void SelectCharacter(CharacterAnimal animal)
	{
		if (m_FrontAnimal == CharacterAnimal.NotSelected)
		{
			m_FrontAnimal = animal;
		}
		else
		{
			m_BackAnimal = animal;
			FadeToScene("MainGame");
		}
	}
	
	public CharacterAnimal GetFrontAnimal()
	{
		return m_FrontAnimal;
	}

	public CharacterAnimal GetBackAnimal()
	{
		return m_BackAnimal;
	}

	public void ScorePoints(int points)
	{
		m_PlayerScore += points;
	}

	public void RecordTime(int time)
	{
		m_PlayerTime = time;
	}

	public int GetPlayerTime()
	{
		return m_PlayerTime;
	}

	public int GetScore()
	{
		return m_PlayerScore;
	}

    public string GetPlayerTimeString()
    {
        int minutes = GetPlayerTime() / 60;
        int seconds = GetPlayerTime() - (60 * minutes);
        return minutes.ToString() + ":" + (seconds < 10 ? "0" : "") + seconds.ToString();
    }

    public string GameTitle
    {
        get { return this.m_GameTitle.Value; }
    }

	public void FadeToScene(string nextScene)
	{
		m_NextScene = nextScene;
		if (EventManager.Instance)
		{
			EventManager.Instance.PostEvent(new GameEventStartFadeOut());
			m_NextScene = nextScene;
		}
	}

	public Sprite GetCharacterSprite(CharacterAnimal animal,
		SideOfCanoe sideOfCanoe, PaddleFrame paddleFrame)
	{
		int i = (int)animal;
		if (i < 0 || i > m_SpriteSet.Length)
		{
			return null;
		}

		switch (sideOfCanoe)
		{
			case SideOfCanoe.Left:
			switch(paddleFrame)
			{
				case PaddleFrame.Neutral:	return m_SpriteSet[i].m_LeftNeutral;
				case PaddleFrame.Forward:	return m_SpriteSet[i].m_LeftForward;
				case PaddleFrame.Back:		return m_SpriteSet[i].m_LeftBack;
			}
			break;

			case SideOfCanoe.Right:
			switch(paddleFrame)
			{
				case PaddleFrame.Neutral:	return m_SpriteSet[i].m_RightNeutral;
				case PaddleFrame.Forward:	return m_SpriteSet[i].m_RightForward;
				case PaddleFrame.Back:		return m_SpriteSet[i].m_RightBack;
			}
			break;
		}
		
		// Not found
		return null;
	}

	public Sprite GetCharacterSplashSprite(CharacterAnimal animal)
	{
		int i = (int)animal;
		if (i < 0 || i > m_SpriteSet.Length)
		{
			return null;
		}
		else
		{
			return m_SpriteSet[i].m_Splash;
		}
	}

	//-------------------------------------------------------------------------
	// Events

	void Start()
	{
		ResetGame();

		if (EventManager.Instance)
		{
			EventManager.Instance.RegisterListener(EventType.e_FadeOutComplete, this);
			EventManager.Instance.RegisterListener(EventType.e_PickupCollected, this);
		}
		else
		{
			Debug.Log("Event registration failed for GameController");
		}
	}
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			if (EventManager.Instance)
			{
				EventManager.Instance.PostEvent(new GameEventRaceStarted());
			}
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			if (EventManager.Instance)
			{
				EventManager.Instance.PostEvent(new GameEventRaceFinished());
			}
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			FadeToScene("EndScene");
		}
	}
	
	void OnDestroy()
	{
		if (EventManager.Instance)
		{
			EventManager.Instance.UnregisterListener(EventType.e_FadeOutComplete, this);
			EventManager.Instance.UnregisterListener(EventType.e_PickupCollected, this);
		}
	}

    public void OnEventReceived(GameEvent gameEvent)
    {
		switch(gameEvent.GetEventType())
		{
			case EventType.e_FadeOutComplete:
			{
				if (!string.IsNullOrEmpty(m_NextScene))
				{
					GoToNextScene();
				}
				else
				{
					Debug.Log("Fade out complete, but next scene null or empty");
				}
			}
			break;

			case EventType.e_PickupCollected:
			{
				PickupCollectedEventData eventData = (PickupCollectedEventData)gameEvent.GetEventData();
				ScorePoints(eventData.m_ScoreBonus);
			}
			break;
		}
    }

	//-------------------------------------------------------------------------
	// Internal helper functions

	private void GoToNextScene()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(m_NextScene);
		m_NextScene = "";
	}
}
