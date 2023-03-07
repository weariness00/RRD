using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{
    private List<IObserver> observerList = new List<IObserver>();

    public void AddObserver(IObserver observer) { observerList.Add(observer); }
    public void RemoveObserver(IObserver observer) { observerList.Remove(observer); }

    protected void NotifyObserver()
    {
        foreach (IObserver observer in observerList)
        {
            observer.OnNotify();
        }
    }
}
