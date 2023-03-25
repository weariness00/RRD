using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Action skill;
    public Vector3 motionSpeed;

    [HideInInspector] public Status status;
    [HideInInspector] public Animator animator;

    Dictionary<PlayerState, IState> dictionaryState = new Dictionary<PlayerState, IState>();
    public IState currentState { get; private set; }

    private void Start()
    {
        status = Util.GetORAddComponet<Status>(gameObject);
        animator = GetComponent<Animator>();

        dictionaryState.Add(PlayerState.Idle, new PlayerIdle());
        dictionaryState.Add(PlayerState.Walk, new PlayerWalk());

        currentState = dictionaryState[PlayerState.Idle];
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

    public void ChangeState(PlayerState state)
    {
        if (dictionaryState[state] == currentState)
            return;

        currentState.StateExit();
        currentState = dictionaryState[state];
        currentState.StateEnter(this);
    }
}

public interface IState
{
    void StateEnter<T>(T pc);
    void StateUpdate();
    void StateExit();
}

public enum PlayerState
{
    Idle,
    Walk,
    Run,
    Attak,
}

public class PlayerIdle : IState
{
    PlayerController controller;

    public void StateEnter<T>(T pc)
    {
        controller = pc as PlayerController;
        controller.animator.SetFloat("Speed", 0f);
    }

    public void StateExit()
    {

    }

    public void StateUpdate()
    {
        if (Input.GetKey(Managers.Key.InputAction(KeyToAction.MoveFront)) ||
            Input.GetKey(Managers.Key.InputAction(KeyToAction.MoveBack)) ||
            Input.GetKey(Managers.Key.InputAction(KeyToAction.MoveLeft)) ||
            Input.GetKey(Managers.Key.InputAction(KeyToAction.MoveRight)))
            controller.ChangeState(PlayerState.Walk);
    }
}

public class PlayerWalk : IState
{
    PlayerController controller;

    public void StateEnter<T>(T pc)
    {
        controller = pc as PlayerController;
        controller.animator.SetFloat("Speed", 1f);
    }

    public void StateExit()
    {

    }

    public void StateUpdate()
    {
        if (Input.GetKey(Managers.Key.InputAction(KeyToAction.MoveFront)))
            controller.Move(Vector3.forward);
        if (Input.GetKey(Managers.Key.InputAction(KeyToAction.MoveBack)))
            controller.Move(Vector3.back);
        if (Input.GetKey(Managers.Key.InputAction(KeyToAction.MoveLeft)))
            controller.Move(Vector3.left);
        if (Input.GetKey(Managers.Key.InputAction(KeyToAction.MoveRight)))
            controller.Move(Vector3.right);

        if(!Input.anyKey)
            controller.ChangeState(PlayerState.Idle);
    }
}