using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : Skill
{
    public Transform target;
    

    public float projectileSpeed;
    public int range;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, projectileSpeed);

        if (Input.GetButtonDown("Jump"))  //sapce bar�� ����
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
        RaycastHit hit;
        Physics.SphereCast(transform.position, status.range, Vector3.up, out hit, 100f, layerMask);

        {

        }
    }

    public override void OnSkill()
    {
        if (!isOn)
            return;
        base.OnSkill();

    }
}
