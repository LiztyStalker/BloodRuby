using System;

public class WireBuffDataClass : BuffDataClass
{

	//	[SerializeField] float m_moveSpeedPercent;

	//	public float damagePercent{get{ return m_damagePercent; }}
	//	public float explosiveRange{get{ return m_explosiveRange; }}

//	void Awake(){
//		buffStateSet(TYPE_BUFF_STATE.CONTINUE);
//	}


//	void Start(){
//		//Debug.Log ("Start : " + GetInstanceID());
//
//		startCoroutine (-1f, null);
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

}


