using System.IO;
using UnityEngine;

public class JsonUtil
{
    public static T LoadFile<T>(string path) where T : class
	{
        string _Path = $"Data/{path}";
        TextAsset textAsset = Resources.Load<TextAsset>(_Path);
		if(textAsset == null)
		{
			Debug.LogWarning($"{_Path}에 파일이 존재하지 않습니다.");
			return null;
		}

        T data = JsonUtility.FromJson<T>(textAsset.text);
		return data;
    }

	public static void SaveFile<T>(T data, string path)
	{
		string defualtPath = $"{Application.dataPath}/Resources/Data/{path}.Json";
		string json = JsonUtility.ToJson(data, true);

        File.WriteAllText(defualtPath, json);
    }
}