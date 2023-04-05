using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public string[] sentence;
    public bool isInteraction;

    private void Update()
    {
        if (isInteraction && Input.GetKeyDown(KeyCode.E))
        {
            if (DialogueText.Instance.dialogueGroup.alpha == 0)
                DialogueText.Instance.Ondialogue(sentence);
        }
    }

    private void OnMouseDown()
    {
        if (isInteraction)
        {
            if (DialogueText.Instance.dialogueGroup.alpha == 0)
                DialogueText.Instance.Ondialogue(sentence);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            transform.LookAt(other.transform);
            isInteraction = true;
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
