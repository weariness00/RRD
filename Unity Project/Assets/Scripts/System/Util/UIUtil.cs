using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIUtil : MonoBehaviour
{
    Dictionary<Type, UnityEngine.Object[]> objectDictionary = new Dictionary<Type, UnityEngine.Object[]>();


    protected void Bind<T>(GameObject _Object, string[] names) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objs;
        if (objectDictionary.ContainsKey(typeof(T)))
        {
            objs = objectDictionary[typeof(T)];
            Array.Resize(ref objs, objs.Length + names.Length);
        }
        else
        {
            objs = new UnityEngine.Object[names.Length];
            objectDictionary.Add(typeof(T), objs);
        }

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
                objs[i] = Util.FindChild(_Object, names[i], true);
            else
                objs[i] = Util.FindChild<T>(_Object, names[i], true);

            if (objs[i] == null)
                Debug.LogWarning($"Failed To Bind({names[i]})");
        }
    }
    protected void Bind<T>(GameObject _Object,Type type) where T : UnityEngine.Object  { Bind<T>(_Object, Enum.GetNames(type));  }
    protected void Bind<T>(Type type) where T : UnityEngine.Object{ Bind<T>(gameObject, type); }
    protected void Bind<T>(string[] names) where T : UnityEngine.Object{ Bind<T>(gameObject, names); }

    protected T Get<T>(int index) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objs = null;
        if (objectDictionary.TryGetValue(typeof(T), out objs) == false)
            return null;

        return objs[index] as T;
    }
    protected T[] Gets<T>() where T : UnityEngine.Object { return objectDictionary[typeof(T)] as T[];   }

    protected Text GetText(int index) { return Get<Text>(index); }
    protected Button GetButton(int index) { return Get<Button>(index); }
    protected Image GetImage(int index) { return Get<Image>(index); }
    protected InputField GetInputFiled(int index) { return Get<InputField>(index); }
}