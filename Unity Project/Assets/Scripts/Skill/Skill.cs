using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    protected float skillCooltime;
    public float damage;
    public float range;

    protected PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void FindTarget()  //적에게 도달했을 때 가장 가까운 적 탐색
    {
        
    }

    IEnumerator SkillTriger()
    {
        yield return new WaitForSeconds(skillCooltime);
    }
}
