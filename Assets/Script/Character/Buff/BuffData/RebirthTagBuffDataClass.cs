using System;
using UnityEngine;

public class RebirthTagBuffDataClass : BuffDataClass
{

	[SerializeField] BuffDataClass m_invisibleBuff;
	[SerializeField] float m_healingPercent;
//	float c_time = 3f;

//	void Awake(){
//		buffStateSet(TYPE_BUFF_STATE_ACT.ATTACK);
//	}


//	void Start(){
//		startCoroutine (c_time, null);
//	}

	public override void buffStart (ICharacterInterface ownerCharacter, ICharacterInterface actCharacter)
	{
		base.buffStart (ownerCharacter, actCharacter);
		actCharacter.addHealth ((int)((float)actCharacter.maxHealth * m_healingPercent), ownerCharacter);
		actCharacter.rebirthAction ();
		actCharacter.buffAdd (m_invisibleBuff, ownerCharacter, actCharacter);

		actCharacter.addReport (1, actCharacter.mos, TYPE_REPORT.REB_GET);
		ownerCharacter.addReport (1, ownerCharacter.mos, TYPE_REPORT.REB_SET);
//		m_character.addState.isInvisible = true;

//		character.buffAdd (m_invisibleBuff, character);
	}

//	public override bool buffEnd ()
//	{
//		m_character.addState.isInvisible = false;
//		return base.buffEnd ();
//	}
}


