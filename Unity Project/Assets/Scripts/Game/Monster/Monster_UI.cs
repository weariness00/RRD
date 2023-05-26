using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster_UI : UIUtil
{
	static public GameObject canvas = null;
    static public GameObject hp_Bar = null;

    Status status;
    Collider collider;
    Scrollbar bar_UI;

    private void Start()
    {
        if (canvas == null) canvas = GameObject.Find("Monster UI Canvas");
        if (hp_Bar == null) hp_Bar = Resources.Load<GameObject>("Prefabs/UI/HP_Bar");

        status = Util.GetORAddComponet<Status>(gameObject);

        collider = GetComponentInChildren<Collider>();
        bar_UI = Instantiate(hp_Bar, canvas.transform).GetComponent<Scrollbar>();
    }

    private void Update()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, collider.bounds.max.y, 0));
        if (pos.z <= 0) return;
        bar_UI.transform.position = pos;
    }

    private void LateUpdate()
    {
        bar_UI.size = Mathf.InverseLerp(0, status.maxHp.Cal(), status.hp.Cal());
    }
}
