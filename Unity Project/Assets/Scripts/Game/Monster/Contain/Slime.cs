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
    }

    namespace SlimeFSM
    {
        public enum State
        {
            Idle,
            Patrol,
            Attack,
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
            float? distance;
            public void StateEnter<T>(T component) where T : Component
            {
                monster = component as Monster;
                monster.animator.SetBool("Patrol", true);
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
                if (monster.ftm.currentTarget != null && distance == null)
                {
                    monster.fsm.PushState(new Attack());
                    return;
                }

                float aniClipTime = Time.deltaTime + monster.animator.GetFloat("fPatrolTime");
                if (aniClipTime > 10.0f) aniClipTime = 0.0f;
                monster.animator.SetFloat("fPatrolTime", aniClipTime);

                if (aniClipTime > 7.0f) return;

                distance = monster.MoveToTarget();
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
            }
        }

        public class Die : IStateMachine
        {
            Monster monster;
            public void StateEnter<T>(T component) where T : Component
            {
                monster = component as Monster;
                monster.animator.SetBool("Die", true);
            }

            public void StateExit()
            {
                monster.animator.SetBool("Die", false);
            }

            public void StatePause()
            {
                monster.animator.SetBool("Die", false);
            }

            public void StateResum()
            {
                monster.animator.SetBool("Die", true);
            }

            public void StateUpdate()
            {
            }
        }
    }
}