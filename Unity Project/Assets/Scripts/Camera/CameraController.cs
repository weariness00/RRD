using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	GameObject owner;
    public Vector3 offset;

    public GameObject ownerObject;
    public string ownerName;

    private void Start()
    {
        owner = GameObject.FindGameObjectWithTag(ownerName);
        if (ownerObject != null)
            owner = ownerObject;
    }

    private void LateUpdate()
    {
        Camera.main.transform.position = offset + owner.transform.position;
    }
}
