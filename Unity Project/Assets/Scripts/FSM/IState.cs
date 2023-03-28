using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void StateEnter<T>(T component) where T : UnityEngine.Component;
    void StateUpdate();
    void StateExit();

    void StatePause();

    void StateResum();
}