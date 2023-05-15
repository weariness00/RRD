using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLighting : Skill
{
    Transform target;

    public float chainCount;
    public float projectileSpeed;

    private void Update()
    {
        if(target != null)
            transform.position = Vector3.MoveTowards(transform.position, target.position, projectileSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        FindTarget();
        chainCount--;
    }

    public void FindTarget()  
    {
        Collider[] enemy = null;
        enemy = Physics.OverlapSphere(transform.position, status.range.Cal(), layerMask);

        if(target == null)
        {
            target = enemy[0].transform;
            enemy[0].gameObject.GetComponent<Renderer>().material.color = Color.red;
            return;
        }
        if (enemy == null || chainCount <= 0)
            Destroy(gameObject);

        List<Collider> currentEnemys = new List<Collider>();
        foreach (var item in enemy)
        {
            if (target.GetInstanceID().Equals(item.GetInstanceID()))
                continue;

            currentEnemys.Add(item);
        }

        currentEnemys[0].gameObject.GetComponent<Renderer>().material.color = Color.red; 

        target = currentEnemys[0].transform;
        Debug.Log(chainCount);
    }
}