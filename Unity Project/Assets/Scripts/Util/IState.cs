using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IState
{
    void StateEnter<T>(T component) where T : UnityEngine.Component;
    void StateUpdate();
    void StateExit();

    void StatePause();

    void StateResum();
}

public struct StateEvent
{
    public UnityEvent Enter;
    public UnityEvent Exit;
    public UnityEvent Pause;
    public UnityEvent Resum;
    public UnityEvent Update;
}
