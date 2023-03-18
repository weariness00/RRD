using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    public Status status;

    public UnityEvent unityEvent;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            unityEvent?.Invoke();
        }
    }
}
