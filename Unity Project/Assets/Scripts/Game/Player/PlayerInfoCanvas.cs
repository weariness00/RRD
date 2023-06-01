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

    Coroutine[] skillCool = new Coroutine[3];
    private void LateUpdate()
    {
        //if (Managers.Key.InputActionDown(KeyToAction.Skill_NormalAttack))
        //    StartCoroutine(OnSkill_UINode(player.skill_NormalAttack, Skill_NormalAttackNode));
        if (Managers.Key.InputActionDown(KeyToAction.Skill_EhanceAttack) && skillCool[0] == null)
            skillCool[0] = StartCoroutine(OnSkill_UINode(player.skill_EnhanceAttack, Skill_EnhanceAttackNode, 0));
        if (Managers.Key.InputActionDown(KeyToAction.Skill_Auxiliary) && skillCool[1] == null)
            skillCool[1] = StartCoroutine(OnSkill_UINode(player.skill_Auxiliary, Skill_AuxiliaryNode, 1));
        if (Managers.Key.InputActionDown(KeyToAction.Skill_Ultimate) && skillCool[2] == null)
            skillCool[2] = StartCoroutine(OnSkill_UINode(player.skill_Ultimate, Skill_UltimateNode, 2));

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

    IEnumerator OnSkill_UINode(Skill skill, Skill_UINode skill_UINode, int index)
    {
        if (skill == null || skill.coolTime == 0) yield break;

        float startTime = Time.time;
        skill_UINode.coolTimeText.gameObject.SetActive(true);
        skill_UINode.CoolTimeFilled.gameObject.SetActive(true);
        while (true)
        {
            float coolTime = Time.time - startTime;
            if (skill.coolTime < coolTime) break;

            skill_UINode.coolTimeText.text = ((int)(skill.coolTime - coolTime)).ToString();
            skill_UINode.CoolTimeFilled.fillAmount = coolTime / skill.coolTime;
            yield return null;
        }
        skill_UINode.coolTimeText.gameObject.SetActive(false);
        skill_UINode.CoolTimeFilled.gameObject.SetActive(false);

        skillCool[index] = null;
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
    }
}
