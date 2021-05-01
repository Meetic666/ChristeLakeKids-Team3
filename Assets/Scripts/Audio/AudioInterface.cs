using UnityEngine;
using FMODUnity;

public class AudioInterface : MonoBehaviour
{
    public StudioEventEmitter m_BGM;
    public StudioEventEmitter m_Splash;

    public void SetBGMHi()
    {
        FMOD.RESULT result = RuntimeManager.StudioSystem.setParameterByName("GameState", 1);
        if (result != FMOD.RESULT.OK)
        {
            Debug.LogError(string.Format(("[FMOD] StudioGlobalParameterTrigger failed to set parameter {0} : result = {1}"), "GameState", result));
        }
    }

    public void SetBGMLo()
    {
        FMOD.RESULT result = RuntimeManager.StudioSystem.setParameterByName("GameState", 0);
        if (result != FMOD.RESULT.OK)
        {
            Debug.LogError(string.Format(("[FMOD] StudioGlobalParameterTrigger failed to set parameter {0} : result = {1}"), "GameState", result));
        }
    }

    public void Splash()
    {
        m_Splash.Play();
    }
}
