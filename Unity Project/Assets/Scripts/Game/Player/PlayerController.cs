using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using PlayerFSM;

public class PlayerController : MonoBehaviour, IDamage
{
    [HideInInspector] public FSMStructer<PlayerController> fsm;
    [HideInInspector] public PlayerAnimationController animationController;
    [HideInInspector] public Status status;
    [HideInInspector] public Animator animator;
    public Equipment equipment;

    public Skill skill_Q;
    public Skill skill_E;
    public Vector3 motionSpeed;

    [HideInInspector] public UnityEvent AttackCall;
    [HideInInspector] public UnityEvent LevelUpCall;

    private void Awake()
    {
        fsm = new FSMStructer<PlayerController>(this);
        animationController = GetComponent<PlayerAnimationController>();

        status = Util.GetORAddComponet<Status>(gameObject);
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
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
            Managers.Damage.Attack(other.GetComponentInParent<Monster>(), status.damage.Cal() + equipment.weapon.status.damage.Cal());
        }
    }

    // 확인용 임시 메서드
    public void CreateWeapon()
    {
        int layer = (int)equipment.Equip(Instantiate(equipment.weapon));
        animator.SetInteger("Layer", layer);
        animator.SetLayerWeight(layer, 1);
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