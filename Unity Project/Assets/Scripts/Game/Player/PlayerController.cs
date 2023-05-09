using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using PlayerFSM;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public FSMStructer<PlayerController> fsm;
    [HideInInspector] public PlayerAnimationController animationController;
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
        animationController = GetComponent<PlayerAnimationController>();

        status = Util.GetORAddComponet<Status>(gameObject);
        animator = GetComponent<Animator>();

        fsm.SetDefaultState(new Idle());
    }

    private void Start()
    {
        Instantiate(animationController.waepon, rightHand.transform);
    }

    private void Update()
    {
        fsm.Update();

        if (Managers.Key.InputActionDown(KeyToAction.Attack))
            fsm.ChangeState(new Attack());

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