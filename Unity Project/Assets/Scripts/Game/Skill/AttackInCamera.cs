using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

/// <summary>
/// if Enemy in Camera, get the damage
/// </summary>
public class AttackInCamera : Skill
{
    public VisualEffect Lazer;

    Vector3 boxSize = new Vector3(5, 5, 5); // 임시로 주는 값 Player의 화면 비율에 따라 다르게 해줘야함

    float size = 0.5f;

    public override void OnSkill()
    {
        if (!isOn) return;

        base.OnSkill();

        RaycastHit[] hits = Physics.BoxCastAll(player.transform.position + player.transform.forward, boxSize, player.transform.forward, player.transform.rotation, 1000f, LayerMask.GetMask("Monster"));

        foreach (RaycastHit hit in hits)
        {
            RaycastHit lineHit;
            Vector3[] directions = { // Player와 Monster의 방향
                (hit.collider.bounds.min - player.transform.position).normalized,
                (hit.collider.bounds.center - player.transform.position).normalized, 
                (hit.collider.bounds.max - player.transform.position).normalized };

            foreach (Vector3 direction in directions)
            {
                Physics.Raycast(player.transform.position, direction, out lineHit, 1000f);
                //Debug.DrawRay(playerTranform.position, direction * 10f, Color.red, 3f);

                if (!lineHit.colliderInstanceID.Equals(hit.colliderInstanceID))
                    continue;

                if (lineHit.transform.gameObject.layer == 7) // 현재 찾아낸 Monster의 앞에 장애물이 있는지 없는지 체크
                {
                    Managers.Damage.Attack(lineHit.transform.GetComponent<Monster>(), status.damage.Cal());

                    VisualEffect vfx = Instantiate(Lazer, player.transform.position + Vector3.up, Quaternion.identity);
                    ObjectEventHandle oeh = Util.GetORAddComponet<ObjectEventHandle>(vfx.gameObject);

                    oeh.UpdateEvent.AddListener((handle) => { VFX_Update(vfx, hit); });

                    vfx.transform.position += Vector3.forward;
                    vfx.transform.localScale = Vector3.one * size;
                    vfx.SetFloat("Duration", 1f);
                    Destroy(vfx.gameObject, 1f);
                    break;
                }
            }
        }
    }

    public void VFX_Update(VisualEffect vfx , RaycastHit hit_Tatget)
    {
        vfx.transform.LookAt(hit_Tatget.collider.bounds.center);
        vfx.SetFloat("Distance", Vector3.Distance(hit_Tatget.collider.bounds.center - vfx.transform.forward * size, vfx.transform.position) * size);
    }
}