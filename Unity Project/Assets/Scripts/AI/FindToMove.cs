using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// ���� target���� �̵��ϴ� �˰���
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
        // ���� Ÿ�ٰ��� �Ÿ�
        float distance = (transform.position - currentTarget.transform.position).magnitude;

        // ��Ÿ����� ������ �������� �����
        if (distance < status.range)
            return;

        // ��Ÿ����� ��� �⺻ Ÿ������ �����Ѵ�.
        if (distance > status.range + 10f)
            currentTarget = defaultTarget;

        Vector3 direction = (currentTarget.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 2 * Time.deltaTime);
        transform.position += direction * status.speed * Time.deltaTime;
    }
}
