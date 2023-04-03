using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCsentence : MonoBehaviour
{
    public string[] sentence;

    private void OnMouseDown()
    {
        if(DialogueText.Instance.dialogueGroup.alpha == 0)
            DialogueText.Instance.Ondialogue(sentence);
    }
}
