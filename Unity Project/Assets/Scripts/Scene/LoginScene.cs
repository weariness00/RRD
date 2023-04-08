using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScene : BaseScene
{

    public void Login()
    {
        Managers.Scene.LoadScene(SceneType.Lobby);
    }

    protected override void Init()
    {
        base.Init();

        Type = SceneType.Login;
    }
    public override void Clear()
    {
        Debug.Log($"{Type}Scene Clear");
    }
}
