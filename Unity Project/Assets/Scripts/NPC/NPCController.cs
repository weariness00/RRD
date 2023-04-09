using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public string[] sentence;
    public bool isInteraction;

    private void Update()
    {
        if (isInteraction && Input.GetKeyDown(KeyCode.E))
        {
            if (DialogueManager.Instance.dialogueGroup.alpha == 0)
                DialogueManager.Instance.Ondialogue(sentence);
        }
    }

    private void OnMouseDown()
    {
        if (isInteraction)
        {
            if (DialogueManager.Instance.dialogueGroup.alpha == 0)
                DialogueManager.Instance.Ondialogue(sentence);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            transform.LookAt(other.transform);
            isInteraction = true;
            Debug.Log("상호작용 가능");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInteraction = false;
        }
    }
}
