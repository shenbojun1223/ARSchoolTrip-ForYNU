using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _SoundManager : MonoBehaviour {
	public pair[] clipArray;
	
	private Dictionary<string,AudioClip> audioDict=new Dictionary<string,AudioClip>();
	// public player_music _player_music;

	[System.Serializable]
	public struct pair
	{
		public string name;
		public AudioClip clip;
	}

	// public struct player_music
	// {
	// 	public AudioSource player;
	// 	public Dictionary<string,AudioClip>  audioDict;
	// }

	void Awake() {
		for (int i = 0; i < clipArray.Length; i++)
        {
            if (!audioDict.ContainsKey(clipArray[i].name))
            {

                audioDict.Add(clipArray[i].name, clipArray[i].clip);
            }
        }
	}

	public void PlaySound(string name)
	{
		GetComponent<AudioSource>().clip=audioDict[name];
		GetComponent<AudioSource>().Play();
	}

		public void PlaySound(string name,int second)
	{
		GetComponent<AudioSource>().clip=audioDict[name];
		GetComponent<AudioSource>().Play((ulong)second);
	}
}
