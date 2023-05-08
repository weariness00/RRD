using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using PlayerFSM;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public FSMStructer<PlayerController> fsm;
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
        fsm = new FSMStructer<PlayerController>(this);

        status = Util.GetORAddComponet<Status>(gameObject);
        animator = GetComponent<Animator>();

        fsm.SetDefaultState(new Idle());
    }

    private void Start()
    {
        //InitState();
    }

    private void Update()
    {
        //currentState.StateUpdate();
        fsm.Update();

        if (Managers.Key.InputActionDown(KeyToAction.Attack))
            fsm.ChangeState(new Attack());
            //ChangeState(State.Attack);

        if (status.LevelUP())
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
}