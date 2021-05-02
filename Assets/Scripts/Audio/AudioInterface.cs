using UnityEngine;
using FMODUnity;
using System;
using UnityEngine.Events;
using System.Runtime.InteropServices;
using System.Collections;

public class AudioInterface : MonoBehaviour
{
    public FMOD.Studio.EventInstance m_BGM;
    public FMOD.Studio.EventInstance m_Water;

    public UnityEvent OnBeat;

    private FMOD.Studio.EVENT_CALLBACK beatCallback;
    private static AudioInterface callbackInstance;

    enum GameStateParam
    {
        Idle = 0,
        Lo = 1,
        Ready = 2,
        Set = 3,
        Race = 4
    }

    public void Start()
    {
        StartCoroutine(LoadSounds());
    }

    private void SetupCallback()
    {
        beatCallback = new FMOD.Studio.EVENT_CALLBACK(BeatEventCallback);
        callbackInstance = this;
        Validate(m_BGM.setCallback(beatCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT));
    }

    private void Validate(FMOD.RESULT result)
    {
        if (result == FMOD.RESULT.OK)
        {
            return;
        }

        Debug.LogError(result.ToString());
    }

    IEnumerator LoadSounds()
    {      
        RuntimeManager.WaitForAllLoads();

        while (!RuntimeManager.HasBanksLoaded || RuntimeManager.AnyBankLoading())
        {
            yield return null;
        }

        LoadSound(out m_BGM, "event:/Music/BGM1");
        LoadSound(out m_Water, "event:/Water");        

        m_BGM.start();
        m_Water.start();

        SetupCallback();
    }

    private void LoadSound(out FMOD.Studio.EventInstance sound, string eventName)
    {
        FMOD.Studio.LOADING_STATE loadingState = FMOD.Studio.LOADING_STATE.LOADING;
        FMOD.Studio.EventDescription eventDescription;

        sound = RuntimeManager.CreateInstance(eventName);
        Validate(sound.getDescription(out eventDescription));
        eventDescription.loadSampleData();
              
        while (loadingState == FMOD.Studio.LOADING_STATE.LOADING)
        {
            RuntimeManager.StudioSystem.update();
            Validate(eventDescription.getSampleLoadingState(out loadingState));
        }
    }

    [AOT.MonoPInvokeCallback(typeof(FMOD.Studio.EVENT_CALLBACK))]
    static FMOD.RESULT BeatEventCallback(FMOD.Studio.EVENT_CALLBACK_TYPE type, IntPtr instancePtr, IntPtr parameterPtr)
    {
        FMOD.Studio.TIMELINE_BEAT_PROPERTIES parameter = (FMOD.Studio.TIMELINE_BEAT_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_BEAT_PROPERTIES));

        if (parameter.beat == 1)
        {
            callbackInstance.OnBeat.Invoke();
        }

        return FMOD.RESULT.OK;
    }

    public void GetReady()
    {
        Validate(RuntimeManager.StudioSystem.setParameterByName("GameState", (float)GameStateParam.Ready));
    }

    public void GetSet()
    {
        Validate(RuntimeManager.StudioSystem.setParameterByName("GameState", (float)GameStateParam.Set));
    }

    public void Go()
    {
        Validate(RuntimeManager.StudioSystem.setParameterByName("GameState", (float)GameStateParam.Race));
    }

    public void SetBGMHi()
    {
        Validate(RuntimeManager.StudioSystem.setParameterByName("GameState", (float)GameStateParam.Race));
    }

    public void SetBGMLo()
    {
        Validate(RuntimeManager.StudioSystem.setParameterByName("GameState", (float)GameStateParam.Lo));
    }

    public void SetWaterSpeed(System.Single speed)
    {
        Validate(m_Water.setParameterByName("Speed", speed));
    }

    public void Splash()
    {
        FMOD.Studio.EventInstance instance;
        LoadSound(out instance, "event:/SFX/Splash");        
        Validate(instance.start());
    }

    public void Crunch()
    {
        FMOD.Studio.EventInstance instance;
        LoadSound(out instance, "event:/SFX/Crunch");
        Validate(instance.start());
    }

    public void Click()
    {
        FMOD.Studio.EventInstance instance;
        LoadSound(out instance, "event:/SFX/Click");
        Validate(instance.start());
    }

    public void Coin()
    {
        FMOD.Studio.EventInstance instance;
        LoadSound(out instance, "event:/SFX/Coin");
        Validate(instance.start());
    }
}
