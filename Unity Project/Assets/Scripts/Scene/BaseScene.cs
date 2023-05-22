using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneType
{
    Unknown,
    Login,
    Lobby,
    Game,
    GameEnd,
    GameOver,
}

public abstract class BaseScene : MonoBehaviour
{
    public SceneType Type { get; protected set; } = SceneType.Unknown;
    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
	{
        Managers.Instance.enabled = true;
	}

    public abstract void Clear();
}
