using UnityEngine;
using FMODUnity;

public class AudioInterface : MonoBehaviour
{
    public StudioEventEmitter m_BGM;
    public StudioEventEmitter m_Splash;
    public StudioEventEmitter m_Crunch;
    public StudioEventEmitter m_Water;

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

    public void SetWaterSpeed(System.Single speed)
    {
        m_Water.SetParameter("Speed", speed);
    }

    public void Splash()
    {
        m_Splash.Play();
    }

    public void Crunch()
    {
        m_Crunch.Play();
    }
}
