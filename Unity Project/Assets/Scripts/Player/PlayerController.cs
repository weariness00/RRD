using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using PlayerFSM;

public class PlayerController : MonoBehaviour
{
    public Action skill;
    public Vector3 motionSpeed;

    [HideInInspector] public Status status;
    [HideInInspector] public Animator animator;

    Dictionary<PlayerFSM.State, IState> dictionaryState = new Dictionary<PlayerFSM.State, IState>();
    public IState currentState { get; private set; }

    private void Start()
    {
        status = Util.GetORAddComponet<Status>(gameObject);
        animator = GetComponent<Animator>();

        dictionaryState.Add(PlayerFSM.State.Idle, new Idle());
        dictionaryState.Add(PlayerFSM.State.Walk, new Walk());

        currentState = dictionaryState[PlayerFSM.State.Idle];
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

    public void Dead()
    {
        int count = GameManager.Instance.alivePlayerCount--;
        if (count <= 0)
            GameManager.Instance.GameOver();
    }

    public void ChangeState(PlayerFSM.State state)
    {
        if (dictionaryState[state] == currentState)
            return;

        currentState.StateExit();
        currentState = dictionaryState[state];
        currentState.StateEnter(this);
    }
}

