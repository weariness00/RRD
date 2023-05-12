using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectItemICon : MonoBehaviour, IPointerClickHandler
{
    public ItemGenerator generator;

    [Space]
    public ScrollRect iconListScroll;
    public Sprite[] iconList;

    private void Start()
    {
        generator.createCall += SelectIcon;

        Init();
    }

    void Init()
    {
        foreach(var icon in iconList)
        {
            GameObject obj = new GameObject();
            obj.transform.parent = iconListScroll.content.transform;

            Image newImage = obj.AddComponent<Image>();
            newImage.sprite = icon;
            newImage.name = icon.name;
            newImage.transform.localScale = Vector3.one;

            UIEventHandler uIEventHandler = Util.GetORAddComponet<UIEventHandler>(obj);
            EvenetHandleIcon(uIEventHandler);
        }
    }

    public void SelectIcon(ItemData itemData)
    {
        itemData.icon = selectIcon.sprite;
    }

    // Icon이 무엇이 있는지 보여주는 Scroll List 관련 함수
    public void OnPointerClick(PointerEventData eventData)
    {
        iconListScroll.gameObject.SetActive(!iconListScroll.gameObject.activeSelf);
    }

    #region ClickIcon 관련

    public Image selectIcon;
    void EvenetHandleIcon(UIEventHandler handler)
    {
        handler.OnPointerDownCall.AddListener(PointerDownIcon);
    }

    public void PointerDownIcon(PointerEventData eventData)
    {
        selectIcon.sprite = eventData.pointerCurrentRaycast.gameObject.GetComponent<Image>().sprite;
    }
    #endregion
}