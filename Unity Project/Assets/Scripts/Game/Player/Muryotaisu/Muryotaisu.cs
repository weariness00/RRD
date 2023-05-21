using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Muryotaisu : PlayerController
{
    public Equipment equipment;
    public void CreateWeapon()
    {
        int layer = (int)equipment.Equip(Instantiate(equipment.weapon));
        animator.SetInteger("Layer", layer);
        animator.SetLayerWeight(layer, 1);
    }
}
