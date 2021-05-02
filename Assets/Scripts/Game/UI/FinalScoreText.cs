using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalScoreText : MonoBehaviour
{
	TextMeshProUGUI m_TextComponent;

    // Start is called before the first frame update
    void Start()
    {
		string playerTime = GameController.Instance.GetPlayerTimeString();
        string playerScore = GameController.Instance.GetScore().ToString();

		m_TextComponent = gameObject.GetComponent<TextMeshProUGUI>();
		m_TextComponent.text = "Time: " + playerTime + "\n"
							 + "Score: " + playerScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
