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
        //카메라 위치 받아오기
        skillPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        Debug.Log(skillPoint);

        Instantiate(targetEffect, skillPoint, Quaternion.identity);  //마우스 좌표에 스킬 이펙트 발동

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
        //이 부분 깔리 장판을 지우고 싶은건데 gameobject가 아니라 이펙트만 지우는게 맞는건가?
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
