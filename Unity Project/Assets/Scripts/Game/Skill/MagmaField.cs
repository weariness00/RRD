using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaField : Skill
{
    Vector3 skillPoint;

    public float ticCoolTime;
    public float duration;
    
    void Start()
    {
        //ī�޶� ��ġ �޾ƿ���
        skillPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        Debug.Log(skillPoint);

        Instantiate(targetEffect, skillPoint, Quaternion.identity);  //���콺 ��ǥ�� ��ų ����Ʈ �ߵ�

        StartCoroutine("SkillCoolTime");
    }

    void Update()
    {
        StartCoroutine("TicDamage");
    }

    IEnumerable TicDamage()
    {
        RaycastHit hit;
        Physics.SphereCast(skillPoint, status.range.Cal(), Vector3.zero, out hit, 1f, layerMask);

        Managers.Damage.Attack(hit.transform.gameObject, status.damage.Cal());
        yield return new WaitForSeconds(ticCoolTime);
    }

    IEnumerable SkillCoolTime()
    {
        transform.gameObject.SetActive(false);
        yield return new WaitForSeconds(coolTime);
        transform.gameObject.SetActive(true);
    }

    IEnumerable SkillDuration()
    {
        //�� �κ� �� ������ ����� �����ǵ� gameobject�� �ƴ϶� ����Ʈ�� ����°� �´°ǰ�?
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
