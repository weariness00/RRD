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

            //�÷��̾�� ü���� �˻��ϴ°� �ƴϳ�?
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
