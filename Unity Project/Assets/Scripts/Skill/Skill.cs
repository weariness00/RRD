using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.UIElements.ToolbarMenu;
using static UnityEngine.ParticleSystem;

public class Skill : MonoBehaviour
{
    protected float coolTime;
    public bool isOn = true;    // true : can use skill, false : can't use skill

    protected Status status;

    [SerializeField] protected ParticleSystem skillEffect;
    [SerializeField] protected ParticleSystem targetEffect;

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

        Instantiate
    }

    /// <summary>
    /// Player OR Item etc..
    /// have Skill Socket,
    /// find Skill Socket and equip this skill,
    /// </summary>
    public void AddSkill<T>(T script)
    {

    }

    // ��ų ���� �� �޼��带 ȣ��
    public virtual void OnSkill()
    {
        Debug.Log($"Use This Skill {name}");
        StartCoroutine(WaitCoolTime());

        Util.Instantiate(skillEffect, gameObject.transform);
    }

    public virtual void FindTarget()  //������ �������� �� ���� ����� �� Ž��
    {
        
    }

    IEnumerator WaitCoolTime()
    {
        isOn = false;
        yield return new WaitForSeconds(coolTime);
        isOn = true;
    }
}
