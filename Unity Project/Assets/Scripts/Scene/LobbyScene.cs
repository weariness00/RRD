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

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1f;
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
