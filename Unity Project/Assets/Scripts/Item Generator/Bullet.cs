using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector] public Status status;

    private void Awake()
    {
        status = Util.GetORAddComponet<Status>(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
    }

}
