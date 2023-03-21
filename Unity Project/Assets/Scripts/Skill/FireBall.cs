using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Non Target
/// </summary>
public class FireBall : Skill
{
    FireProperty property = new FireProperty();

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * status.speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            return;

        property.damage = status.damage;
        property.ApplyEffect(other.gameObject);
        property.ApplyDebuff(other.gameObject);

        gameObject.SetActive(false);
    }
}
