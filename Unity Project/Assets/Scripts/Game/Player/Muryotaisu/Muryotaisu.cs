using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Muryotaisu : PlayerController
{
    public Equipment equipment;
    public AudioClip attack;

    private void Start()
    {
        CreateWeapon();
    }

    protected override void Update()
    {
        base.Update();          
    }

    public void CreateWeapon()
    {
        int layer = (int)equipment.Equip(Instantiate(equipment.weapon));
        animator.SetInteger("Layer", layer);
        animator.SetLayerWeight(layer, 1);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster")
        {
            float damage = status.damage.Cal() + equipment.weapon.status.damage.Cal();
            Managers.Damage.Attack(other.GetComponentInParent<Monster>(),damage);
        }
    }
}
