using System;
using UnityEngine;

public class CoolTimeBuffDataClass : BuffDataClass
{

//	[SerializeField] float m_time;

//	void Awake(){
//		buffStateSet(TYPE_BUFF_STATE.TIME);
//	}

//	void Start(){
//		//Debug.Log ("Start : " + GetInstanceID());
//
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


