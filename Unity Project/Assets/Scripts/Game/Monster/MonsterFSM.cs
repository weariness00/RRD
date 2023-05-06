using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterFSM
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
            monster.Idle.Enter?.Invoke();
        }

        public void StateExit()
        {
            monster.Idle.Exit?.Invoke();
        }

        public void StatePause()
        {
            monster.Idle.Pause?.Invoke();
        }

        public void StateResum()
        {
            monster.Idle.Resum?.Invoke();
        }

        public void StateUpdate()
        {
            monster.Idle.Update?.Invoke(); 
        }
    }

    public class Patrol : IState
    {
        Monster monster;
        public void StateEnter<T>(T component) where T : Component
        {
            monster = component as Monster;
            monster.Patrol.Enter?.Invoke();
        }

        public void StateExit()
        {
            monster.Patrol.Exit?.Invoke();
        }

        public void StatePause()
        {
            monster.Patrol.Pause?.Invoke();
        }

        public void StateResum()
        {
            monster.Patrol.Resum?.Invoke();
        }

        public void StateUpdate()
        {
            monster.Patrol.Update?.Invoke();
        }
    }

    public class Attack : IState
    {
        Monster monster;
        public void StateEnter<T>(T component) where T : Component
        {
            monster = component as Monster;
            monster.Attack.Enter?.Invoke();
        }

        public void StateExit()
        {
            monster.Attack.Exit?.Invoke();
        }

        public void StatePause()
        {
            monster.Attack.Pause?.Invoke();
        }

        public void StateResum()
        {
            monster.Attack.Resum?.Invoke();
        }

        public void StateUpdate()
        {
            monster.Attack.Update?.Invoke();
        }
    }

    public class Die : IState
    {
        Monster monster;
        public void StateEnter<T>(T component) where T : Component
        {
            monster = component as Monster;
            monster.Die.Enter?.Invoke();
        }

        public void StateExit()
        {
            monster.Die.Exit?.Invoke();
        }

        public void StatePause()
        {
            monster.Die.Pause?.Invoke();
        }

        public void StateResum()
        {
            monster.Die.Resum?.Invoke();
        }

        public void StateUpdate()
        {
            monster.Die.Update?.Invoke();
        }
    }
}
