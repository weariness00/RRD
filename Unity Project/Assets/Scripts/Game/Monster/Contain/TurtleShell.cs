using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monsters
{
    using TurtleShellFSM;

    public class TurtleShell : Monster
    {
        public bool isDefend = false;

        private void Start()
        {
            if (isOnIdle) fsm.SetDefaultState(new Idle());
            else fsm.SetDefaultState(new Patrol());
        }

        public override void Hit(float damage)
        {
            base.Hit(damage);

            if (!isDefend && status.hp < Mathf.Lerp(0, status.maxMp, 0.3f))
            {
                animator.SetFloat("Hit Type", 1.0f);
                isDefend = true;
            }

            if (CheckDie()) fsm.ChangeState(new Die());
            else fsm.ChangeState(new Hit());
        }
    }

    namespace TurtleShellFSM
    {
        public class Idle : IStateMachine
        {
            TurtleShell monster;
            public void StateEnter<T>(T component) where T : Component
            {
                monster = component as TurtleShell;
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
            TurtleShell monster;
            Vector3 direction;
            public void StateEnter<T>(T component) where T : Component
            {
                monster = component as TurtleShell;
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
            TurtleShell monster;
            public void StateEnter<T>(T component) where T : Component
            {
                monster = component as TurtleShell;
                monster.animator.SetTrigger("Attack");

                monster.StartCoroutine(SpecialAttack());
            }

            public void StateExit()
            {
                monster.StopCoroutine(SpecialAttack());
            }

            public void StatePause()
            {
                monster.StopCoroutine(SpecialAttack());
            }

            public void StateResum()
            {
                monster.animator.SetTrigger("Attack");
                monster.StartCoroutine(SpecialAttack());
            }

            public void StateUpdate()
            {
                if (monster.ftm.currentTarget == null) monster.fsm.PopState();

                monster.ftm.V2MoveToTarget();
                if (monster.ftm.distance < monster.status.range) monster.animator.SetBool("isAttackInside", true);
                else if (monster.isDefend && monster.ftm.distance < monster.status.range + 2.0f) monster.fsm.PushState(new Defend());
                else monster.animator.SetBool("isAttackInside", false);
            }

            WaitForSeconds specialAttackTimeDuration = new WaitForSeconds(1.0f);
            IEnumerator SpecialAttack()
            {
                while (true)
                {
                    monster.animator.SetFloat("Attack Type", 0);
                    yield return specialAttackTimeDuration;
                    monster.animator.SetFloat("Attack Type", 1);
                }
            }
        }

        public class Defend : IStateMachine
        {
            TurtleShell monster;

            public void StateEnter<T>(T component) where T : Component
            {
                monster = component as TurtleShell;

                monster.animator.SetTrigger("Defend");
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
                if (monster.ftm.distance < monster.status.range) monster.fsm.PopState();
            }
        }

        public class Hit : IStateMachine
        {
            TurtleShell monster;

            public void StateEnter<T>(T component) where T : Component
            {
                monster = component as TurtleShell;

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
            TurtleShell monster;
            public void StateEnter<T>(T component) where T : Component
            {
                monster = component as TurtleShell;
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