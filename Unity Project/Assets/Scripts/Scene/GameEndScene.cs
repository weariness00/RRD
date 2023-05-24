using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndScene : BaseScene
{

    protected override void Init()
    {
        base.Init();
        
        Type = SceneType.GameEnd;

        Managers.Scene.LoadScene(SceneType.Lobby);
    }

    public override void Clear()
    {
    }
}
