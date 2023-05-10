using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

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
                    pc.fsm.PushState(new Run());
                else
                    pc.fsm.PushState(new Walk());

                return;
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
            pc.status.speed = 1.0f;
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
                pc.fsm.PushState(new Run());

            Vector3 dir = Vector3.zero;

            if (Managers.Key.InputAction(KeyToAction.MoveFront))
                dir += Vector3.forward;
            if (Managers.Key.InputAction(KeyToAction.MoveBack))
                dir += Vector3.back;
            if (Managers.Key.InputAction(KeyToAction.MoveLeft))
                dir += Vector3.left;
            if (Managers.Key.InputAction(KeyToAction.MoveRight))
                dir += Vector3.right;

            if (!Managers.Key.InputAnyKey)
            {
                pc.fsm.PopState();
                return;
            }

            pc.Move(dir);
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
            Vector3 dir = Vector3.zero;

            if (Managers.Key.InputAction(KeyToAction.MoveFront))
                dir += Vector3.forward;
            if (Managers.Key.InputAction(KeyToAction.MoveBack))
                dir += Vector3.back;
            if (Managers.Key.InputAction(KeyToAction.MoveLeft))
                dir += Vector3.left;
            if (Managers.Key.InputAction(KeyToAction.MoveRight))
                dir += Vector3.right;

            if (dir.Equals(Vector3.zero))
            {
                pc.fsm.PopState();
                return;
            }

            pc.Move(dir);


            if (!Managers.Key.InputAnyKey)
            {
                pc.fsm.ChangeState(new Idle());
                return;
            }

            if (!Managers.Key.InputAction(KeyToAction.Run))
            {
                pc.fsm.PopState();
                return;
            }

            pc.animator.SetFloat("Speed", Mathf.Lerp(pc.animator.GetFloat("Speed"), 2f, Time.deltaTime * pc.status.speed));
        }
    }

    public class Attack : IStateMachine
    {
        PlayerController pc;
        public void StateEnter<T>(T component) where T : Component
        {
            pc = component as PlayerController;
            pc.animator.ResetTrigger("EndAttack");
            pc.animator.SetTrigger("StartAttack");
            pc.AttackCall?.Invoke();

            pc.StartCoroutine(EndAttack());
        }

        public void StateExit()
        {
            pc.equipment.weapon.GetComponent<BoxCollider>().enabled = false;
        }

        public void StatePause()
        {
        }

        public void StateResum()
        {
        }

        public void StateUpdate()
        {
            // 만약 공격 도중 다른 키를 누르면 공격 취소
            if (Managers.Key.InputAction(KeyToAction.MoveFront) ||
                Managers.Key.InputAction(KeyToAction.MoveBack) ||
                Managers.Key.InputAction(KeyToAction.MoveLeft) ||
                Managers.Key.InputAction(KeyToAction.MoveRight))
            {
                pc.animator.SetTrigger("EndAttack");
                pc.StopCoroutine(EndAttack());
                pc.fsm.PopState();
                return;
            }

            // 애니메이션
        }

        IEnumerator EndAttack()
        {
            AnimatorStateInfo clip;
            while(true)
            {
                yield return null;
                clip = pc.animator.GetCurrentAnimatorStateInfo(pc.animator.GetInteger("Layer"));
                if (clip.IsName("Attack")) break;
            }

            yield return new WaitForSeconds(clip.length);
            pc.fsm.PopState();
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
            // 사망 애니메이션 호출
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
            
            //레벨업 애니메이션 실행
            // 혹은 파티클 켜주기
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
            {
                pc.fsm.PopState();
                return;
            }
        }
    }
}