using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    //필요없는데 혹시 몰라서 일단 남겨둠
    public static InteractionManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void NPCInteraction(GameObject gameObject)
    {
        //RaycastHit[] hits;
        if (gameObject.tag == "NPC")
        {
            switch (gameObject.name)
            {
                
            }
        }
    }

    public void NPCInteraction(Camera camera)
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);

        Debug.Log(hit);
    }
}
