using System;
using System.Collections;
using UnityEngine;

public class ParticleLifeClass : MonoBehaviour
{

	ParticleAttackClass m_particleAttack;

	void Start(){
		ParticleSystem particle = GetComponent<ParticleSystem> ();
		if(particle == null) Destroy (gameObject, 1f);
		Destroy (gameObject, particle.main.duration);
	}	

	public void setParticleCircle(UICharacterClass characterCtrler, Sprite weaponSprite, int damage, float radius, float scale){
		m_particleAttack = getParticleAttack ();
		m_particleAttack.setParticleCircle (characterCtrler, weaponSprite, damage, radius, scale);
	}
		
	public void setParticleBox(UICharacterClass characterCtrler, Sprite weaponSprite, int damage, float x, float y, bool isOffsetRevX, bool isOffsetRevY, float scale, float angle){
		m_particleAttack = getParticleAttack ();
		m_particleAttack.setParticleBox (characterCtrler, weaponSprite, damage, x, y, isOffsetRevX, isOffsetRevY, scale, angle);
	}

	public void setParticleBox(UICharacterClass characterCtrler, Sprite weaponSprite, int damage, float scale, float angle){
		m_particleAttack = getParticleAttack ();
		m_particleAttack.setParticleBox (characterCtrler, weaponSprite, damage, scale, angle);
	}


	ParticleAttackClass getParticleAttack(){
		ParticleAttackClass particleAttack = GetComponent<ParticleAttackClass> ();
		if (particleAttack == null) 
			particleAttack = gameObject.AddComponent<ParticleAttackClass> ();
		return particleAttack;
	}

}


