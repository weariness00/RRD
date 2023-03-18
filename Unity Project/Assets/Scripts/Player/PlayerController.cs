using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyManager.Instance.InputAction(KeyToAction.MoveFront)))
            Debug.Log(KeyToAction.MoveFront.ToString());
    }

    public void MoveFront()
    {
        Debug.Log("Character Is Move Front");
    }

    public void MoveBack()
    {
        Debug.Log("Character Is Move Back");
    }
    Rigidbody rigid; //�̰� �̸� ��ĳ�ؾ����� �𸣰���
    public LayerMask LayerMask;  //���� ����, enemy == 6��

    Vector3 moveVec;
    public float speed;


    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
        Turn();

        if (key_Q)
        {
        }
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