using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// 현재 target에게 이동하는 알고리즘
/// </summary>
public class FindToMove : MonoBehaviour
{
    public GameObject defultTarget;
    [HideInInspector] public GameObject currentTarget;
    [HideInInspector] public int currentTargetPriority = -1;
    public float maxDistanceToTarget = 1f;
    [Space]
    [HideInInspector] public float speed = 1f;

    private void Awake()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.color = Color.gray;
    }

    private void Update()
    {
        MoveToTarget();
    }

    private void OnEnable()
    {
        currentTarget = defultTarget;
        currentTargetPriority = -1;
    }

    void MoveToTarget()
    {
        if (currentTarget != defultTarget)
        {
            float distance = (transform.position - currentTarget.transform.position).magnitude;
            if (distance > maxDistanceToTarget)
                currentTarget = defultTarget;
        }

        transform.LookAt(currentTarget.transform);
        transform.position += (currentTarget.transform.position - transform.position).normalized * speed * Time.deltaTime;
    }
}
