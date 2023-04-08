using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyScene : BaseScene
{
    public GameObject charactorGrid;
    List<Toggle> charactorSlots;

    int selectCharactorIndex = -1;
    protected override void Init()
    {
        base.Init();
        
        foreach (Toggle toggle in Util.GetChildren<Toggle>(charactorGrid))
        {
            int temp = ++selectCharactorIndex;
            toggle.onValueChanged.AddListener((bool isOn) => { if(isOn) CharactorIndex(temp); });

            charactorSlots.Add(toggle);
        }
    }

    public override void Clear()
    {
    }

    public void GameStart()
    {
        Managers.Scene.LoadScene(SceneType.Game);
    }

    public void CharactorIndex(int index)
    {
        selectCharactorIndex = index;
    }
}
