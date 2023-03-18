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

    public override void FindTarget()  //적에게 도달했을 때 가장 가까운 적 탐색
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
            if (Vector3.SqrMagnitude(item.transform.position - transform.position) > 2f)  //같은 적에게 계속 연쇄되는 것을 방지하기 위한 거리(후에 정확한 거리로 수정 필요)
            {
                currentEnemys.Add(item);
            }
        }

        currentEnemys[0].gameObject.GetComponent<Renderer>().material.color = Color.red;  //타격이 들어갔는지 확인코드 추후 몬스터 스크립트에서 데미지를 입을 경우로 이동

        Target = currentEnemys[0].transform;
        Debug.Log(chainCount);
    }

    void Chain()
    {

    }
}