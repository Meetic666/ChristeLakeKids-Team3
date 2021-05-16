using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAgainButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void OnClicked()
	{
		if (GameController.Instance)
		{
			GameController.Instance.ResetGame();
			GameController.Instance.FadeToScene("Splash");
		}
	}
}
