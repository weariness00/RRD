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
    public GameObject currentTarget;
    [HideInInspector] public int currentTargetPriority = -1;
    public float maxDistanceToTarget = 1f;
    public float distance;

    [Space]
    [HideInInspector] public Status status;

    private void Start()
    {
        defaultTarget = GameObject.FindGameObjectWithTag("Player");
        status = Util.GetORAddComponet<Status>(gameObject);

        currentTarget = defaultTarget;
    }

    bool CheckDistance()
    {
        distance = (transform.position - currentTarget.transform.position).magnitude;
        // 사거리보다 작으면 움직임을 멈춘다
        if (distance < status.range.Cal()) return false;
        // 사거리보다 길면 기본 타겟으로 변경한다.
        if (distance > status.range.Cal() + 10f) currentTarget = defaultTarget;

        return true;
    }

    public void V3MoveToTarget()
    {
        if (!CheckDistance()) return;
        if (currentTarget == null) return;
        Vector3 direction = (currentTarget.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 2 * Time.deltaTime);
        transform.position += direction * status.speed.Cal() * Time.deltaTime;
    }

    public void V2MoveToTarget()
    {
        if (!CheckDistance()) return;
        if (currentTarget == null) return;
        Vector3 direction = (currentTarget.transform.position - transform.position).normalized;
        direction.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 2 * Time.deltaTime);
        transform.position += direction * status.speed.Cal() * Time.deltaTime;
    }
}
