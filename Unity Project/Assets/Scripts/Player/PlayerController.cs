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
        //if (Input.GetKeyDown(KeyManager.Instance.InputAction(KeyToAction.MoveFront)))
        //    Debug.Log(KeyToAction.MoveFront.ToString());

        if (Input.GetKeyDown(KeyCode.Q))
            skill?.Invoke();
    }

    public void MoveFront()
    {
        Debug.Log("Character Is Move Front");
    }

    public void MoveBack()
    {
        Debug.Log("Character Is Move Back");
    }
    Rigidbody rigid; 
    public LayerMask LayerMask;  

    Vector3 moveVec;
    public float speed;


    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }


    bool key_Q;
    void KeyDown()
    {
        key_Q = Input.GetButtonDown("Skill1");
    }

    void Move()
    {
        moveVec = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;  //�̵��ε� ���� ������ ����°� ������?
        transform.position += moveVec * speed * Time.deltaTime;
    }

    void Turn()
    {
        transform.LookAt(transform.position + moveVec);
    }


    

    

}