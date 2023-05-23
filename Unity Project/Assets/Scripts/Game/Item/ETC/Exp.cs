using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : Item
{
    GameObject target;

    float value = 10.0f;

    private void Start()
    {
        target = GameManager.Instance.Player.gameObject;
    }

    private void Update()
    {
        if (!isGet) return;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * 5.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player") return;

        Status status = collision.gameObject.GetComponent<Status>();
        status.experience += value;
        status.LevelUP();

        Debug.Log("°æÇèÄ¡ ½Àµæ : " + value);
        Destroy(gameObject);
    }
}