using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelect : MonoBehaviour
{
	public TextMeshProUGUI m_TextMesh;
	public bool m_Clicked;
	public Color m_SelectColor;
	public CharacterAnimal m_Animal;

	private Image m_Image;

    // Start is called before the first frame update
    void Start()
    {
		m_Image = gameObject.GetComponent<Image>();
		m_Clicked = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void OnClicked()
	{
		Debug.Log("mClicked = " + m_Clicked.ToString());
		Debug.Log("CanSelect = " + GameController.Instance.CanSelectCharacter().ToString());

		if (!m_Clicked && GameController.Instance
			&& GameController.Instance.CanSelectCharacter())
		{
			Debug.Log("Firing select character");

			m_Clicked = true;
			GameController.Instance.SelectCharacter(m_Animal);

			if (AudioInterface.Instance)
			{
				AudioInterface.Instance.Click();
			}

			m_TextMesh.color = m_SelectColor;
			m_Image.color = m_SelectColor;
		}
	}
}
