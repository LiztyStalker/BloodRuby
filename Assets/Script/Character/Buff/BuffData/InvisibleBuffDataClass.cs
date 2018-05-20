using System;
using UnityEngine;

public class InvisibleBuffDataClass : BuffDataClass
{
//	const float c_time = 3f;

//	void Awake(){
//		buffStateSet(TYPE_BUFF_STATE_ACT.ATTACK);
//	}


//	void Start(){
////		Debug.Log ("시작");
//		startCoroutine (c_time, null);
//	}

	public override void buffStart (ICharacterInterface ownerCharacter, ICharacterInterface actCharacter)
	{
		base.buffStart (ownerCharacter, actCharacter);
	}

	public override bool buffEnd ()
	{
		return base.buffEnd ();
	}



	public override bool useBuff (ICharacterInterface useActcharacter)
	{
		buffEnd ();
		return false;
	}

	public override bool buffReplace ()
	{
		initTime ();
		return true;
	}
}



