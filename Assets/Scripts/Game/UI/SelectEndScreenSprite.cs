using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectEndScreenSprite : MonoBehaviour
{
	public bool m_IsFrontPaddler;

    // Start is called before the first frame update
    void Start()
    {
		if (GameController.Instance)
		{
			Image image = gameObject.GetComponent<Image>();
			CharacterAnimal animal = CharacterAnimal.NotSelected;
			if (m_IsFrontPaddler)
			{
				animal = GameController.Instance.GetFrontAnimal();
			}
			else
			{
				animal = GameController.Instance.GetBackAnimal();
			}

			if (animal != CharacterAnimal.NotSelected)
			{
				image.sprite = GameController.Instance.GetCharacterSplashSprite(animal);
			}
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
