using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIView : MonoBehaviour
{
    void Start()
    {
        RefreshView();
    }

    protected abstract void RefreshView();
}
