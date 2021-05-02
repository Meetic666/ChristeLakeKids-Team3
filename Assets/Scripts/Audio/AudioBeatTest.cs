using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBeatTest : MonoBehaviour
{
    public Animation m_animation;    
    
    public void OnBeat()
    {
        m_animation.Rewind();
        m_animation.Play();
    }
}
