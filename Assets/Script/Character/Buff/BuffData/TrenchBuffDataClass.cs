using System;


public class TrenchBuffDataClass : BuffDataClass
{

//	void Start(){
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


