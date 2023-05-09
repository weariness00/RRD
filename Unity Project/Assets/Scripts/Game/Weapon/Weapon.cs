using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public ItemData itemData;
    [HideInInspector] public Status status;

    public Define.WeaponType type;

    private void Start()
    {
        status = Util.GetORAddComponet<Status>(gameObject);
    }
}