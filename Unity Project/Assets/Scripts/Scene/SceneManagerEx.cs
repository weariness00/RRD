using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
	public BaseScene CurrenScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }

	public void LoadScene(SceneType type)
	{
		Managers.Sound.Clear();

		CurrenScene.Clear();
		SceneManager.LoadScene($"{type.ToString()}Scene");
	}
}
