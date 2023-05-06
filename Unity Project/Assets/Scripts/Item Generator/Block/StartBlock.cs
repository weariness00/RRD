using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StartBlock : CodeBlock
{
	public List<GameObject> addBlockSpace = new List<GameObject>();

    protected override void Init()
    {
        base.Init();

        // �浹 �ڽ��� �ִ� panel�� �˻��� ��ȯ �Ͽ� list�� �߰�
        foreach (GameObject block in Util.GetChildren(gameObject))
        {
            if (block.tag != Enum.GetName(typeof(Define.CodeBlockType), Define.CodeBlockType.AddBlockSpace))
                continue;

            block.GetComponent<BoxCollider2D>().enabled = true;
            addBlockSpace.Add(block);
        }
    }

    private void Start()
    {
        Init();
    }
}