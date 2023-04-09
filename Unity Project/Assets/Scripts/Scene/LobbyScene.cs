using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
    }

    public override void Clear()
    {
    }

    public void GameStart()
    {
        Managers.Scene.LoadScene(SceneType.Game);
    }
}
