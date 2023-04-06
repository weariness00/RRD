using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// if Enemy in Camera, get the damage
/// </summary>
public class AttackInCamera : Skill
{
    Vector3 boxSize = new Vector3(5, 5, 5); // 임시로 주는 값 Player의 화면 비율에 따라 다르게 해줘야함

    public override void OnSkill()
    {
        if (!isOn)
            return;

        base.OnSkill();

        RaycastHit[] hits = Physics.BoxCastAll(playerTranform.position, boxSize, playerTranform.forward, playerTranform.rotation, 1000f, LayerMask.GetMask("Monster"));

        foreach (RaycastHit hit in hits)
        {
            RaycastHit lineHit;
            Vector3[] directions = { // Player와 Monster의 방향
                (hit.collider.bounds.min - playerTranform.position).normalized,
                (hit.collider.bounds.center - playerTranform.position).normalized, 
                (hit.collider.bounds.max - playerTranform.position).normalized };

            foreach (Vector3 direction in directions)
            {
                Physics.Raycast(playerTranform.position, direction, out lineHit, 1000f);
                //Debug.DrawRay(playerTranform.position, direction * 10f, Color.red, 3f);

                if (!lineHit.colliderInstanceID.Equals(hit.colliderInstanceID))
                    continue;

                if (lineHit.transform.gameObject.layer == 7) // 현재 찾아낸 Monster의 앞에 장애물이 있는지 없는지 체크
                {
                    Managers.Damage.Attack(lineHit.transform.gameObject, status);
                    Util.Instantiate(targetEffect, lineHit.transform);
                    break;
                }
            }
        }
    }
}