using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAnyKey : MonoBehaviour
{
    bool m_Transitioning = false;

    // Update is called once per frame
    void Update()
    {
        if(!m_Transitioning && Input.anyKeyDown)
        {
            GameController.Instance.GoToCharacterSelect();
            m_Transitioning = true;
        }
    }
}
