using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rigid; //이거 이름 어캐해야할지 모르겠음
    public LayerMask LayerMask;  //적만 감지, enemy == 6번

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
        moveVec = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;  //이동인데 따로 변수를 만드는게 좋을까?
        transform.position += moveVec * speed * Time.deltaTime;
    }

    void Turn()
    {
        transform.LookAt(transform.position + moveVec);
    }


    

    

}