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
        if(dialogue.text == currentSentence)  //dialogue.text.Equals(currentSentence)로 쓰는 이유가 뭐임?
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
        dialogueGroup.blocksRaycasts = true;  //true일때만 마우스 이벤트를 감지
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
            //플레이어가 가까이 다가가면 대사
            transform.LookAt(other.transform);
        }
    }

    //이 스크립트를 가지고 있는 오브젝트를 클릭했을 때 이벤트 발생
    //이 부분을 다가갔을 때 머리위에 텍스트를 표시하면 말풍선으로 변함
    public void OnPointerDown(PointerEventData eventData)
    {
        if(!isTyping)
            NextSentence();
    }
}
