using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    protected float coolTime;
    public bool isOn = true;    // true : can use skill, false : can't use skill

    protected Status status;

    protected PlayerController player;
    protected Transform playerTranform;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
        status = Util.GetORAddComponet<Status>(gameObject);

        playerTranform = player.transform;
        coolTime = 1f;

        // temporary
        player.skill -= OnSkill;
        player.skill += OnSkill;
    }

    /// <summary>
    /// Player OR Item etc..
    /// have Skill Socket,
    /// find Skill Socket and equip this skill,
    /// </summary>
    public void AddSkill<T>(T script)
    {

    }

    // 스킬 사용시 이 메서드를 호출
    public virtual void OnSkill()
    {
        if (!isOn)
            return;

        Debug.Log($"Use This Skill {name}");
        StartCoroutine(WaitCoolTime());
    }

    public virtual void FindTarget()  //적에게 도달했을 때 가장 가까운 적 탐색
    {
        
    }

    IEnumerator WaitCoolTime()
    {
        isOn = false;
        yield return new WaitForSeconds(coolTime);
        isOn = true;
    }
}
