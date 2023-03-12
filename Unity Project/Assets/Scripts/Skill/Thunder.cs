using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{
    public Transform Target;
    public LayerMask LayerMask;

    public float projectileSpeed;
    public int range;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Target.position, projectileSpeed);

        if (Input.GetButtonDown("Jump"))  //sapce bar로 실험
        {
            Attack();
        }
    }

    void Attack()
    {
        FindTarget();
        Destroy(gameObject, 2f);
    }

    void FindTarget()
    {
        Collider[] enemy = null;
        List<Collider> currentEnemys = new List<Collider>();

        enemy = Physics.OverlapSphere(transform.position, range, LayerMask);

        for(int i = 0; i < enemy.Length; i++)
        {
            //Instantiate(ThunderPrefab, enemy[i].transform.position + Vector3.up * 10, Quaternion.identity);  //1번째 인자는 오브젝트, 2번째 인자는 생성 위치, 3번째 인자는 회전

        }


    }
}
