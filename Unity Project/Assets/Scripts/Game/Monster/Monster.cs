using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public enum MonsterType
{
    Ground,
    UnderGround,
    Fly
};

[System.Serializable]
public enum MonsterRate
{
    Normal,
    Elite,
    Boss
};

[System.Serializable]
public enum MonsterStatus
{
    Fire,
    Lighting,
    Water,
    Earth,
    Wind
};

[System.Serializable]
public class MonsterInfo
{
    public int id;
    public string name;

    [Space]
    public MonsterType type;
    public MonsterRate rate;
    public MonsterStatus status;

    [Space]
    public int hp;
    public int mp;

    [Space]
    public int damage;
    public float speed;
    public float range;
}

public class Monster : MonoBehaviour, IDamage
{
    static public UnityEvent<Transform> onDie;
    static public bool isOnIdle = false;

    public int id;

    [Space]
    public MonsterType type;
    public MonsterRate rate;

    [HideInInspector] public Animator animator;
    [HideInInspector] public Status status;
    public FSMStructer<Monster> fsm;
    public FindToMove ftm;
    ItemDropTable idt;

    [Space]
    public GameObject crowbar;

    public GameObject hitParticle;

    [SerializeField] protected List<QuestAction> deadQuestAction;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        status = Util.GetORAddComponet<Status>(gameObject);
        
        fsm = new FSMStructer<Monster>(this);
        ftm = Util.GetORAddComponet<FindToMove>(gameObject);
        idt = Util.GetORAddComponet<ItemDropTable>(gameObject);

        onDie = new UnityEvent<Transform>();

        if (isOnIdle) fsm.SetDefaultState(ReturnIdle());
        else fsm.SetDefaultState(ReturnPatrol());

        deadQuestAction = new List<QuestAction>() { QuestAction.Kill, QuestAction.Monster };
    }

    private void Update()
    {
        fsm.Update();
    }

    public void Init(MonsterInfo info)
    {
        status = Util.GetORAddComponet<Status>(gameObject);
        name = info.name;

        id = info.id;
        type = info.type;
        rate = info.rate;
        status.hp.value = info.hp;
        status.maxHp.value = info.hp;
        status.mp.value = info.mp;
        status.maxMp.value = info.mp;
        status.speed.value = info.speed;
        status.damage.value = info.damage;
        status.range.value = info.range;
    }

    protected virtual void Attack()
    {
        Managers.Damage.Attack(ftm.currentTarget.GetComponent<PlayerController>(), status.damage.Cal());
    }

    public bool CheckDie()
    {
        if (status.hp.Cal() > 0) return false;
        return true;
    }

    public void Dead(float dstroyTimeDuration)
    {
        gameObject.GetComponentsInChildren<Collider>()[0].enabled = false;

        MonsterSpawnManager.Instance.aliveMonsterCount--;
        
        onDie.Invoke(this.transform);

        QuestManager.Instance.SendQeustEvent(deadQuestAction.ToArray());

        idt.Loot();
        Destroy(gameObject, dstroyTimeDuration);
    }

    public void Dead()
    {
        Dead(3.0f);
    }

    public virtual IStateMachine ReturnIdle() { return new DefaultMonsterFSM.Idle(); }
    public virtual IStateMachine ReturnPatrol() { return new DefaultMonsterFSM.Patrol(); }
    public virtual IStateMachine ReturnTarget() { return new DefaultMonsterFSM.Target(); }
    public virtual IStateMachine ReturnAttack() { return new DefaultMonsterFSM.Attack(); }
    public virtual IStateMachine ReturnHit() { return new DefaultMonsterFSM.Hit(); }
    public virtual IStateMachine ReturnDie() { return new DefaultMonsterFSM.Die(); }

    public virtual void Hit(float damage)
    {
        if (status.hp.value >= status.maxHp.value * 0.9f)
        {
            damage = crowbar.GetComponent<Crowbar>().ItemEffect(damage);
        }
        status.hp.value -= damage;

        if (CheckDie()) fsm.ChangeState(ReturnDie());
        else fsm.ChangeState(ReturnHit());
    }

    public virtual void HitParticle()
    {
        //Instantiate(hitParticle, transform.position, Quaternion.identity);
    }
}

