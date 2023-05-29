using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public float coolTime;
    public bool isOn = true;    // true : can use skill, false : can't use skill

    [HideInInspector] public Status status;

    protected PlayerController player;
    protected LayerMask layerMask;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        player = GameManager.Instance.Player;
        status = Util.GetORAddComponet<Status>(gameObject);
        layerMask = LayerMask.GetMask("Monster");

        coolTime = 1f;
    }

    public virtual void OnSkill()
    {
        Debug.Log($"Use This Skill {name}");
        StartCoroutine(WaitCoolTime());
    }

    IEnumerator WaitCoolTime()
    {
        isOn = false;
        yield return new WaitForSeconds(coolTime);
        isOn = true;
    }
}
