using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Weapon : MonoBehaviour
{
    public ItemData itemData;
    [HideInInspector] public Status status;

    public Define.WeaponType type;

    public AudioClip attackSound;
    public VisualEffect effect = null;

    private void Start()
    {
        status = Util.GetORAddComponet<Status>(gameObject);
    }

    public void OnEffect()
    {
        effect?.Play();
    }
}