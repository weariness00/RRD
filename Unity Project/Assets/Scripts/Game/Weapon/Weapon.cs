using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : Equipment
{
    public Status status;

    public UnityEvent unityEvent;

    private void Start()
    {
        equipmenetType = EquipmentType.Weapon;
    }
}
