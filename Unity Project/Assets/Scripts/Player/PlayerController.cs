using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Status status = null;
    public Action skill;

    void Start()
    {
        status = Util.GetORAddComponet<Status>(gameObject); // raping
    }

    void Update()
    {
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
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 2 * Time.deltaTime);
        transform.position += direction * status.speed * Time.deltaTime;
    }

}