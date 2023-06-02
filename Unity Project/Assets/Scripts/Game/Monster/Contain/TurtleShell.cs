using Monsters.TurtleShellFSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monsters
{

    public class TurtleShell : Monster
    {
        public bool isDefend = false;
        public float skillDamage = 0; // юс╫ц©К

        public override IStateMachine ReturnTarget() { return new TurtleShellFSM.Target(); }
        public override IStateMachine ReturnAttack() { return new TurtleShellFSM.Attack(); }

        protected override void Attack()
        {
            float damage = status.damage.Cal() + skillDamage;
            Managers.Damage.Attack(ftm.currentTarget.GetComponent<PlayerController>(), damage);
        }

        public override void Hit(float damage)
        {
            base.Hit(damage);

            if (!isDefend && status.hp.value < Mathf.Lerp(0, status.maxMp.value, 0.3f))
            {
                animator.SetFloat("Hit Type", 1.0f);
                isDefend = true;
            }

            if (CheckDie())
            {
                isDefend = false;
            }

        }
    }

    namespace TurtleShellFSM
    {
        public class Target : IStateMachine
        {
            TurtleShell monster;
            public void StateEnter<T>(T component) where T : Component
            {
                monster = component as TurtleShell;
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
                else if (monster.isDefend && monster.ftm.distance < monster.status.range.Cal() + 2.0f) monster.fsm.PushState(new Defend());
            }
        }

        public class Attack : IStateMachine
        {
            TurtleShell monster;
            public void StateEnter<T>(T component) where T : Component
            {
                monster = component as TurtleShell;

                monster.animator.SetFloat("Attack Type", SetAttackType());

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

            float SetAttackType()
            {
                monster.skillDamage = 0;
                if (monster.status.mp.value < 10f) return 0;

                monster.skillDamage = 5;
                monster.status.mp.value -= 10f;
                return 1.0f;
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
                if (monster.ftm.distance > monster.status.range.Cal() + 1.0f) monster.fsm.PopState();
            }
        }
    }
}