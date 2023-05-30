using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{
    public static DifficultyType difficultyType = DifficultyType.None;

    protected override void Init()
    {
        base.Init();

        Type = SceneType.Lobby;
    }

    public override void Clear()
    {
    }

    public void GameStart()
    {
        Managers.Scene.LoadScene(SceneType.Game);
    }

    public void SetDifficulty(int typeIndex) { difficultyType = (DifficultyType)typeIndex; }
}
