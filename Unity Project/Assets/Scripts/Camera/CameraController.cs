using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	GameObject owner;
    public Vector3 offset;
    public string ownerName;

    private void Start()
    {
        owner = GameObject.FindGameObjectWithTag(ownerName);
    }

    private void LateUpdate()
    {
        Camera.main.transform.position = offset + owner.transform.position;
    }
}
