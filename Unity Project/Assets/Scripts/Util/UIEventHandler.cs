using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIEventHandler : MonoBehaviour, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler, IDragHandler, IPointerExitHandler
{
    public UnityEvent<PointerEventData> OnDragCall = new UnityEvent<PointerEventData>();
    public UnityEvent<PointerEventData> OnPointerClickCall = new UnityEvent<PointerEventData>();
    public UnityEvent<PointerEventData> OnPointerDownCall = new UnityEvent<PointerEventData>();
    public UnityEvent<PointerEventData> OnPointerExitCall = new UnityEvent<PointerEventData>();
    public UnityEvent<PointerEventData> OnPointerUpCall = new UnityEvent<PointerEventData>();

    public void OnDrag(PointerEventData eventData)
    {
        OnDragCall?.Invoke(eventData);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnPointerClickCall?.Invoke(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDownCall?.Invoke(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnPointerExitCall?.Invoke(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnPointerUpCall?.Invoke(eventData);
    }
}
