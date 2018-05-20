using System;
using UnityEngine;

public class CharacterSoundClass : MonoBehaviour
{
	[SerializeField] string m_naturalWalkSoundKey;
	[SerializeField] string m_roadWalkSoundKey;
	[SerializeField] string m_waterWalkSoundKey;
	[SerializeField] string m_forestWalkSoundKey;


	SoundPlayClass m_soundPlayer;

	void Start(){
		m_soundPlayer = GetComponent<SoundPlayClass> ();
	}


	public void soundPlay(float time){
		
	}




}


