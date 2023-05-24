using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Muryotaisu : PlayerController
{
    public Equipment equipment;

    private void Start()
    {
        CreateWeapon();
    }

    public void CreateWeapon()
    {
        int layer = (int)equipment.Equip(Instantiate(equipment.weapon));
        animator.SetInteger("Layer", layer);
        animator.SetLayerWeight(layer, 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster")
        {
            float damage = status.damage.Cal() + equipment.weapon.status.damage.Cal();
            //ItemClassList["StunGrenade"].GetComponent<StunGrenade>().ItemEffect(other);
            Managers.Damage.Attack(other.GetComponentInParent<Monster>(),damage);
        }
    }
}
