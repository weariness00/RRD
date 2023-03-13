using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Util
{
    public static T GetORAddComponet<T>(GameObject _Object) where T : UnityEngine.Component
    {
        T component = _Object.GetComponent<T>();
        if (component == null)
            component = _Object.AddComponent<T>();

        return component;
    }

    public static GameObject FindChild(GameObject _Object, string name = null, bool isRecursive = false)
    {
        Transform transform = FindChild<Transform>(_Object, name, isRecursive);
        if (transform == null)
            return null;

        return transform.gameObject;
    }

    public static T FindChild<T>(GameObject _Object, string name = null, bool isRecursive = false) where T : UnityEngine.Object
    {
        if (_Object == null)
            return null;

        if (isRecursive == false)
        {
            for (int i = 0; i < _Object.transform.childCount; i++)
            {
                Transform trasform = _Object.transform.GetChild(0);
                if (string.IsNullOrEmpty(name) || trasform.name == name)
                {
                    T component = trasform.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        else
        {
            foreach (T componen in _Object.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || componen.name == name)
                    return componen;
            }
        }

        return null;
    }
}

[System.Serializable]
public class MultKeyDictionary<Key1,Key2,Value> : Dictionary<Key1, Dictionary<Key2, Value>>
{
	public Value this[Key1 key1,Key2 key2]
	{
		get
		{
			if(!ContainsKey(key1) || !this[key1].ContainsKey(key2))
				throw new ArgumentOutOfRangeException();
			return base[key1][key2];
        }
        set
        {
            if (!ContainsKey(key1))
                this[key1] = new Dictionary<Key2, Value>();
            this[key1][key2] = value;
        }
    }

    public void Add(Key1 key1, Key2 key2, Value value)
    {
        if (!ContainsKey(key1))
            this[key1] = new Dictionary<Key2, Value>();
        this[key1][key2] = value;
    }

    public bool ContainsKey(Key1 key1, Key2 key2)
    {
        return base.ContainsKey(key1) && this[key1].ContainsKey(key2);
    }

    public new IEnumerable<Value> Values
    {
        get
        {
            return from baseDict in base.Values
                   from baseKey in baseDict.Keys
                   select baseDict[baseKey];
        }
    }
}