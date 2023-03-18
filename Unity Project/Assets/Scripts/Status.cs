using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public int hp;
    public int maxHp;
    public int mp;
    public int maxMp;
    [Space]

    public int damage;

    private void Start()
    {
        hp = maxHp; mp = maxMp;
    }
}