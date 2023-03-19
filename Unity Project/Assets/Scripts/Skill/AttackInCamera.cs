using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// if Enemy in Camera, get the damage
/// </summary>
public class AttackInCamera : Skill
{
    Vector3 boxSize = new Vector3(5, 5, 5); // �ӽ÷� �ִ� �� Player�� ȭ�� ������ ���� �ٸ��� �������

    public override void OnSkill()
    {
        base.OnSkill();
        RaycastHit[] hits = Physics.BoxCastAll(playerTranform.position, boxSize, playerTranform.forward, playerTranform.rotation, 1000f, LayerMask.GetMask("Monster"));

        foreach (RaycastHit hit in hits)
        {
            Vector3 direction = (hit.transform.position - playerTranform.position).normalized; // Player�� Monster�� ����
            RaycastHit lineHit;
            Physics.Raycast(playerTranform.position, direction, out lineHit, 1000f);
            //Debug.DrawRay(playerTranform.position, direction, Color.red, 3f);

            if (lineHit.transform.gameObject.layer == 7) // ���� ã�Ƴ� Monster�� �տ� ��ֹ��� �ִ��� ������ üũ
                Managers.Damage.Attack(lineHit.transform.gameObject, status);
        }
    }
}