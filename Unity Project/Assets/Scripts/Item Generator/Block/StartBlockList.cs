using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBlockList : BlockList
{
    protected override void AddNodeInEditor(GameObject node)
    {
        GameObject obj = Instantiate(node, editor.content.transform);
        obj.name = node.name;

        Util.GetORAddComponet<StartBlock>(obj).editor = editor;
    }
}
