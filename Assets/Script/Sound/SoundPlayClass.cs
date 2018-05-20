using System;
using UnityEngine;


public enum TYPE_BTN_SOUND{NONE, INFOR, OK, CANCEL, WARNING, ERROR, SELL, BUY}
public enum TYPE_SOUND {BGM, EFFECT}

public class SoundPlayClass : MonoBehaviour
{
	[SerializeField] string m_key;
	[SerializeField] TYPE_SOUND m_typeSound = TYPE_SOUND.EFFECT;
	[SerializeField] bool m_isPlayOnAwake = true;
	[SerializeField] bool m_isLoop = false;
	[SerializeField] bool m_is3DSound = true;

	AudioSource m_audioSource;

	public TYPE_SOUND typeSound{ get { return m_typeSound; } }



	void Start(){
		if(m_isPlayOnAwake) audioPlay (m_key, m_typeSound, m_is3DSound);
	}


	public void audioPlay(AudioClip audioClip, TYPE_SOUND typeSound, bool is3DSound = true){
		
		m_audioSource = GetComponent<AudioSource> ();
		if (m_audioSource == null) {
			m_audioSource = gameObject.AddComponent<AudioSource> ();
		}
		
		m_typeSound = typeSound;

		if (m_audioSource != null) {
			m_audioSource.Stop ();

			if (typeSound == TYPE_SOUND.BGM) {
				m_audioSource.clip = audioClip;
				m_audioSource.mute = PrepClass.isBGM;
				m_audioSource.loop = true;
				m_audioSource.Play ();
			} else {
				if (is3DSound) {
					m_audioSource.spatialBlend = 0.9f;
					m_audioSource.spread = 360f;
				}
				m_audioSource.mute = PrepClass.isEffect;
				m_audioSource.loop = m_isLoop;
				m_audioSource.PlayOneShot (audioClip);
//				Destroy (gameObject, m_audioSource.clip.length);
			}

		}

	}

	public void audioPlay(TYPE_BTN_SOUND typeBtnSound){
		SoundFactoryClass.GetInstance.effectPlay (this, typeBtnSound);
	}

//	public void audioPlayOneShot(string key, TYPE_SOUND typeSound){
//		soundPlayOneShot (key, typeSound);
//	}

	public void audioPlay(string key, TYPE_SOUND typeSound, bool is3DSound = true){
		soundPlay (key, typeSound, is3DSound);
	}

	public void audioPlay(TYPE_SOUND typeSound, bool is3DSound = true){
		soundPlay (m_key, typeSound, is3DSound);
	}

	void soundPlay(string key, TYPE_SOUND typeSound, bool is3Dsound){
		if (typeSound == TYPE_SOUND.EFFECT) {
			SoundFactoryClass.GetInstance.effectPlay (this, key, is3Dsound);
		} else {
			SoundFactoryClass.GetInstance.bgmPlay (this, key, false);
		}
	}






	public void setMute(bool isMute){
		if (m_audioSource == null)
			m_audioSource = GetComponent<AudioSource> ();
		m_audioSource.mute = isMute;
	}

	void OnDisable(){
		SoundFactoryClass.GetInstance.soundEnd (this);
	}



}


