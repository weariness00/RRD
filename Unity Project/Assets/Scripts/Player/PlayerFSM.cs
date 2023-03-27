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
        Attak,
    }

    public class Idle : IState
    {
        PlayerController controller;

        public void StateEnter<T>(T component) where T : UnityEngine.Component
        {
            controller = component as PlayerController;
            controller.animator.SetFloat("Speed", 0f);
        }

        public void StateExit()
        {

        }

        public void StateUpdate()
        {
            if (Input.GetKey(Managers.Key.InputAction(KeyToAction.MoveFront)) ||
                Input.GetKey(Managers.Key.InputAction(KeyToAction.MoveBack)) ||
                Input.GetKey(Managers.Key.InputAction(KeyToAction.MoveLeft)) ||
                Input.GetKey(Managers.Key.InputAction(KeyToAction.MoveRight)))
                controller.ChangeState(State.Walk);
        }
    }

    public class Walk : IState
    {
        PlayerController controller;

        public void StateEnter<T>(T component) where T : UnityEngine.Component
        {
            controller = component as PlayerController;
            controller.animator.SetFloat("Speed", 1f);
        }

        public void StateExit()
        {

        }

        public void StateUpdate()
        {
            if (Input.GetKey(Managers.Key.InputAction(KeyToAction.MoveFront)))
                controller.Move(Vector3.forward);
            if (Input.GetKey(Managers.Key.InputAction(KeyToAction.MoveBack)))
                controller.Move(Vector3.back);
            if (Input.GetKey(Managers.Key.InputAction(KeyToAction.MoveLeft)))
                controller.Move(Vector3.left);
            if (Input.GetKey(Managers.Key.InputAction(KeyToAction.MoveRight)))
                controller.Move(Vector3.right);

            if (!Input.anyKey)
                controller.ChangeState(State.Idle);
        }
    }
}