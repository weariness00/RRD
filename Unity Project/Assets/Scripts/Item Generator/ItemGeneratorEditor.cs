using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemGeneratorEditor : MonoBehaviour
{
    public CodeBlock selectBlock;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Delete))
        {
            if(selectBlock != null )
                Destroy( selectBlock.gameObject );
        }
    }
}
