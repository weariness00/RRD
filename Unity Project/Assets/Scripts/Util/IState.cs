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

public class FSMStructer<T> where T : UnityEngine.Component
{
    public T component;

    public FSMStructer(T _Component)
    {
        component = _Component;
    }

    public IState DefaultState;
    public IState CurrentState;
    Stack<IState> StackState = new Stack<IState>();

    public void Update()
    {
        CurrentState?.StateUpdate();
    }

    public void SetDefaultState(IState state)
    {
        DefaultState = state;
        PushState(DefaultState);
    }

    public void PushState(IState state)
    {
        CurrentState?.StatePause();

        CurrentState = state;
        CurrentState.StateEnter(component);
        StackState.Push(CurrentState);
    }

    public void PopState()
    {
        CurrentState?.StateExit();
        if(StackState.TryPop(out CurrentState))
        {
            CurrentState.StateResum();
        }
        else
        {
            CurrentState = DefaultState;
            CurrentState.StateEnter(component);
            StackState.Push(CurrentState);
        }
    }

    public void ChangeState(IState state)
    {
        while(StackState.TryPop(out CurrentState))
        {
            CurrentState.StateResum();
            CurrentState.StateExit();
        }

        CurrentState = state;
        CurrentState.StateEnter(component);
        StackState.Push(CurrentState);
    }
}