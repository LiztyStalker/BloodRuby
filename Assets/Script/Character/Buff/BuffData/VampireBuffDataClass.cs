using UnityEngine;
using System.Collections;

public class VampireBuffDataClass : BuffDataClass {

	[SerializeField] float m_value;
	[SerializeField] TYPE_VALUE m_typeValue;
	[SerializeField] ParticleSystem m_vampireParticle;

//	void Awake(){
//		buffStateSet(TYPE_BUFF_STATE_ACT.ATTACK);
//	}


	public override bool useBuff (ICharacterInterface useActCharacter, IBullet bullet){


		Debug.LogWarning ("Vampire");


		float value = getAssistBuffData (m_value);


		ParticleSystem tmpParticle = Instantiate (m_vampireParticle, ownerCharacter.transform.position, Quaternion.identity);
		tmpParticle.transform.SetParent (ownerCharacter.transform);
		tmpParticle.gameObject.AddComponent<ParticleLifeClass> ();

		switch(m_typeValue){
		case TYPE_VALUE.VALUE:
			actCharacter.addHealth ((int)(value * 100f), ownerCharacter);
			return true;
		case TYPE_VALUE.PERCENT:
			actCharacter.addHealth((int)((float)bullet.damage * value), ownerCharacter);
			return true;
		}
		return false;

	}
}
