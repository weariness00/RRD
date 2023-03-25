using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Status status = null;
    public Action skill;

    public Vector3 motionSpeed; // 임시의

    Animator animator;

    void Start()
    {
        status = Util.GetORAddComponet<Status>(gameObject); // raping
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetFloat("Speed", 0);

        if (Input.GetKey(Managers.Key.InputAction(KeyToAction.MoveFront)))
            Move(Vector3.forward);
        if (Input.GetKey(Managers.Key.InputAction(KeyToAction.MoveBack)))
            Move(Vector3.back);
        if (Input.GetKey(Managers.Key.InputAction(KeyToAction.MoveLeft)))
            Move(Vector3.left);
        if (Input.GetKey(Managers.Key.InputAction(KeyToAction.MoveRight)))
            Move(Vector3.right);

        if (Input.GetKeyDown(KeyCode.Q))
            skill?.Invoke();
    }

    void Move(Vector3 direction)
    {
        animator.SetFloat("Speed", status.speed);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 2 * Time.deltaTime);
        transform.position += direction * status.speed * Time.deltaTime;
    }

    void ReSpawn()
    {
        GameManager.Instance.alivePlayerCount++;
    }

    void Dead()
    {
        int count = GameManager.Instance.alivePlayerCount--;
        if (count <= 0)
            GameManager.Instance.GameOver();
    }
}