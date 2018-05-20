using System;
using UnityEngine;

/// <summary>
/// 능력치 증감 버프
/// AddStateClass가 1개 이상 있어야 함
/// </summary>
public class AddStateBuffDataClass : BuffDataClass
{
//	[SerializeField] float m_time;

//	void Start(){
//		startCoroutine (m_time, null);
//	}

	public override void buffStart (ICharacterInterface ownerCharacter, ICharacterInterface setCharacter)
	{
		base.buffStart (ownerCharacter, setCharacter);
		addValueState (this);
	}

	public override bool buffEnd ()
	{
		returnValueState(this);
		return base.buffEnd ();

	}


	public override bool buffReplace ()
	{
		initTime ();
		return true;
	}

}


