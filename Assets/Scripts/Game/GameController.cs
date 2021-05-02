using PocketValues.Types;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : SingletonBehaviour<GameController>, EventListener
{
	//-------------------------------------------------------------------------
	// Types

	public enum CharacterAnimal
	{
		NotSelected,
		Axolotl,
		Cheetah,
		Panda,
		Raccoon,
		Seal
	};

	//-------------------------------------------------------------------------
	// Data

    [SerializeField]
    private StringReference m_GameTitle = new StringReference();

	int m_PlayerScore;
	int m_PlayerTime;

	string m_NextScene;

	CharacterAnimal m_CharacterAnimal;

	//-------------------------------------------------------------------------
	// Interface

	public void ResetGame()
	{
		m_PlayerScore = 0;
		m_PlayerTime = 0;
		m_CharacterAnimal = CharacterAnimal.NotSelected;
	}

	public void SetCharacterAnimal(CharacterAnimal animal)
	{
		m_CharacterAnimal = animal;
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

	//-------------------------------------------------------------------------
	// Events

	void Start()
	{
		if (EventManager.Instance)
		{
			EventManager.Instance.RegisterListener(EventType.e_FadeOutComplete, this);
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
