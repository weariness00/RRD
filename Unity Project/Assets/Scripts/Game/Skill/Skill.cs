using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.UIElements.ToolbarMenu;
using static UnityEngine.ParticleSystem;

public class Skill : MonoBehaviour
{
    public float coolTime;
    public bool isOn = true;    // true : can use skill, false : can't use skill
    public bool? isGuide = null; // null : 아무것도 실행 안함, true : 타켓팅 형식의 스킬, false : 논타겟 형식의 스킬

    [HideInInspector] public Status status;

    [SerializeField] protected ParticleSystem skillEffect;
    [SerializeField] protected ParticleSystem targetEffect;

    protected PlayerController player;
    protected Transform playerTranform;
    protected LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
        status = Util.GetORAddComponet<Status>(gameObject);
        layerMask = LayerMask.GetMask("Monster");

        playerTranform = player.transform;
        coolTime = 1f;
    }

    private void Update()
    {
        if (isGuide == true)
        {

        }
        else if(isGuide == false)
        {
            gameObject.transform.Translate(Vector3.forward * status.speed.Cal() * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            return;

        Debug.Log($"스킬 충돌 {gameObject.name}");
        Managers.Damage.Attack(other.gameObject, status);
        Util.Instantiate(targetEffect, other.gameObject.transform);
    }

    /// <summary>
    /// Player OR Item etc..
    /// have Skill Socket,
    /// find Skill Socket and equip this skill,
    /// </summary>
    public void AddSkill<T>(T script)
    {

    }

    public virtual void OnSkill()
    {
        Debug.Log($"Use This Skill {name}");
        StartCoroutine(WaitCoolTime());

        Util.Instantiate(skillEffect, gameObject.transform);
    }

    IEnumerator WaitCoolTime()
    {
        isOn = false;
        yield return new WaitForSeconds(coolTime);
        isOn = true;
    }
}
