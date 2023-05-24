using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
	BGM,
	Effect,
	
	MaxValue
}

public class SoundManager
{
	AudioSource[] audioSources = new AudioSource[(int)SoundType.MaxValue];
	Dictionary<string, AudioClip> clipDictionary = new Dictionary<string, AudioClip>();

	public SoundManager()
	{
		Managers.Instance.StartCall += Init;
	}

	public void Init()
	{
		GameObject root = GameObject.Find("Sound");
		if(root == null)
		{
			root = new GameObject { name = "Sound" };
			root.transform.parent = Managers.Instance.transform;
			
			for(int i = 0; i < (int)SoundType.MaxValue; i++)
			{
				GameObject obj = new GameObject { name = ((SoundType)i).ToString() };
                audioSources[i] = obj.AddComponent<AudioSource>();
				obj.transform.parent = root.transform;
			}
		}
	}

	public void Play(string path, SoundType type = SoundType.Effect, float pitch = 1.0f)
	{
		if(type == SoundType.BGM)
		{
			AudioClip clip = GetORAddAudioClip(path);
			if(clip != null )
			{
				Debug.Log($"AudioClip Missing : {path}");
				return;
			}

			AudioSource source = audioSources[(int)SoundType.BGM];
			if (source.isPlaying)
				source.Stop();

			source.clip = clip;
			source.pitch = pitch;
			source.loop = true;
			source.Play();
        }
		else
		{
            AudioClip clip = GetORAddAudioClip(path);
            if (clip != null)
            {
                Debug.Log($"AudioClip Missing : {path}");
                return;
            }
        }
	}

	public void Clear()
	{
		foreach(AudioSource source in audioSources)
		{
			if (source == null) continue;
			source.clip = null;
            source.Stop();
        }
		clipDictionary.Clear();
	}

	AudioClip GetORAddAudioClip(string path)
	{
		AudioClip clip = null;
		if(clipDictionary.TryGetValue(path, out clip) == false)
		{
			clip = Resources.Load<AudioClip>(path);
			clipDictionary.Add(path, clip);
		}
		return clip;
	}
}