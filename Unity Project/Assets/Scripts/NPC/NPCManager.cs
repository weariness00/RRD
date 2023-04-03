using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public string[] sentence;

    private void OnMouseDown()
    {
        if(DialogueText.Instance.dialogueGroup.alpha == 0)
            DialogueText.Instance.Ondialogue(sentence);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            transform.LookAt(other.transform);
        }
    }
}
