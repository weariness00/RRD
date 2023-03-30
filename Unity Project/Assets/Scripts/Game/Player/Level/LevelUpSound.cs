using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpSound : MonoBehaviour
{
	AudioSource audioSource;

    private void Start()
    {
        audioSource = Util.GetORAddComponet<AudioSource>(gameObject);
        audioSource.clip = Resources.Load("Sound/LevelUp") as AudioClip;
    }

    public void OnSound()
    {
        audioSource.Play();
    }
}
