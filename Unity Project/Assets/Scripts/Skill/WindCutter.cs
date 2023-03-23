using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WindCutter : Skill
{
	WindProperty property;

    public GameObject cutterObject;
    public int cutterCount = 1;
    public float spawnDuration = 2f;

    public override void OnSkill()
    {
        if (!isOn)
            return;
        base.OnSkill();

        for (int i = 0; i < cutterCount; i++)
        {
            StartCoroutine(waitCutter(i * 2f));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            return;

        property = Util.GetORAddComponet<WindProperty>(gameObject);
        property.damage = status.damage;
        property.ApplyEffect(other.gameObject);
        property.ApplyDebuff(other.gameObject);
    }

    IEnumerator waitCutter(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        GameObject obj = Util.Instantiate(cutterObject);
        obj.SetActive(true);

        Skill cutterSkill = Util.GetORAddComponet<Skill>(obj);
        cutterSkill.status = Util.GetORAddComponet<Status>(obj);
        cutterSkill.isGuide = false;
        cutterSkill.status = status;
        cutterSkill.OnSkill();

        Destroy(obj, 5f);
    }
}
