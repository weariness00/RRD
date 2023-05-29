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
			Debug.LogWarning($"{_Path}�� ������ �������� �ʽ��ϴ�.");
			return null;
		}

        T data = JsonUtility.FromJson<T>(textAsset.text);
		return data;
    }

	public static void SaveFile<T>(T data, string path)
	{
		string defualtPath = $"Assets/Resourecs/Data/{path}.json";
        string json = File.ReadAllText(defualtPath);
    }
}