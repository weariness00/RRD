using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    //�ʿ���µ� Ȥ�� ���� �ϴ� ���ܵ�
    public static InteractionManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Interaction(GameObject gameObject)
    {
        //RaycastHit[] hits;
        if (gameObject.tag == "NPC")
        {
            switch (gameObject.name)
            {
                
            }
        }
        else if (gameObject.tag == "Chset")
        {
            Managers.Key.InputAction(KeyToAction.Interact);

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
