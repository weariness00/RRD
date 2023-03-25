using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChillProperty : ElementalProperty
{

    private void Start()
    {
    }

    public override void ApplyEffect(GameObject _Object)
    {
        unityEvent?.Invoke();
        StartCoroutine("ChillDuration");
    }

    public override void ReleaseEffect()
    {

    }

    IEnumerable ChillDuration(GameObject _Object)
    {
        // 플레이어의 동작 속도 감소
        _Object.GetComponent<PlayerController>().motionSpeed = new Vector3(0.1f, 0.1f, 0.1f);  //느려지는 강도 조절이 필요할 거 같은데
        yield return new WaitForSeconds(durationTime);
        // 복구
    }

    public override void ApplyDebuff(GameObject target)
    {
        
    }
}