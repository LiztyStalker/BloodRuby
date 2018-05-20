using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SoundFactoryClass : SingletonClass<SoundFactoryClass>
{
	Dictionary <string, AudioClip> m_bgmDic = new Dictionary<string, AudioClip>();
	Dictionary <string, AudioClip> m_effectDic = new Dictionary<string, AudioClip>();

	List<SoundPlayClass> m_soundList = new List<SoundPlayClass>();


//	AudioSource m_MyselfAudioSource;
//	AudioSource m_RangeAudioSource;
//	AudioSource m_WorldAuidoSource;

	readonly string[] m_btnKeys = 
	{
		"BtnNone",
		"BtnInfor",
		"BtnOK",
		"BtnCancel",
		"BtnWarning",
		"BtnError",
		"BtnSell",
		"BtnBuy"
	};


	public void initDictionary(){
		AudioClip[] bgmList = Resources.LoadAll<AudioClip> ("Sound/BGM");

		m_bgmDic.Clear ();
		foreach (AudioClip bgm in bgmList) {
			Debug.Log ("bgm : " + bgm.name + " " + bgm);
			m_bgmDic.Add (bgm.name, bgm);
		}


		AudioClip[] effectList = Resources.LoadAll<AudioClip>("Sound/Effect");

		m_effectDic.Clear ();
		foreach (AudioClip effect in effectList) {
			Debug.Log ("effect : " + effect.name + " " + effect);
			m_effectDic.Add (effect.name, effect);
		}

//		m_MyselfAudioSource = new AudioSource ();
//		m_RangeAudioSource;
//		m_WorldAuidoSource;

	}


	public void setMute(TYPE_SOUND typeSound){
		SoundPlayClass[] soundList = m_soundList.Where (sound => sound.typeSound == typeSound).ToArray<SoundPlayClass> ();
		foreach (SoundPlayClass sound in soundList) {
			if (typeSound == TYPE_SOUND.EFFECT)
				sound.setMute (PrepClass.isEffect);
			else
				sound.setMute (PrepClass.isBGM);
		}
	}


	/// <summary>
	/// 배경음 플레이
	/// </summary>
	/// <param name="soundCilp">Sound cilp.</param>
	/// <param name="key">Key.</param>
	public void bgmPlay(SoundPlayClass soundPlayer, string key, bool is3DSound = false){
		if (string.IsNullOrEmpty (key))
			return;
		
		if (m_bgmDic.ContainsKey (key)) {
			soundPlayer.audioPlay (m_bgmDic [key], TYPE_SOUND.BGM, is3DSound);
			m_soundList.Add (soundPlayer);
		}

//		soundCilp.audioPlay ();
	}

	/// <summary>
	/// 효과음 플레이
	/// </summary>
	/// <param name="soundPlayer">Sound cilp.</param>
	/// <param name="key">Key.</param>
	public void effectPlay(SoundPlayClass soundPlayer, string key, bool is3DSound = true){
		if (string.IsNullOrEmpty (key))
			return;
		
		if (m_effectDic.ContainsKey (key)) {
			soundPlayer.audioPlay (m_effectDic [key], TYPE_SOUND.EFFECT, is3DSound);
			m_soundList.Add (soundPlayer);
		} else {
			Debug.LogWarning ("사운드 없음 : " + key);
		}
	}

	/// <summary>
	/// 버튼 효과음 플레이
	/// </summary>
	/// <param name="soundPlayer">Sound player.</param>
	/// <param name="typeBtnSound">Type button sound.</param>
	public void effectPlay(SoundPlayClass soundPlayer, TYPE_BTN_SOUND typeBtnSound){
		effectPlay (soundPlayer, getBtnSoundKey (typeBtnSound), false);
	}


	string getBtnSoundKey(TYPE_BTN_SOUND typeBtnSound){		
		if (m_btnKeys.Length > 0 && m_btnKeys.Length > (int)typeBtnSound)
			return m_btnKeys [(int)typeBtnSound];
		return "";
	}
	/// <summary>
	/// 사운드 종료
	/// </summary>
	/// <param name="soundCilp">Sound cilp.</param>

	public void soundEnd(SoundPlayClass soundCilp){
		if(m_soundList.Contains(soundCilp)) m_soundList.Remove (soundCilp);
	}




}


