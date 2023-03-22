using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Status status = null;
    public Action skill;

    public Vector3 motionSpeed;

    void Start()
    {
        status = Util.GetORAddComponet<Status>(gameObject); // raping
    }

    void Update()
    {
        if (Input.GetKey(KeyManager.Instance.InputAction(KeyToAction.MoveFront)))
            Move(Vector3.forward - motionSpeed);
        if (Input.GetKey(KeyManager.Instance.InputAction(KeyToAction.MoveBack)))
            Move(Vector3.back - motionSpeed);
        if (Input.GetKey(KeyManager.Instance.InputAction(KeyToAction.MoveLeft)))
            Move(Vector3.left - motionSpeed);
        if (Input.GetKey(KeyManager.Instance.InputAction(KeyToAction.MoveRight)))
            Move(Vector3.right - motionSpeed);

        if (Input.GetKeyDown(KeyCode.Q))
            skill?.Invoke();
    }

    void Move(Vector3 direction)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 2 * Time.deltaTime);
        transform.position += direction * status.speed * Time.deltaTime;
    }

}