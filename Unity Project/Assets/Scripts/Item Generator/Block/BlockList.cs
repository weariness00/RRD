using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BlockList : MonoBehaviour
{
    [SerializeField] protected ScrollRect editor;

	ScrollRect scrollView;

    protected List<GameObject> Nodes = new List<GameObject>();

    private void Awake()
    {
        scrollView = GetComponent<ScrollRect>();

        // 클릭시 editor에 소환되도록 하기
        foreach (GameObject node in Util.GetChildren(scrollView.content.gameObject))
        {
            UIEventHandler handler = Util.GetORAddComponet<UIEventHandler>(node);
            handler.OnPointerClickCall.AddListener((eventData) => { AddNodeInEditor(node); });

            Nodes.Add(node);
        }
    }

    protected virtual void AddNodeInEditor(GameObject node)
    {
        GameObject obj = Instantiate(node, editor.content.transform);
        obj.name = node.name;

        Util.GetORAddComponet<AbilityBlock>(obj).editor = editor;
    }
}
