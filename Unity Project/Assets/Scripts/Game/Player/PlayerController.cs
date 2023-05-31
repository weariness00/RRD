using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using PlayerFSM;
using UnityEditor;

[CanEditMultipleObjects]
public class PlayerController : MonoBehaviour, IDamage
{
    public Transform LookTransform;
    [HideInInspector] public FSMStructer<PlayerController> fsm;
    [HideInInspector] public PlayerAnimationController animationController;
    [HideInInspector] public Status status;

    [HideInInspector] public Animator animator;
    [HideInInspector] public Collider collider;

    public Skill skill_Q;
    public Skill skill_E;
    public Vector3 motionSpeed;

    [HideInInspector] public UnityEvent<GameObject> AttackCall; // 임시
    [HideInInspector] public Crowbar crowbar = new Crowbar(); // 임시
    [HideInInspector] public UnityEvent LevelUpCall;

    public bool outofcombat;
    public bool isStop = false;

    protected virtual void Awake()
    {
        fsm = new FSMStructer<PlayerController>(this);
        animationController = GetComponent<PlayerAnimationController>();
        status = Util.GetORAddComponet<Status>(gameObject);

        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();

        outofcombat = true;

        fsm.SetDefaultState(new Idle());
    }

    protected virtual void Start()
    {
        PlayerDataExcel data = Managers.Excel.Load<PlayerDataExcel>("Player/PlayerDataExcel");
        var statusData = data.Status.Find((s) => { return s.Name.Equals(name); });
        if (statusData != null) status.SetData(statusData);
    }

    protected virtual void Update()
    {
        if (GameManager.Instance.isPause) return;
        if (status.isDead) return;

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

    protected virtual void OnTriggerEnter(Collider other)
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
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 10.0f* Time.deltaTime);
        transform.position += direction * status.speed.Cal() * Time.deltaTime;
    }

    public Vector3 MoveMent()
    {
        Vector3 dir = Vector3.zero;

        if (Managers.Key.InputAction(KeyToAction.MoveFront))
            dir += LookTransform.forward;
        if (Managers.Key.InputAction(KeyToAction.MoveBack))
            dir += -LookTransform.forward;
        if (Managers.Key.InputAction(KeyToAction.MoveLeft))
            dir += -LookTransform.right;
        if (Managers.Key.InputAction(KeyToAction.MoveRight))
            dir += LookTransform.right;

        dir.y = 0;
        return dir;
    }

    public void ReSpawn()
    {
        GameManager.Instance.alivePlayerCount++;
    }

    public void Hit(float damage)
    {
        if(status.isDead) return;

        status.hp.value -= damage;

        if (status.CheckDead()) fsm.ChangeState(new Die());
        else fsm.ChangeState(new Hit());
    }

    public void HitParticle()
    {
    }
}