using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueText : MonoBehaviour, IPointerDownHandler
{
    public static DialogueText Instance;

    public Text dialogue;
    public GameObject nextDialogue;
    public CanvasGroup dialogueGroup;

    public Queue<string> sentences;
    [HideInInspector]public string currentSentence;

    public float typingSpeed;
    public bool isTyping;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        sentences = new Queue<string>();
    }

    private void Update()
    {
        if(dialogue.text == currentSentence)  //dialogue.text.Equals(currentSentence)�� ���� ������ ����?
        {
            nextDialogue.SetActive(true);            
            isTyping = false;
        }
    }

    public void Ondialogue(string[] lines)
    {
        sentences.Clear();
        foreach(string line in lines)
        {
            sentences.Enqueue(line); 
        }
        dialogueGroup.alpha = 1;
        dialogueGroup.blocksRaycasts = true;  //true�϶��� ���콺 �̺�Ʈ�� ����
        NextSentence();
    }

    public void NextSentence()
    {
        if(sentences.Count != 0)
        {
            currentSentence = sentences.Dequeue();
            isTyping = true;
            nextDialogue.SetActive(false);
            StartCoroutine(Typing(currentSentence));
        }

        else
        {
            dialogueGroup.alpha = 0;
            dialogueGroup.blocksRaycasts = false;  
        }
    }

    IEnumerator Typing(string line)
    {
        dialogue.text = "";
        foreach(char letter in line.ToCharArray())
        {
            dialogue.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            //�÷��̾ ������ �ٰ����� ���
            transform.LookAt(other.transform);
        }
    }

    //�� ��ũ��Ʈ�� ������ �ִ� ������Ʈ�� Ŭ������ �� �̺�Ʈ �߻�
    //�� �κ��� �ٰ����� �� �Ӹ����� �ؽ�Ʈ�� ǥ���ϸ� ��ǳ������ ����
    public void OnPointerDown(PointerEventData eventData)
    {
        if(!isTyping)
            NextSentence();
    }
}