namespace DefaultMonsterFSM
{
    public class Idle : IStateMachine
    {
        Monster monster;
        public void StateEnter<T>(T component) where T : Component
        {
            monster = component as Monster;
            monster.animator.SetBool("Idle", true);
        }

        public void StateExit()
        {
            monster.animator.SetBool("Idle", false);
        }

        public void StatePause()
        {
            monster.animator.SetBool("Idle", false);
        }

        public void StateResum()
        {
            monster.animator.SetBool("Idle", true);
        }

        public void StateUpdate()
        {
        }   
    }

    public class Patrol : IStateMachine
    {
        Monster monster;
        Vector3 direction;
        public void StateEnter<T>(T component) where T : Component
        {
            monster = component as Monster;
            monster.animator.SetBool("Patrol", true);

            direction = Random.insideUnitSphere.normalized;
        }

        public void StateExit()
        {
            monster.animator.SetBool("Patrol", false);
        }

        public void StatePause()
        {
            monster.animator.SetBool("Patrol", false);
        }

        public void StateResum()
        {
            monster.animator.SetBool("Patrol", true);
        }

        public void StateUpdate()
        {
            if (monster.ftm.currentTarget != null)
            {
                monster.fsm.PushState(monster.ReturnTarget());
                return;
            }

            float aniClipTime = Time.deltaTime + monster.animator.GetFloat("fPatrolTime");
            if (aniClipTime > 10.0f) aniClipTime = 0.0f;
            monster.animator.SetFloat("fPatrolTime", aniClipTime);
            if (aniClipTime > 7.0f)
            {
                direction = Random.insideUnitSphere.normalized;
                return;
            }

            monster.transform.rotation = Quaternion.Slerp(monster.transform.rotation, Quaternion.LookRotation(direction), 2 * Time.deltaTime);
            monster.transform.position += direction * monster.status.speed.Cal() * Time.deltaTime;
        }
    }

    public class Attack : IStateMachine
    {
        Monster monster;
        public void StateEnter<T>(T component) where T : Component
        {
            monster = component as Monster;

            monster.animator.SetTrigger("Attack");
            monster.StartCoroutine(EndAttack());
        }

        public void StateExit()
        {
            monster.StopCoroutine(EndAttack());
        }

        public void StatePause()
        {
        }

        public void StateResum()
        {
        }

        public void StateUpdate()
        {

        }

        IEnumerator EndAttack()
        {
            while (true)
            {
                yield return null;
                if (monster.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack Type")) break;
            }

            yield return new WaitForSeconds(monster.animator.GetCurrentAnimatorStateInfo(0).length);
            monster.fsm.PopState();
        }
    }

    public class Target : IStateMachine
    {
        Monster monster;
        public void StateEnter<T>(T component) where T : Component
        {
            monster = component as Monster;
            monster.animator.SetBool("Target", true);
        }

        public void StateExit()
        {
            monster.animator.SetBool("Target", false);
        }

        public void StatePause()
        {
            monster.animator.SetBool("Target", false);
        }

        public void StateResum()
        {
            monster.animator.SetBool("Target", true);
        }

        public void StateUpdate()
        {
            monster.ftm.V2MoveToTarget();
            if (monster.ftm.distance < monster.status.range.Cal()) monster.fsm.PushState(monster.ReturnAttack());
        }
    }

    public class Hit : IStateMachine
    {
        Monster monster;

        public void StateEnter<T>(T component) where T : Component
        {
            monster = component as Monster;
            monster.animator.SetTrigger("Hit");
        }

        public void StateExit()
        {
        }

        public void StatePause()
        {
        }

        public void StateResum()
        {
        }

        public void StateUpdate()
        {
            if (monster.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
                monster.fsm.ChangeState(monster.ReturnPatrol());
        }
    }
    public class Die : IStateMachine
    {
        Monster monster;
        public void StateEnter<T>(T component) where T : Component
        {
            monster = component as Monster;
            monster.animator.SetTrigger("Die");

            Debug.Log("죽음");

            monster.Dead();
        }

        public void StateExit()
        {
        }

        public void StatePause()
        {
        }

        public void StateResum()
        {
        }

        public void StateUpdate()
        {
        }
    }
}