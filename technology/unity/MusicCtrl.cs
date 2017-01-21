using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum Mic
{
	JieSuan1=0,
	JieSuan2=1,
	JieSuan3=2,
	JieSuan4=3,
	JieSuan5=4,
	JieSuan6=5,
	JieSuan7=6,
	JieSuan8=7,
	Lianxiao1=8,
	Lianxiao2=9,
	Lianxiao3=10,
	Lianxiao4=11,
	Lianxiao5=12,
	Lianxiao6=13,
	Lianxiao7=14,
	Raise1=15,
	Raise2=16,
	Raise3=17,
	Raise4=18,
	Duang =19,
	Zuigao= 20,
	chengjiuhuanhu=21,
	jieshu=22,
	button=23,
	danji=24


}

public class MusicCtrl : MonoBehaviour
{
	public static MusicCtrl Instance;
	public static float static_vol;
	private List<AudioSource> AudioSrc = new List<AudioSource> ();
	public AudioClip[] AudioCp;
	public AudioClip[] AudioBGM;
	
	void Awake ()
	{
		Instance = this;
		static_vol = PlayerPrefs.GetFloat ("volume", 1f);
		
	}


	private AudioSource BGMAS = null;
	public void PlayBGM (int id)
	{
		if (BGMAS == null)
			BGMAS = gameObject.AddComponent<AudioSource> ();
		else
			BGMAS.Stop ();
		BGMAS.playOnAwake = false;
		BGMAS.clip = AudioBGM [id];
		BGMAS.volume = static_vol;
		BGMAS.loop = true;
		BGMAS.Play ();
	}
	public void StopBGM ()
	{
		if (BGMAS != null)
			BGMAS.Stop ();
	}
	public void PlayShort (int id)
	{
		foreach (AudioSource AS in AudioSrc) {
			if (!AS.isPlaying) {
				AS.clip = AudioCp [id];
				AS.volume = static_vol;
				AS.loop = false;
				AS.Play ();
				return;
			}
		}
		AudioSource addoneAS = gameObject.AddComponent<AudioSource> ();
		addoneAS.playOnAwake = false;
		addoneAS.clip = AudioCp [id];
		addoneAS.volume = static_vol;
		addoneAS.loop = false;
		AudioSrc.Add (addoneAS);
	}
	public void Stop (int id)
	{
		foreach (AudioSource AS in AudioSrc) {
			if (AS.isPlaying && AS.clip == AudioCp [id]) {
				AS.Stop ();
			}
		}
	}
	
	void Btn_Sounds ()
	{
		if (static_vol == 0) {
			static_vol = 1f;
			PlayerPrefs.SetFloat ("volume", 1f);
		} else {
			static_vol = 0f;
			PlayerPrefs.SetFloat ("volume", 0);
		}
		
	}
}
