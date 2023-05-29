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

        if (Input.GetButtonDown("Jump"))  //sapce bar
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
        // 왜 배열이 아닌지
        RaycastHit[] hits;
        hits = Physics.SphereCastAll(transform.position, status.range.Cal(), Vector3.up, 100f, layerMask);

        foreach(var hit in hits)
        {
            Managers.Damage.Attack(hit.transform.gameObject, status.damage.Cal());
        }
    }

    public override void OnSkill()
    {
        if (!isOn)
            return;
        base.OnSkill();
    }
}
