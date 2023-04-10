using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityBlock : MonoBehaviour
{
    UIEventHandler handler;
    RectTransform rect;
    [HideInInspector] public ScrollRect editor;

    private void Start()
    {
        handler = Util.GetORAddComponet<UIEventHandler>(gameObject);
        rect = GetComponent<RectTransform>();
        rect.anchorMin = rect.anchorMax = new Vector2(0.5f, 0.5f);

        handler.RemoveAllEvent();
        handler.OnDragCall.AddListener(DragNode);
        handler.OnPointerDownCall.AddListener(PointerDownNode);
        handler.OnPointerUpCall.AddListener(PointerUpNode);
    }


    public void DragNode(PointerEventData eventData)
    {
        RectTransform editorRect = editor.GetComponent<RectTransform>();
        rect.anchoredPosition = eventData.position - editorRect.sizeDelta / 2;
    }

    public void PointerDownNode(PointerEventData eventData)
    {
        gameObject.transform.parent = editor.transform.root;

        RectTransform editorRect = editor.GetComponent<RectTransform>();
        rect.anchoredPosition = eventData.position - editorRect.sizeDelta / 2;
    }

    public void PointerUpNode(PointerEventData eventData)
    {
        gameObject.transform.parent = editor.content.transform;
        rect.anchorMin = rect.anchorMax = new Vector2(0.5f, 0.5f);
    }
}
