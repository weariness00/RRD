using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using PlayerFSM;
using UnityEditor;

[CanEditMultipleObjects]
public class PlayerController : MonoBehaviour, IDamage
{
    [HideInInspector] public FSMStructer<PlayerController> fsm;
    [HideInInspector] public PlayerAnimationController animationController;
    [HideInInspector] public Status status;
    [HideInInspector] public Animator animator;

    public Skill skill_Q;
    public Skill skill_E;
    public Vector3 motionSpeed;

    [HideInInspector] public UnityEvent<GameObject> AttackCall; // 임시
    [HideInInspector] public Crowbar crowbar = new Crowbar(); // 임시
    [HideInInspector] public UnityEvent LevelUpCall;

    public bool outofcombat;

    private void Awake()
    {
        fsm = new FSMStructer<PlayerController>(this);
        animationController = GetComponent<PlayerAnimationController>();

        status = Util.GetORAddComponet<Status>(gameObject);
        animator = GetComponent<Animator>();

        outofcombat = true;

        fsm.SetDefaultState(new Idle());
    }

    private void Update()
    {
        fsm.Update();

        if (Managers.Key.InputActionDown(KeyToAction.Attack))
            fsm.PushState(new Attack());

        if (status.LevelUP())
            LevelUpCall?.Invoke();

        if (Managers.Key.InputActionDown(KeyToAction.Skill_Q))
            skill_Q.OnSkill();

        if (Managers.Key.InputActionDown(KeyToAction.Skill_E))
            skill_E.OnSkill();

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Monster")
        {
            AttackCall?.Invoke(other.gameObject);

            // 임시
            float damage;
            damage = crowbar.ItemEffect(status.damage.Cal());
            // ---------

            Managers.Damage.Attack(other.GetComponentInParent<Monster>(), damage);
        }
    }

    public void Move(Vector3 direction)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 2 * Time.deltaTime);
        transform.position += direction * status.speed.Cal() * Time.deltaTime;
    }

    public void ReSpawn()
    {
        GameManager.Instance.alivePlayerCount++;
    }

    public void Hit(float damage)
    {
        status.hp.value -= damage;

        fsm.PushState(new Hit());
    }

    public void HitParticle()
    {
    }
}