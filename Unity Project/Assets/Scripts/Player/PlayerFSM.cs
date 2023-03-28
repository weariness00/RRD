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
    }

    public class Idle : IState
    {
        PlayerController pc;

        public void StateEnter<T>(T component) where T : UnityEngine.Component
        {
            pc = component as PlayerController;
            pc.animator.SetFloat("Speed", 0f);
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
            if (Input.GetKey(Managers.Key.InputAction(KeyToAction.MoveFront)) ||
                Input.GetKey(Managers.Key.InputAction(KeyToAction.MoveBack)) ||
                Input.GetKey(Managers.Key.InputAction(KeyToAction.MoveLeft)) ||
                Input.GetKey(Managers.Key.InputAction(KeyToAction.MoveRight)))
                pc.PushState(State.Walk);
        }
    }

    public class Walk : IState
    {
        PlayerController pc;

        public void StateEnter<T>(T component) where T : UnityEngine.Component
        {
            pc = component as PlayerController;
            pc.animator.SetFloat("Speed", 1f);
        }

        public void StateExit()
        {
            pc.animator.SetFloat("Speed", 0f);
        }

        public void StatePause()
        {
        }

        public void StateResum()
        {
            pc.animator.SetFloat("Speed", 1f);
        }

        public void StateUpdate()
        {
            if (Input.GetKey(Managers.Key.InputAction(KeyToAction.MoveFront)))
                pc.Move(Vector3.forward);
            if (Input.GetKey(Managers.Key.InputAction(KeyToAction.MoveBack)))
                pc.Move(Vector3.back);
            if (Input.GetKey(Managers.Key.InputAction(KeyToAction.MoveLeft)))
                pc.Move(Vector3.left);
            if (Input.GetKey(Managers.Key.InputAction(KeyToAction.MoveRight)))
                pc.Move(Vector3.right);

            if (!Input.anyKey)
                pc.ChangeState(State.Idle);
        }
    }

    public class Attack : IState
    {
        PlayerController pc;

        public void StateEnter<T>(T component) where T : Component
        {
            pc = component as PlayerController;
            pc.AttackCall?.Invoke();
        }

        public void StateExit()
        {
            // �ִϸ��̼� ����
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
            if (Input.GetKey(Managers.Key.InputAction(KeyToAction.MoveFront)) ||
                Input.GetKey(Managers.Key.InputAction(KeyToAction.MoveBack)) ||
                Input.GetKey(Managers.Key.InputAction(KeyToAction.MoveLeft)) ||
                Input.GetKey(Managers.Key.InputAction(KeyToAction.MoveRight)))
                pc.PopState();
            // �ִϸ��̼�
        }
    }

    public class Dead : IState
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
}