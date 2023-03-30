using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void GameOver()
    {
        Managers.Scene.LoadScene(SceneType.GameOver);
    }
}
