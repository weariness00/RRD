using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUtil : MonoBehaviour
{
    Dictionary<Type, UnityEngine.Object[]> objectDictionary = new Dictionary<Type, UnityEngine.Object[]>();

    protected void Bind<T>(Type type) where T : UnityEngine.Object
	{
        string[] names = Enum.GetNames(type);
        UnityEngine.Object[] objs = new UnityEngine.Object[names.Length];
        objectDictionary.Add(typeof(T), objs);

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
                objs[i] = Util.FindChild(gameObject, names[i], true);
            else
                objs[i] = Util.FindChild<T>(gameObject, names[i], true);

            if (objs[i] == null)
                Debug.LogWarning($"Failed To Bind({names[i]})");
        }
    }

    protected T Get<T>(int index) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objs = null;
        if (objectDictionary.TryGetValue(typeof(T), out objs) == false)
            return null;

        return objs[index] as T;
    }

    protected Text GetText(int index) { return Get<Text>(index); }
    protected Button GetButton(int index) { return Get<Button>(index); }
    protected Image GetImage(int index) { return Get<Image>(index); }
    protected InputField GetInputFiled(int index) { return Get<InputField>(index); }
}