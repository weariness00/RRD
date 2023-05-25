using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public enum SoundType
{
	BGM,
	Effect,
	
	MaxValue
}

public class SoundManager : UIUtil
{
	public GameObject Sound_UI;
	AudioSource[] audioSources = new AudioSource[(int)SoundType.MaxValue];
	Dictionary<string, AudioClip> clipDictionary = new Dictionary<string, AudioClip>();

	string defualt_Path = "Sound/";

	public SoundManager()
	{
		Managers.Instance.StartCall += Init;
		Managers.Instance.UpdateCall += Update;

		UIInit();
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
    void UIInit()
    {
        Sound_UI = Util.FindChild(Managers.Instance.gameObject, "Sound UI Canvas");

        //Bind<Button>(Sound_UI, new string[] { "Back Button" });
        foreach (var soundTypeName in Enum.GetNames(typeof(SoundType)))
            Bind<Slider>(Sound_UI, new string[] { $"{soundTypeName} Slider" });

    }

	void Update()
	{
        for (int i = 0; i < (int)SoundType.MaxValue; i++)
			audioSources[i].volume = Get<Slider>(i).value;
    }

    public void Play(string path, SoundType type = SoundType.Effect, float pitch = 1.0f)
	{
		if(!path.Contains("/"))
			path = defualt_Path + path;

		AudioSource source;
		AudioClip clip = GetORAddAudioClip(path);
        if (clip == null)
        {
            Debug.LogWarning($"AudioClip Missing : {path}");
            return;
        }
		Play(clip, type, pitch);
    }

    public void Play(AudioClip clip, SoundType type = SoundType.Effect, float pitch = 1.0f)
	{
		if(clip == null)
		{
			Debug.LogWarning("���� Clip�� �������� �ʽ��ϴ�.");
			return;
		}

        AudioSource source;
        if (type == SoundType.BGM)
        {
            source = audioSources[(int)SoundType.BGM];
            if (source.isPlaying)
                source.Stop();

            source.clip = clip;
            source.pitch = pitch;
            source.loop = true;

            source.Play();
        }
        else
        {
            source = audioSources[(int)SoundType.Effect];
            source.pitch = pitch;
            source.PlayOneShot(clip);
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