using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        Type = SceneType.Game;
    }

    public override void Clear()
    {
    }
}
