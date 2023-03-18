using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyManager.Instance.InputAction(KeyToAction.MoveFront)))
            Debug.Log(KeyToAction.MoveFront.ToString());
    }

    public void MoveFront()
    {
        Debug.Log("Character Is Move Front");
    }

    public void MoveBack()
    {
        Debug.Log("Character Is Move Back");
    }
}
