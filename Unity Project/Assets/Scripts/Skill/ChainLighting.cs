using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLighting : Skill
{
    public Transform target;
    public LayerMask layerMask;

    public float chainCount;
    public float projectileSpeed;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, projectileSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        FindTarget();
        chainCount--;
    }

    public override void FindTarget()  
    {
        Collider[] enemy = null;
        List<Collider> currentEnemys = new List<Collider>();

        enemy = Physics.OverlapSphere(transform.position, status.range, layerMask);

        if (enemy == null || chainCount <= 0)
        {
            gameObject.SetActive(false);
        }

        foreach (var item in enemy)
        {
            if (Vector3.SqrMagnitude(item.transform.position - transform.position) > 2f)  
            {
                currentEnemys.Add(item);
            }
        }

        currentEnemys[0].gameObject.GetComponent<Renderer>().material.color = Color.red; 

        target = currentEnemys[0].transform;
        Debug.Log(chainCount);
    }

    void Chain()
    {

    }
}