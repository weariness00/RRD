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
    public bool? isGuide = null; // null : �ƹ��͵� ���� ����, true : Ÿ���� ������ ��ų, false : ��Ÿ�� ������ ��ų

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

        Debug.Log($"��ų �浹 {gameObject.name}");
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
