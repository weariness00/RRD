using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcelDataManager
{
	Dictionary<string, ScriptableObject> dataList = new Dictionary<string, ScriptableObject>();

	public T Load<T>(string path) where T : ScriptableObject
	{
		ScriptableObject obj = null;

		if (dataList.TryGetValue(path, out obj)) return obj as T;

        string _Path = $"Data/{path}";
		T data = Resources.Load<ScriptableObject>(_Path) as T;

		if(data == null)
		{
			Debug.LogWarning($"{_Path}에 ScriptableObject가 존재하지 않습니다.");
			return null;
		}

		return data;
	}
}
