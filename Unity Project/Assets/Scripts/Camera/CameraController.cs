using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public enum ViewType
{
    Friest_Person_View,
    Third_Person_View,
    Quarter_View,
}

public class CameraController : MonoBehaviour
{
	GameObject owner;
    public ViewType type = ViewType.Third_Person_View;

    public Vector3 pivot_Offset;
    public Vector3 camera_offset;

    public GameObject ownerObject;
    public string ownerName;

    private void Start()
    {
        owner = GameObject.FindGameObjectWithTag(ownerName);
        if (ownerObject != null)
            owner = ownerObject;

        switch (type)
        {
            case ViewType.Friest_Person_View:
                break;
            case ViewType.Third_Person_View:
                Camera.main.transform.position = camera_offset;
                break;
            case ViewType.Quarter_View:
                break;
            default:
                break;
        }
    }

    private void LateUpdate()
    {
        switch (type)
        {
            case ViewType.Friest_Person_View:
                break;
            case ViewType.Third_Person_View:
                Third_Person_View();
                break;
            case ViewType.Quarter_View:
                Camera.main.transform.position = camera_offset + owner.transform.position;
                break;
            default:
                break;
        }
    }

    float xRotate, yRotate, xRotateMove, yRotateMove;
    float rotateSpeed = 500.0f;
    void MousRotate(Transform target)
    {
        xRotateMove = -Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed;
        yRotateMove = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed;

        yRotate = transform.eulerAngles.y + yRotateMove;
        //xRotate = transform.eulerAngles.x + xRotateMove; 
        xRotate = xRotate + xRotateMove;

        xRotate = Mathf.Clamp(xRotate, -90, 90); // 위, 아래 고정

        target.eulerAngles = new Vector3(xRotate, yRotate, 0);
    }

    void Third_Person_View()
    {
        Camera.main.transform.LookAt(transform.position);
        
        transform.position = pivot_Offset + owner.transform.position;
        MousRotate(transform);
        //transform.rotation = owner.transform.rotation;
    }
}
