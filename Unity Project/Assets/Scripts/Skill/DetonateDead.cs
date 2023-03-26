using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetonateDead: Skill
{
    Monster deadBody;
    GameObject target;
    Vector3 skillPos;

    private void Start()
    {
        deadBody = Util.GetORAddComponet<Monster>(gameObject);
        FindDeadBody();
        StartCoroutine("SkillCoolTime");
        Explosive();
    }

    void FindDeadBody()
    {
        skillPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        RaycastHit hit;
        Physics.SphereCast(skillPos, status.range, Vector3.up, out hit, 1f, layerMask);
        deadBody.transform.position = hit.transform.position;
        
    }

    void Explosive()
    {
        Instantiate(gameObject, deadBody.transform.position, Quaternion.identity);
        Destroy(deadBody.gameObject);
    }

    IEnumerator SkillCoolTime()
    {
        transform.gameObject.SetActive(false);
        yield return new WaitForSeconds(coolTime);
        transform.gameObject.SetActive(true);
        yield break;
    }    
}
