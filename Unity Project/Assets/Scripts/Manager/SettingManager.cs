using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour
{

    private void Start()
    {
		Managers.Instance.UpdateCall += OnOff;
		gameObject.SetActive(false);
    }

    void OnOff()
	{
		if (Managers.Key.InputActionDown(KeyToAction.Setting_UI))
		{
			gameObject.SetActive(!gameObject.activeSelf);
			if(Managers.Scene.CurrenScene.Type == SceneType.Game)
				GameManager.Instance.Pause();
		}
	}
}
