using PocketValues.Types;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : SingletonBehaviour<GameController>
{
    [SerializeField]
    private StringReference m_GameTitle = new StringReference();

    public string GameTitle
    {
        get { return this.m_GameTitle.Value; }
    }
}
