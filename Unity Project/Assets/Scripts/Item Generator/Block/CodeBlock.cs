using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class CodeBlock : MonoBehaviour
{
    UIEventHandler handler;
    RectTransform rect;
    [HideInInspector] public ScrollRect editor;

    BoxCollider2D collider;

    GameObject addBlockSpace = null;

    private void Start()
    {
        Init();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("111");

        if (collision.gameObject.CompareTag(Enum.GetName(typeof(Define.CodeBlockType), Define.CodeBlockType.AddBlockSpace)))
        {
            Debug.Log("ads");
            addBlockSpace = collider.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        addBlockSpace = null;
    }

    public void DragNode(PointerEventData eventData)
    {
        Vector2 mousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rect.transform as RectTransform, eventData.position, null, out mousePos);
        rect.anchoredPosition += mousePos - eventData.delta;
    }

    public void PointerDownNode(PointerEventData eventData)
    {
        collider.enabled = true;

        ItemGenerator.Instance.editor.selectBlock = this;

        gameObject.transform.parent = editor.transform.root;

        Vector2 mousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rect.transform as RectTransform, eventData.position, null, out mousePos);
        rect.anchoredPosition += mousePos - eventData.delta;
    }

    public void PointerUpNode(PointerEventData eventData)
    {
        collider.enabled = false;

        gameObject.transform.parent = editor.content.transform;
        rect.anchorMin = rect.anchorMax = new Vector2(0.5f, 0.5f);

        if(addBlockSpace != null)
        {
            addBlockSpace.GetComponent<BoxCollider2D>().enabled = false;

            rect.anchoredPosition = addBlockSpace.GetComponent<RectTransform>().anchoredPosition;
        }
    }

    protected virtual void Init()
    {
        handler = Util.GetORAddComponet<UIEventHandler>(gameObject);
        rect = GetComponent<RectTransform>();
        rect.anchorMin = rect.anchorMax = new Vector2(0.5f, 0.5f);

        collider = GetComponent<BoxCollider2D>();

        handler.RemoveAllEvent();
        handler.OnDragCall.AddListener(DragNode);
        handler.OnPointerDownCall.AddListener(PointerDownNode);
        handler.OnPointerUpCall.AddListener(PointerUpNode);

        gameObject.tag = Enum.GetName(typeof(Define.CodeBlockType), Define.CodeBlockType.CodeBlock);
    }

}
