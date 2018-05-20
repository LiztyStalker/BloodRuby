using System;
using UnityEngine;

public class BattleBuffDataClass : BuffDataClass
{

	[SerializeField] int m_skillSlot;
	[SerializeField] float m_cooltimeReturn;

	protected override void Start ()
	{
		setBuffLoopDelegate (moveDebuff);
		base.Start ();
	}

	public override void buffStart (ICharacterInterface ownerCharacter, ICharacterInterface actCharacter)
	{
		base.buffStart (ownerCharacter, actCharacter);
		addValueState (this);

	}

	public override bool buffEnd ()
	{
		
		returnValueState (this);
		return base.buffEnd ();
	}


	void moveDebuff(){
		//자동으로 시전자에게 이동
	}



	public override bool useBuff (ICharacterInterface character, IBullet bullet)
	{
		Debug.Log ("dead");
		ownerCharacter.resetSkillCoolTime (m_skillSlot, m_cooltimeReturn);
		return base.useBuff (character, bullet);
	}
}


