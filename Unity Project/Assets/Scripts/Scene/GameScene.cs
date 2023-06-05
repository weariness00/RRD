using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScene : BaseScene
{
    public void Start()
    {
        GameManager.Instance.difficulty.SetDifficulty(LobbyScene.difficultyType);
        Destroy(GameObject.FindObjectOfType<LobbyScene>());
    }
    protected override void Init()
    {
        base.Init();

        Type = SceneType.Game;

        Time.timeScale = 1;
    }

    public override void Clear()
    {
    }

    public void GameEnd()
    {
        Managers.Scene.LoadScene(SceneType.GameEnd);
    }
}