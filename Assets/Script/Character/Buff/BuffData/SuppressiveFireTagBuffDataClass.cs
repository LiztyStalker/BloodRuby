using System;
using UnityEngine;

public class SuppressiveFireTagBuffDataClass : BuffDataClass
{

//	[SerializeField] int m_time;
//
//	void Start(){
//		startCoroutine (m_time, null);
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

	public override bool buffReplace ()
	{
		initTime ();
		return true;
	}


}


