using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Attach this script to an entity with a TextMeshPro - Text (UI) component.
// Use that component to configure where the text will appear.

public class TimeCountUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        m_StartTime = Time.time;
        m_TextComponent = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        m_TextComponent.text = GetElapsedTimeString();
    }

    public int GetSecondsElapsed()
    {
        return (int)(Time.time - m_StartTime);
    }

    public string GetElapsedTimeString()
    {
        int elapsedTime = GetSecondsElapsed();
        int minutes = elapsedTime / 60;
        int seconds = elapsedTime - (60 * minutes);
        return minutes.ToString() + ":" + (seconds < 10 ? "0" : "") + seconds.ToString();
    }

    public TextMeshProUGUI m_TextComponent;
    public float m_StartTime;
}
