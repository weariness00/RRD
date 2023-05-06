using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monsters
{
    using SlimeFSM;
    using System;

    public class Slime : Monster
    {

        public void Start()
        {
            fsm = new FSMStructer<Monster>(this);
            animator = GetComponent<Animator>();

            fsm.SetDefaultState(new Idle());
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
        public class Idle : IState
        {
            Monster monster;
            public void StateEnter<T>(T component) where T : Component
            {
                monster = component as Monster;
                monster.animator.SetTrigger("Idle");
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

        public class Patrol : IState
        {
            Monster monster;
            public void StateEnter<T>(T component) where T : Component
            {
                monster = component as Monster;
                monster.animator.SetTrigger("Patrol");
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
                if (monster.ftm.currentTarget != null) monster.fsm.PushState(new Attack());
            }
        }

        public class Attack : IState
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
            }

            public void StateUpdate()
            {
                if (monster.ftm.currentTarget == null) monster.fsm.PopState();
            }
        }

        public class Die : IState
        {
            Monster monster;
            public void StateEnter<T>(T component) where T : Component
            {
                monster = component as Monster;
                monster.animator.SetTrigger("Die");
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