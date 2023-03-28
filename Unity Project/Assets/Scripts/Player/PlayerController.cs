using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using PlayerFSM;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public Action skill;
    public Vector3 motionSpeed;
    [HideInInspector] public UnityEvent AttackCall;

    [HideInInspector] public Status status;
    [HideInInspector] public Animator animator;

    Dictionary<PlayerFSM.State, IState> dictionaryState = new Dictionary<PlayerFSM.State, IState>();
    [HideInInspector] public Stack<IState> stateStack = new Stack<IState>();
    [HideInInspector] public IState currentState {  get; private set; }

    private void Start()
    {
        status = Util.GetORAddComponet<Status>(gameObject);
        animator = GetComponent<Animator>();

        dictionaryState.Add(PlayerFSM.State.Idle, new Idle());
        dictionaryState.Add(PlayerFSM.State.Walk, new Walk());
        dictionaryState.Add(PlayerFSM.State.Walk, new Attack());
        dictionaryState.Add(PlayerFSM.State.Walk, new Dead());

        currentState = dictionaryState[PlayerFSM.State.Idle];
        stateStack.Push(currentState);
        currentState.StateEnter(this);
    }
    private void Update()
    {
        currentState.StateUpdate();
    }

    public void Move(Vector3 direction)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 2 * Time.deltaTime);
        transform.position += direction * status.speed * Time.deltaTime;
    }

    public void ReSpawn()
    {
        GameManager.Instance.alivePlayerCount++;
    }

    public void PushState(PlayerFSM.State state)
    {
        if (dictionaryState[state] == currentState)
            return;

        currentState.StatePause();
        currentState = dictionaryState[state];
        stateStack.Push(currentState);
        currentState.StateEnter(this);
    }

    public void PopState()
    {
        currentState.StateExit();
        stateStack.Pop();

        if (stateStack.Count.Equals(0))
            stateStack.Push(dictionaryState[PlayerFSM.State.Idle]);

        currentState = stateStack.Peek();
        currentState.StateResum();
    }

    public void ChangeState(PlayerFSM.State state)
    {
        if (dictionaryState[state] == currentState)
            return;

        currentState.StateExit();
        stateStack.Clear();

        currentState = dictionaryState[state];
        stateStack.Push(currentState);
        currentState.StateEnter(this);
    }
}

