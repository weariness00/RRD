using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFuntion : MonoBehaviour
{

	public void Play_Effect(AudioClip clip)
	{
		Managers.Sound.Play(clip, SoundType.Effect);
	}
	public void Play_BGM(AudioClip clip)
	{
        Managers.Sound.Play(clip, SoundType.BGM);
    }
}
