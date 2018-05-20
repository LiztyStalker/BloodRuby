using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIOptionClass : UILobbyParentClass
{
//	[SerializeField] UIOptionCreatorClass m_creatorPanel;
	[SerializeField] Toggle m_bgmToggle;
	[SerializeField] Toggle m_effectToggle;


	void Start(){
//		PrepClass.isBGM = false;
//		PrepClass.isEffect = false;
		getToggle();
	}


	public void OnValueChangedBGM(){
		PrepClass.isBGM = !m_bgmToggle.isOn;
		setToggle ();
	}


	public void OnValueChangedEffect(){
		PrepClass.isEffect = !m_effectToggle.isOn;
		setToggle ();
	}



	void setToggle(){


		if (PrepClass.isBGM)
			PlayerPrefs.SetInt ("isBGM", 1);
		else
			PlayerPrefs.SetInt ("isBGM", 0);

		if (PrepClass.isEffect)
			PlayerPrefs.SetInt ("isEffect", 1);
		else
			PlayerPrefs.SetInt ("isEffect", 0);
		toggleView ();

	}

	void getToggle(){



		if (PlayerPrefs.GetInt ("isBGM", 0) == 1) {
			PrepClass.isBGM = true;
		} else {
			PrepClass.isBGM = false;
		}

		if (PlayerPrefs.GetInt ("isEffect", 0) == 1) {
			PrepClass.isEffect = true;
		} else {
			PrepClass.isEffect = false;
		}
		toggleView ();
	}

	void toggleView(){
		m_bgmToggle.isOn = !PrepClass.isBGM;
		m_effectToggle.isOn = !PrepClass.isEffect;
		SoundFactoryClass.GetInstance.setMute (TYPE_SOUND.BGM);
		SoundFactoryClass.GetInstance.setMute (TYPE_SOUND.EFFECT);
			
	}

}

