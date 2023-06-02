using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public enum ViewType
{
    Friest_Person_View,
    Third_Person_View,
    Quarter_View,
}

public class CameraController : MonoBehaviour
{
    public Camera camera;
	public GameObject owner;
    public GameObject ownerLookTarget;
    Collider ownerCollider;
    public ViewType type = ViewType.Third_Person_View;

    public Vector3 camera_offset;

    private void Start()
    {
        ownerCollider = owner.GetComponent<Collider>();
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
        if (Cursor.visible == true) return;

        switch (type)
        {
            case ViewType.Friest_Person_View:
                break;
            case ViewType.Third_Person_View:
                Third_Person_View();
                break;
            case ViewType.Quarter_View:
                camera.transform.position = camera_offset + owner.transform.position;
                break;
            default:
                break;
        }


    }

    float xRotate, yRotate, xRotateMove, yRotateMove;
    float rotateSpeed = 500.0f;
    Vector3 MousRotate(Vector3 angles)
    {
        xRotateMove = -Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed;
        yRotateMove = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed;

        yRotate = angles.y + yRotateMove;
        //xRotate = transform.eulerAngles.x + xRotateMove; 
        xRotate = xRotate + xRotateMove;

        xRotate = Mathf.Clamp(xRotate, -90, 90); // 위, 아래 고정

        return angles = new Vector3(xRotate, yRotate, 0);
    }

    void Third_Person_View()
    {
        camera.transform.LookAt(transform.position);

        transform.position = ownerCollider.bounds.center;
        transform.eulerAngles = MousRotate(transform.eulerAngles);
        ownerLookTarget.transform.eulerAngles = transform.eulerAngles;
        ownerLookTarget.transform.localPosition = new Vector3(owner.transform.position.x, ownerCollider.bounds.max.y, owner.transform.position.z) + ownerLookTarget.transform.forward;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.forward, out hit, camera_offset.magnitude, LayerMask.GetMask("Terrain")))
        {
            camera.transform.localPosition = hit.point - transform.position;
        }
        else
        {
            camera.transform.localPosition = camera_offset;
        }
    }
}
