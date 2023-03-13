using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void Start()
    {
        Managers.Key.AddKeyAction(KeyManager.KeyToAction.MoveFront, MoveFront);
        Managers.Key.AddKeyAction(KeyManager.KeyToAction.MoveBack, MoveBack);
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
