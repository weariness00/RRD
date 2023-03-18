using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLighting : Skill
{
    public Transform Target;
    public LayerMask LayerMask;
    public GameObject Player;

    public float chainCount;
    public float projectileSpeed;

    private void Awake()
    {
        FindTarget();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Target.position, projectileSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        FindTarget();
        chainCount--;
    }

    public override void FindTarget()  //������ �������� �� ���� ����� �� Ž��
    {
        Collider[] enemy = null;
        List<Collider> currentEnemys = new List<Collider>();

        enemy = Physics.OverlapSphere(transform.position, range, LayerMask);

        if (enemy == null || chainCount <= 0)
        {
            gameObject.SetActive(false);
        }

        foreach (var item in enemy)
        {
            if (Vector3.SqrMagnitude(item.transform.position - transform.position) > 2f)  //���� ������ ��� ����Ǵ� ���� �����ϱ� ���� �Ÿ�(�Ŀ� ��Ȯ�� �Ÿ��� ���� �ʿ�)
            {
                currentEnemys.Add(item);
            }
        }

        currentEnemys[0].gameObject.GetComponent<Renderer>().material.color = Color.red;  //Ÿ���� ������ Ȯ���ڵ� ���� ���� ��ũ��Ʈ���� �������� ���� ���� �̵�

        Target = currentEnemys[0].transform;
        Debug.Log(chainCount);
    }

    void Chain()
    {

    }
}