using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerFSM
{
    public enum State
    {
        Idle,
        Walk,
        Run,
        Attack,
        Dead,
        LevelUp,
    }

    public class Idle : IStateMachine
    {
        PlayerController pc;
        float prevSpeed;

        public void StateEnter<T>(T component) where T : UnityEngine.Component
        {
            pc = component as PlayerController;
            prevSpeed = pc.animator.GetFloat("Speed");
            //pc.animator.SetFloat("Speed", 0f);
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
            if (Managers.Key.InputAction(KeyToAction.MoveFront) ||
                Managers.Key.InputAction(KeyToAction.MoveBack) ||
                Managers.Key.InputAction(KeyToAction.MoveLeft) ||
                Managers.Key.InputAction(KeyToAction.MoveRight))
            {
                if(Managers.Key.InputAction(KeyToAction.Run))
                    pc.PushState(State.Run);
                else
                    pc.PushState(State.Walk);
            }

            pc.animator.SetFloat("Speed", Mathf.Lerp(0f, prevSpeed, Time.deltaTime));
        }
    }

    public class Walk : IStateMachine
    {
        PlayerController pc;

        public void StateEnter<T>(T component) where T : UnityEngine.Component
        {
            pc = component as PlayerController;
            //pc.animator.SetFloat("Speed", 1f);
        }

        public void StateExit()
        {
            //pc.animator.SetFloat("Speed", 0f);
        }

        public void StatePause()
        {
        }

        public void StateResum()
        {
            //pc.animator.SetFloat("Speed", 1f);
        }

        public void StateUpdate()
        {
            if (Managers.Key.InputAction(KeyToAction.Run))
                pc.PushState(State.Run);

            if (Managers.Key.InputAction(KeyToAction.MoveFront))
                pc.Move(Vector3.forward);
            if (Managers.Key.InputAction(KeyToAction.MoveBack))
                pc.Move(Vector3.back);
            if (Managers.Key.InputAction(KeyToAction.MoveLeft))
                pc.Move(Vector3.left);
            if (Managers.Key.InputAction(KeyToAction.MoveRight))
                pc.Move(Vector3.right);

            if (!Managers.Key.InputAnyKey)
                pc.PopState();

            pc.animator.SetFloat("Speed", Mathf.Lerp(pc.animator.GetFloat("Speed"), 1f, Time.deltaTime));
        }
    }

    public class Run : IStateMachine
    {
        PlayerController pc;

        public void StateEnter<T>(T component) where T : Component
        {
            pc = component as PlayerController;
            //pc.animator.SetFloat("Speed", 2f);
            pc.status.speed += 1f;
        }

        public void StateExit()
        {
            pc.status.speed -= 1f;
        }

        public void StatePause()
        {
        }

        public void StateResum()
        {
        }

        public void StateUpdate()
        {
            if (Managers.Key.InputAction(KeyToAction.MoveFront))
                pc.Move(Vector3.forward);
            if (Managers.Key.InputAction(KeyToAction.MoveBack))
                pc.Move(Vector3.back);
            if (Managers.Key.InputAction(KeyToAction.MoveLeft))
                pc.Move(Vector3.left);
            if (Managers.Key.InputAction(KeyToAction.MoveRight))
                pc.Move(Vector3.right);

            if (!Managers.Key.InputAnyKey)
                pc.ChangeState(State.Idle);

            if (!Managers.Key.InputAction(KeyToAction.Run))
                pc.PopState();

            pc.animator.SetFloat("Speed", Mathf.Lerp(pc.animator.GetFloat("Speed"), 2f, Time.deltaTime * pc.status.speed));
        }
    }

    public class Attack : IStateMachine
    {
        PlayerController pc;

        public void StateEnter<T>(T component) where T : Component
        {
            pc = component as PlayerController;    
            pc.animator.SetBool("Attack", true);
            pc.AttackCall?.Invoke();
        }

        public void StateExit()
        {
            // �ִϸ��̼� ����
            pc.animator.SetBool("Attack", false);
        }

        public void StatePause()
        {
        }

        public void StateResum()
        {
        }

        public void StateUpdate()
        {
            // ���� ���� ���� �ٸ� Ű�� ������ ���� ���
            if (Managers.Key.InputAction(KeyToAction.MoveFront) ||
                Managers.Key.InputAction(KeyToAction.MoveBack) ||
                Managers.Key.InputAction(KeyToAction.MoveLeft) ||
                Managers.Key.InputAction(KeyToAction.MoveRight))
                pc.PopState();
            // �ִϸ��̼�
        }
    }

    public class Dead : IStateMachine
    {
        PlayerController pc;
        public void StateEnter<T>(T component) where T : Component
        {
            pc = component as PlayerController;

            int count = GameManager.Instance.alivePlayerCount--;
            if (count <= 0)
                GameManager.Instance.GameOver();
            // ��� �ִϸ��̼� ȣ��
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

    public class LevelUp : IStateMachine
    {
        PlayerController pc;
        public void StateEnter<T>(T component) where T : Component
        {
            pc = component as PlayerController;
            
            //������ �ִϸ��̼� ����
            // Ȥ�� ��ƼŬ ���ֱ�
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
            if(Input.anyKey)
                pc.PopState();
        }
    }
}