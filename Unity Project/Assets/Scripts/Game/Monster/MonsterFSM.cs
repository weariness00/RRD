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

    public class Die : IStateMachine
    {
        Monster ms;
        public void StateEnter<T>(T component) where T : Component
        {
            ms = component as Monster;

            //플레이어는 체력을 검사하는게 아니네?
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
