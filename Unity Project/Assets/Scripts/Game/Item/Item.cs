using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IItem
{
	public void IItemEffect();
}

public class Item : MonoBehaviour
{
	protected bool isGet = false; // 이 아이템을 다른 누군가가 습득 할 수 있는지

	// 잠시 위로 쏫았다가 떨어지는
	public IEnumerator InitRigidBody()
	{
		isGet = false;
        Rigidbody rigidbody = Util.GetORAddComponet<Rigidbody>(gameObject);
		rigidbody.useGravity = true;
		rigidbody.velocity = Vector3.up * 10.0f;
		rigidbody.angularVelocity = Random.insideUnitSphere;
		rigidbody.mass = 10.0f;

		yield return stdfx.TwoSecond;

		isGet = true;
		rigidbody.isKinematic = true;
        rigidbody.velocity = Vector3.down * 10.0f;
    }
}
