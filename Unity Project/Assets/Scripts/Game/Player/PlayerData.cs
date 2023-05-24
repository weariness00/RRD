using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerData
{
	public string name;
	public Status statsu;

	public void SaveData(string name)
	{
		var data = JsonUtility.ToJson(this);
		File.WriteAllText(Application.dataPath + "/Resources/Data/Player/" + name, data);
	}

	public PlayerData LoadData(string name)
	{
		var loadJson = Resources.Load<TextAsset>($"Data/Player/{name}");
		return JsonUtility.FromJson<PlayerData>(loadJson.ToString());
	}
}
