using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public Transform target;

    public float chainCount;
    public int damage;
    public int range;
    public float projectileSpeed;

    private void Awake()
    {
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, projectileSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        FindNextTarget();
        chainCount--;
    }

    private void FindNextTarget()
    {
        Collider[] enemy = null;
        List<Collider> currentEnemys = new List<Collider>();

        enemy = Physics.OverlapSphere(transform.position, range);

        if (enemy == null || chainCount <= 0)
        {
            gameObject.SetActive(false);
        }

        foreach (var item in enemy)
        {
            if (item.tag == "Enemy")
            {
                currentEnemys.Add(item);
            }
        }

        currentEnemys[0].gameObject.GetComponent<Renderer>().material.color = Color.red;

        target = currentEnemys[0].transform;
        Debug.Log("체인 라이트닝");
    }
}
