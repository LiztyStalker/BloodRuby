using System;
using UnityEngine;

public class AttackTaggingBuffDataClass : TaggingBuffDataClass
{
//	[SerializeField] float m_time;

	void Awake(){
		buffStateSet(TYPE_BUFF_STATE_ACT.ATTACK);
	}

//	void Start(){
//		startCoroutine (time, null);
//	}

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

	/// <summary>
	/// 버프 사용 당함
	/// </summary>
	/// <param name="character">Character.</param>
	/// <param name="bullet">Bullet.</param>
	/// <returns><c>true</c>, if buff was used, <c>false</c> otherwise.</returns>
	/// <param name="useActCharacter">Use act character.</param>
	public override bool useBuff (ICharacterInterface useActCharacter, IBullet bullet)
	{
		//적에게 디버프 걸기
		useActCharacter.buffAdd(buffData, ownerCharacter, useActCharacter);
		return false;
	}

//	public override bool buffReplace ()
//	{
//		if(m_time > 0f) initTime (m_time);
//		return true;
//	}
}


