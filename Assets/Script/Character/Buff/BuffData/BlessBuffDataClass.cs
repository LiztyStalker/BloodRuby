using System;
using UnityEngine;

public class BlessBuffDataClass : BuffDataClass
{

//	[SerializeField] float m_time;
//
//	void Start(){
//		startCoroutine (m_time, null);
//	}
//
	public override void buffStart (ICharacterInterface ownerCharacter, ICharacterInterface setCharacter)
	{
		base.buffStart (ownerCharacter, setCharacter);
		addValueState (this);
	}

	public override bool buffEnd ()
	{
		returnValueState (this);
		return base.buffEnd ();

	}

	public override bool buffReplace ()
	{
		initTime ();
		return true;
	}
}


