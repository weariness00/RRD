using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLighting : Skill
{
    Transform target;

    public float chainCount;
    public float projectileSpeed;

    private void Start()
    {
        FindTarget();
    }

    private void Update()
    {
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
        List<Collider> currentEnemys = new List<Collider>();

        enemy = Physics.OverlapSphere(transform.position, status.range, layerMask);

        if (enemy == null || chainCount <= 0)
            Destroy(gameObject);

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