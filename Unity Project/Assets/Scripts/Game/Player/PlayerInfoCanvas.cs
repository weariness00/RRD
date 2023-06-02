using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class PlayerInfoCanvas : MonoBehaviour
{
    public Type CanvasType;

    public PlayerController player;
    Status status;

	public Slider hp_Slider;
    public TMP_Text hp_Value;

    public Slider mp_Slider;
    public TMP_Text mp_Value;

    public Slider exp_Slider;
    public TMP_Text exp_Value;
    public TMP_Text level_Value;

    public Skill_UINode Skill_NormalAttackNode;
    public Skill_UINode Skill_EnhanceAttackNode;
    public Skill_UINode Skill_AuxiliaryNode;
    public Skill_UINode Skill_UltimateNode;

    private void Start()
    {
        player = GameManager.Instance.Player;
        status = player.status;

        Skill_EnhanceAttackNode.icon.sprite = player.skill_EnhanceAttack?.icon;
        Skill_NormalAttackNode.icon.sprite = player.skill_Auxiliary?.icon;
        Skill_NormalAttackNode.icon.sprite = player.skill_Ultimate?.icon;
    }

    [HideInInspector] public Dictionary<int, Coroutine> skillCoolCoroutineDict = new();
    [HideInInspector] public Dictionary<int, bool> skillUIIsActive = new();
    private void LateUpdate()
    {
        if (player.skill_EnhanceAttack != null && Managers.Key.InputActionDown(KeyToAction.Skill_EhanceAttack) && !skillCoolCoroutineDict.ContainsKey(player.skill_EnhanceAttack.GetInstanceID()))
        {
            int idIndex = player.skill_EnhanceAttack.GetInstanceID();
            skillUIIsActive[idIndex] = true;
            skillCoolCoroutineDict[idIndex] = StartCoroutine(OnSkill_UINode(player.skill_EnhanceAttack, Skill_EnhanceAttackNode));
        }
        if (player.skill_Auxiliary != null && Managers.Key.InputActionDown(KeyToAction.Skill_Auxiliary) && !skillCoolCoroutineDict.ContainsKey(player.skill_Auxiliary.GetInstanceID()))
        {
            int idIndex = player.skill_Auxiliary.GetInstanceID();
            skillUIIsActive[idIndex] = true;
            skillCoolCoroutineDict[idIndex] = StartCoroutine(OnSkill_UINode(player.skill_Auxiliary, Skill_AuxiliaryNode));
        }
        if (player.skill_Ultimate != null && Managers.Key.InputActionDown(KeyToAction.Skill_Ultimate) && !skillCoolCoroutineDict.ContainsKey(player.skill_Ultimate.GetInstanceID()))
        {
            int idIndex = player.skill_Ultimate.GetInstanceID();
            skillUIIsActive[idIndex] = true;
            skillCoolCoroutineDict[idIndex] = StartCoroutine(OnSkill_UINode(player.skill_Ultimate, Skill_UltimateNode));
        }

        OnChangeStatusUI();
    }

    public void OnChangeStatusUI()
	{
        hp_Slider.value = Mathf.InverseLerp(0, status.maxHp.Cal(), status.hp.Cal());
        mp_Slider.value = Mathf.InverseLerp(0, status.maxMp.Cal(), status.mp.Cal());
        exp_Slider.value = Mathf.InverseLerp(0, status.need_Exp, status.experience);

        hp_Value.text = status.hp.Cal().ToString();
        mp_Value.text = status.mp.Cal().ToString();
        exp_Value.text = status.experience.ToString();
        level_Value.text = status.level.ToString();
    }

    IEnumerator OnSkill_UINode(Skill skill, Skill_UINode skill_UINode)
    {
        if (skill == null || skill.coolTime == 0) yield break;

        float startTime = Time.time;
        skill_UINode.coolTimeText.gameObject.SetActive(true);
        skill_UINode.CoolTimeFilled.gameObject.SetActive(true);
        while (true)
        {
            float coolTime = Time.time - startTime;
            if (skill.coolTime < coolTime) break;

            if (skillUIIsActive[skill.GetInstanceID()])
            {
                skill_UINode.coolTimeText.text = ((int)(skill.coolTime - coolTime)).ToString();
                skill_UINode.CoolTimeFilled.fillAmount = 1 - (coolTime / skill.coolTime);
            }
            yield return null;
        }

        if (skillUIIsActive[skill.GetInstanceID()])
        {
            skill_UINode.coolTimeText.gameObject.SetActive(false);
            skill_UINode.CoolTimeFilled.gameObject.SetActive(false);
        }

        skillCoolCoroutineDict.Remove(skill.GetInstanceID());
    }

    [System.Serializable]
    public enum Type
    {
        Status,
        Skill,
    }

    [System.Serializable]
    public class Skill_UINode
    {
        public Image icon;
        public TMP_Text nameText;
        public TMP_Text coolTimeText;
        public Image CoolTimeFilled;

        public void CoolUI_Active(bool value)
        {
            coolTimeText.gameObject.SetActive(value);
            CoolTimeFilled.gameObject.SetActive(value);
        }
    }
}
