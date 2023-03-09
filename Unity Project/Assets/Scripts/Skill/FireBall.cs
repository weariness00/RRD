using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : FireProperty
{
    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            return;

        ApplyEffect(other.gameObject);
        Debug.Log("Ãæµ¹");

        gameObject.SetActive(false);
    }
}
