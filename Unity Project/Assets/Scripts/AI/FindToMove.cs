using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// 현재 target에게 이동하는 알고리즘
/// </summary>
public class FindToMove : MonoBehaviour
{
    public GameObject defaultTarget;

    [HideInInspector] public GameObject currentTarget;
    [HideInInspector] public int currentTargetPriority = -1;
    public float maxDistanceToTarget = 1f;

    [Space]
    [HideInInspector] public Status status;

    private void Start()
    {
        defaultTarget = GameObject.FindGameObjectWithTag("Player");
        status = Util.GetORAddComponet<Status>(gameObject);

        currentTarget = defaultTarget;
    }

    private void Update()
    {
        if (currentTarget == null)
            return;

        MoveToTarget();
    }

    void MoveToTarget()
    {
        // 현재 타겟과의 거리
        float distance = (transform.position - currentTarget.transform.position).magnitude;

        // 사거리보다 작으면 움직임을 멈춘다
        if (distance < status.range)
            return;

        // 사거리보다 길면 기본 타겟으로 변경한다.
        if (distance > status.range + 10f)
            currentTarget = defaultTarget;

        Vector3 direction = (currentTarget.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 2 * Time.deltaTime);
        transform.position += direction * status.speed * Time.deltaTime;
    }
}
