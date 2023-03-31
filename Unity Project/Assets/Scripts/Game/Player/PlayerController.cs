using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using PlayerFSM;
using UnityEngine.Events;
using System.Security.Cryptography;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public Status status;
    [HideInInspector] public Animator animator;
    [HideInInspector] public Weapon WeaponEquipment;

    [SerializeField] GameObject leftHand;
    [SerializeField] GameObject rightHand;

    public Action skill;
    public Vector3 motionSpeed;

    [HideInInspector] public UnityEvent AttackCall;
    [HideInInspector] public UnityEvent LevelUpCall;

    private void Awake()
    {
        status = Util.GetORAddComponet<Status>(gameObject);
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        InitState();
    }
    private void Update()
    {
        currentState.StateUpdate();

        if(status.LevelUP())
            LevelUpCall?.Invoke();
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


    #region 상태 머신

    Dictionary<PlayerFSM.State, IState> dictionaryState = new Dictionary<PlayerFSM.State, IState>();
    [HideInInspector] public Stack<IState> stateStack = new Stack<IState>();
    [HideInInspector] public IState currentState { get; private set; }
    void InitState()
    {
        dictionaryState.Add(PlayerFSM.State.Idle, new Idle());
        dictionaryState.Add(PlayerFSM.State.Walk, new Walk());
        dictionaryState.Add(PlayerFSM.State.Run, new Run());
        dictionaryState.Add(PlayerFSM.State.Attack, new Attack());
        dictionaryState.Add(PlayerFSM.State.Dead, new Dead());
        dictionaryState.Add(PlayerFSM.State.LevelUp, new LevelUp());

        currentState = dictionaryState[PlayerFSM.State.Idle];
        stateStack.Push(currentState);
        currentState.StateEnter(this);
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

    #endregion
}

