using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpUI : MonoBehaviour
{
    PlayerController pc;
    Status status;

    Scrollbar bar;
    TMP_Text text;

    private void Start()
    {
        bar = Util.FindChild<Scrollbar>(gameObject);
        text = Util.FindChild<TMP_Text>(gameObject);

        // 이거 씬이나 그냥 Managers 쪽에서 수정이 되도록 해야할 듯
        pc = GameManager.Instance.Player;
        if (pc == null)
            pc = Util.FindChild<PlayerController>(GameObject.FindWithTag("Player"));
        status = Util.GetORAddComponet<Status>(pc.gameObject);

        // 이거 씬이나 그냥 Managers 쪽에서 수정이 되도록 해야할 듯
        //GameManager.Instance.SetDataCall.AddListener(LevelUp);
        pc.LevelUpCall.AddListener(LevelUp);

    }

    private void LateUpdate()
    {
        EarnExp();
    }

    public void EarnExp()
    {
        float needExp = status.level * 35f;
        bar.size = (status.experience / needExp);
    }

    public void LevelUp()
    {
        text.text = status.level.ToString();
    }

}
