using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public float coolTime = 0;
    public bool isOn = true;    // true : can use skill, false : can't use skill
    public Sprite icon;

    [HideInInspector] public Status status;

    protected PlayerController player;
    protected LayerMask layerMask;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        player = GameManager.Instance.Player;
        status = Util.GetORAddComponet<Status>(gameObject);
        layerMask = LayerMask.GetMask("Monster");
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
