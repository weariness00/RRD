using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectEventHandle : MonoBehaviour
{
    public Dictionary<string, Component> componets = new Dictionary<string, Component>();

    public UnityEvent<ObjectEventHandle> AwakeEvent = new UnityEvent<ObjectEventHandle>();
    public UnityEvent<ObjectEventHandle> StratEvent = new UnityEvent<ObjectEventHandle>();
    public UnityEvent<ObjectEventHandle> UpdateEvent = new UnityEvent<ObjectEventHandle>();
    public UnityEvent<Collision, ObjectEventHandle> OnCollisionEnterEvent = new UnityEvent<Collision, ObjectEventHandle>();
    public UnityEvent<Collider, ObjectEventHandle> OnTriggerEnterEvent = new UnityEvent<Collider, ObjectEventHandle>();

    private void Awake()
    {
        AwakeEvent?.Invoke(this);
    }

    private void Start()
    {
        StratEvent?.Invoke(this);
    }

    private void Update()
    {
        UpdateEvent?.Invoke(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnCollisionEnterEvent?.Invoke(collision, this);
    }

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEnterEvent?.Invoke(other, this);
    }
}
