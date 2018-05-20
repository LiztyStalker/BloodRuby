using System;
using UnityEngine;



public class CastingParticleBuffDataClass : BuffDataClass
{
	[SerializeField] ParticleLifeClass m_fireParticle;
	[SerializeField] int m_damage;
//	[SerializeField] BuffDataClass m_buffData;

//	float angle = 0f;

	public override void buffStart (ICharacterInterface ownerCharacter, ICharacterInterface setCharacter)
	{
		base.buffStart (ownerCharacter, setCharacter);
		setBuffLoopDelegate(castLoop);
		if (GetComponent<LineRenderer> () != null) {
			GetComponent<LineRenderer> ().sortingLayerName = "Character";
		}
		initTime ();
		Debug.Log ("maxTime : " + maxTime);
//		angle = ownerCharacter.angle;
	}

	void castLoop(){
		ownerCharacter.characterCtrler.setMsg (this);
	}

	public override bool buffEnd ()
	{
		if(m_fireParticle != null){
			if (!ownerCharacter.isDead) //A0.7 시전 도중에 사망시 발사 불가
			{
				ParticleLifeClass particle = (ParticleLifeClass)Instantiate (m_fireParticle, ownerCharacter.shootPos, Quaternion.identity); 
				//Debug.LogError ("angle : " + ownerCharacter.angle);
				particle.setParticleBox (ownerCharacter.characterCtrler, icon, m_damage, 1f, ownerCharacter.angle);
			}
		}

		return base.buffEnd ();

	}


}


