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
            public void StateEnter<T>(T component) where T : Component
            {
                monster = component as DogKnight;

                monster.animator.SetFloat("Attack Type", 0.0f); // юс╫ц

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
    }
}