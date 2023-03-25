using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

/// <summary>
/// FindToMove ��ũ��Ʈ�� ������ �ִ� ��ü�� Ž��
///
/// find Object to have FindToMove Script
/// </summary>
public class GiveInfoToObject : MonoBehaviour
{
    public int priority = 0;
    public float radius = 1f;

    public bool isMask = true;

    private void Update()
    {
        SearchFindToMove(isMask);
    }

    void SearchFindToMove(bool _IsMask)
    {
        Vector3 origin = transform.position;
        RaycastHit[] hits;
        if(_IsMask)
        {
            LayerMask layerMask = LayerMask.GetMask("Monster");
            hits = Physics.SphereCastAll(origin, radius, Vector3.up, 0f, layerMask);
        }
        else
            hits = Physics.SphereCastAll(origin, radius, Vector3.up, 0f);

        // �浹�� ��ü���� ���� ����.
        foreach (RaycastHit hit in hits)
        {
            FindToMove ftm = hit.collider.gameObject.GetComponent<FindToMove>();
            if (ftm == null)
                continue;
            if (ftm.currentTargetPriority > priority)
                continue;

            ftm.currentTarget = gameObject;
            ftm.currentTargetPriority = priority;
        }
    }
}