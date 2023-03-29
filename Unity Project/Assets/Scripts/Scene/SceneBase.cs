using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneBase : MonoBehaviour
{
    private void Start()
    {
        
    }

    protected virtual void Init()
	{
		
	}

    public abstract void Clear()
    {

    }
}
