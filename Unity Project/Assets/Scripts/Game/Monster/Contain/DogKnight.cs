using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monsters
{
    public class DogKnight : Monster
    {
        public override IStateMachine ReturnTarget() { return new DogKnightFSM.Target(); }
        public override IStateMachine ReturnAttack() { return new DogKnightFSM.Attack(); }
    }

    namespace DogKnightFSM
    {
        public class Target : IStateMachine
        {
            DogKnight monster;
            public void StateEnter<T>(T component) where T : Component
            {
                monster = component as DogKnight;
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

        public class Attack : IStateMachine
        {
            DogKnight monster;
            Coroutine EndAttackCorounine;
            public void StateEnter<T>(T component) where T : Component
            {
                monster = component as DogKnight;
                EndAttackCorounine = monster.StartCoroutine(EndAttack());

                monster.animator.SetFloat("Attack Type", ChagneAttackType());

                monster.animator.SetTrigger("Attack");
            }

            public void StateExit()
            {
                monster.StopCoroutine(EndAttackCorounine);
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

            float ChagneAttackType()
            {
                float type = monster.animator.GetFloat("Attack Type") + 1f;
                if (type.Equals(2)) type = 0;

                return type;
            }

            IEnumerator EndAttack()
            {
                while (true)
                {
                    yield return null;
                    if (monster.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack Type")) break;
                }

                yield return new WaitForSeconds(monster.animator.GetCurrentAnimatorStateInfo(0).length - 0.1f);
                monster.fsm.PopState();
            }
        }
    }
}