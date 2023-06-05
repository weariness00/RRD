using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster_UI : MonoBehaviour
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

        bar_UI.gameObject.SetActive(false);

        gameObject.GetComponent<Monster>().HitCall.AddListener(HitEvent);
    }

    private void OnDestroy()
    {
        if(barPositionCoroutine != null) StopCoroutine(barPositionCoroutine);
        Destroy(bar_UI.gameObject);
    }

    Coroutine barPositionCoroutine = null;
    IEnumerator Update_BarPosition()
    {
        while(true)
        {
            Vector3 pos = transform.position;
            pos.y = collider.bounds.max.y;
            pos = Camera.main.WorldToScreenPoint(pos);
            if (pos.z <= 0) bar_UI.gameObject.SetActive(false);
            else bar_UI.gameObject.SetActive(true);
            bar_UI.transform.position = pos;
            yield return null;
        }
    }

    public void HitEvent()
    {
        if(barPositionCoroutine == null) barPositionCoroutine = StartCoroutine(Update_BarPosition());
        bar_UI.size = Mathf.InverseLerp(0, status.maxHp.Cal(), status.hp.Cal());
        if (bar_UI.size.Equals(0))
        {
            Destroy(this);
        }
    }
}
