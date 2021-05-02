using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterAnimal
{
	NotSelected = -1,
	Axolotl = 0,
	Cheetah = 1,
	Panda = 2,
	Raccoon = 3,
	Seal = 4
};

public enum SideOfCanoe
{
	Left,
	Right
}

public enum PaddleFrame
{
	Neutral,
	Forward,
	Back
}

public class PaddlingCharacter : MonoBehaviour
{
	public float m_FrameTimeForward = 0.5f;
	public float m_FrameTimeBack = 0.5f;
	public float m_FrameTimeNeutral = 0.5f;

	private SideOfCanoe m_SideOfCanoe = SideOfCanoe.Left;
	private PaddleFrame m_PaddleFrame;
	private CharacterAnimal m_CharacterAnimal = CharacterAnimal.Axolotl;
	private float m_FrameStart;

	private SpriteRenderer m_SpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        m_SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

		if (GameController.Instance)
		{
			// TODO get appropriate animal type
		}
		
		SetPaddleFrame(PaddleFrame.Neutral);
    }

    // Update is called once per frame
    void Update()
    {
		if (m_PaddleFrame == PaddleFrame.Forward
			&& Time.time >= m_FrameStart + m_FrameTimeForward)
		{
			SetPaddleFrame(PaddleFrame.Back);
		}
		if (m_PaddleFrame == PaddleFrame.Back
			&& Time.time >= m_FrameStart + m_FrameTimeBack)
		{
			SetPaddleFrame(PaddleFrame.Neutral);
		}
		
		if(Input.GetKeyDown(KeyCode.Space))
		{
			if (ReadyToPaddle())
			{
				Debug.Log("Ready to paddle");
				Paddle();
			}
			else
			{
				Debug.Log("NOT ready to paddle");
			}
		}
    }

	public bool ReadyToPaddle()
	{
		Debug.Log("m_PaddleFrame = " + m_PaddleFrame.ToString()
			+ "; Time.time = " + Time.time.ToString()
			+ "; m_FrameStart + m_FrameTimeNetural = "
				+ (m_FrameStart + m_FrameTimeNeutral).ToString());

		return m_PaddleFrame == PaddleFrame.Neutral
			&& Time.time >= m_FrameStart + m_FrameTimeNeutral;
	}

	public void Paddle()
	{
		SetPaddleFrame(PaddleFrame.Forward);
	}

	public void SetSideOfCanoe(SideOfCanoe side)
	{
		m_SideOfCanoe = side;
		UpdateSprite();
	}

	void SetPaddleFrame(PaddleFrame paddleFrame)
	{
		m_PaddleFrame = paddleFrame;
		m_FrameStart = Time.time;
		UpdateSprite();
	}

	void SetCharacterAnimal(CharacterAnimal animal)
	{
		m_CharacterAnimal = animal;
		UpdateSprite();
	}

	void UpdateSprite()
	{
		if (GameController.Instance)
		{
			m_SpriteRenderer.sprite = GameController.Instance.GetCharacterSprite(
				m_CharacterAnimal, m_SideOfCanoe, m_PaddleFrame);
		}
	}
}
