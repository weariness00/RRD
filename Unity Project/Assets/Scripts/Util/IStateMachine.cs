using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IStateMachine
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

    public IStateMachine DefaultState;
    public IStateMachine CurrentState;
    Stack<IStateMachine> StackState = new Stack<IStateMachine>();

    public void Update()
    {
        CurrentState?.StateUpdate();
    }

    public void SetDefaultState(IStateMachine state)
    {
        DefaultState = state;
        PushState(DefaultState);
    }

    public void PushState(IStateMachine state)
    {
        CurrentState?.StatePause();

        CurrentState = state;
        CurrentState.StateEnter(component);
        StackState.Push(CurrentState);
    }

    public void PopState()
    {
        CurrentState?.StateExit();
        if(StackState.Count > 0) StackState.Pop();
        if (StackState.TryPop(out CurrentState))
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

    public void ChangeState(IStateMachine state)
    {
        while(StackState.TryPop(out CurrentState))
        {
            CurrentState.StateExit();
        }

        CurrentState = state;
        CurrentState.StateEnter(component);
        StackState.Push(CurrentState);
    }
}