using UnityEngine;
using FMODUnity;
using System;
using UnityEngine.Events;
using System.Runtime.InteropServices;

public class AudioInterface : MonoBehaviour
{
    public StudioEventEmitter m_BGM;
    public StudioEventEmitter m_Splash;
    public StudioEventEmitter m_Crunch;
    public StudioEventEmitter m_Water;
    public StudioEventEmitter m_Click;

    public UnityEvent OnBeat;

    private FMOD.Studio.EVENT_CALLBACK beatCallback;

    private static AudioInterface callbackInstance;

    public void OnEnable()
    {
        beatCallback = new FMOD.Studio.EVENT_CALLBACK(BeatEventCallback);
        callbackInstance = this;
        m_BGM.EventInstance.setCallback(beatCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT);
    }

    [AOT.MonoPInvokeCallback(typeof(FMOD.Studio.EVENT_CALLBACK))]
    static FMOD.RESULT BeatEventCallback(FMOD.Studio.EVENT_CALLBACK_TYPE type, IntPtr instancePtr, IntPtr parameterPtr)
    {
        FMOD.Studio.TIMELINE_BEAT_PROPERTIES parameter = (FMOD.Studio.TIMELINE_BEAT_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_BEAT_PROPERTIES));

        if (parameter.beat == 1)
        {
            callbackInstance.OnBeat.Invoke();
        }

        Debug.Log(parameter.beat);
        return FMOD.RESULT.OK;
    }

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

    public void Click()
    {
        m_Click.Play();
    }
}
