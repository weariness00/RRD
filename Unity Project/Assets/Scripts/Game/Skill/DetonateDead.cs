using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetonateDead: Skill
{
    List<Monster> deadBody;
    GameObject target;
    Vector3 skillPos;

    int explosiveCount;

    private void Start()
    {
        FindDeadBody();
        StartCoroutine("SkillCoolTime");
        Explosive();
    }

    void FindDeadBody()
    {
        skillPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        RaycastHit[] hits;
        hits = Physics.SphereCastAll(skillPos, status.range.Cal(), Vector3.up, 1f, layerMask);
        foreach(var hit in hits)
        {
            Monster monster = hit.transform.GetComponent<Monster>();
            if (monster.status.isDead)
            {
                deadBody.Add(monster);
            }
        }
       
    }

    void Explosive()
    {
        //가장 가까운 시체를 explosiveCount 개수만큼 폭파
        for (int i = 0; i < explosiveCount; i++)
        {
            Instantiate(gameObject, deadBody[i].transform.position, Quaternion.identity);
            Destroy(deadBody[i].gameObject);
        }
    }

    IEnumerator SkillCoolTime()
    {
        transform.gameObject.SetActive(false);
        yield return new WaitForSeconds(coolTime);
        transform.gameObject.SetActive(true);
        yield break;
    }    
}
