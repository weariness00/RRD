using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monsters
{
    using SlimeFSM;

    public class Slime : Monster
    {
        public void Start()
        {
            Init(new MonsterInfo());

            fsm = new FSMStructer<Monster>(this);
            animator = GetComponent<Animator>();

            if (isOnIdle) fsm.SetDefaultState(new Idle());
            else fsm.SetDefaultState(new Patrol());
        }

        public void Update()
        {
            fsm.Update();
        }

        public override void Hit(float damage)
        {
            base.Hit(damage);

            if (CheckDie()) fsm.ChangeState(new Die());
            else fsm.ChangeState(new Hit());           
        }
    }

    namespace SlimeFSM
    {
        public enum State
        {
            Idle,
            Patrol,
            Attack,
            Hit,
            Die,
        }
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
                    monster.fsm.PushState(new Attack());
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
                monster.transform.position += direction * monster.status.speed * Time.deltaTime;
            }
        }

        public class Attack : IStateMachine
        {
            Monster monster;
            public void StateEnter<T>(T component) where T : Component
            {
                monster = component as Monster;
                monster.animator.SetTrigger("Attack");

            }

            public void StateExit()
            {
            }

            public void StatePause()
            {
            }

            public void StateResum()
            {
                monster.animator.SetTrigger("Attack");
            }

            public void StateUpdate()
            {
                if (monster.ftm.currentTarget == null) monster.fsm.PopState();

                monster.ftm.V2MoveToTarget();
                if(monster.ftm.distance < monster.status.range) monster.animator.SetBool("isAttackInside", true);
                else monster.animator.SetBool("isAttackInside", false);
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
                    monster.fsm.ChangeState(new Patrol());
            }
        }

        public class Die : IStateMachine
        {
            Monster monster;
            public void StateEnter<T>(T component) where T : Component
            {
                monster = component as Monster;
                monster.animator.SetTrigger("Die");

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
}