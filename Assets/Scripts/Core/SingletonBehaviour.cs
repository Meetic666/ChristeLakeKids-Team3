using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonBehaviour<T> : MonoBehaviour
    where T : SingletonBehaviour<T>
{
    private static SingletonBehaviour<T> s_Instance = null;

    public static T Instance
    {
        get
        {
            return (T)s_Instance;
        }
    }

    void OnEnable()
    {
        if (Instance == null)
        {
            s_Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
