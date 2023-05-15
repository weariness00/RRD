using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monsters
{
    public class Slime : Monster
    {

        public override IStateMachine ReturnAttack() { return new SlimeFSM.Attack(); }
    }

    namespace SlimeFSM
    {
        public class Attack : IStateMachine
        {
            Slime monster;

            public void StateEnter<T>(T component) where T : Component
            {
                monster = component as Slime;
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
            }

            public void StateUpdate()
            {
            }
        }
    }
}