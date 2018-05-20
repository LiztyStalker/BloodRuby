using System;
using UnityEngine;

public class CureTagBuffDataClass : BuffDataClass
{
	[SerializeField] int m_healthPerSecond;
//	[SerializeField] int m_time;


	protected override void Start(){
		setBuffLoopDelegate (healingCoroutine);
		base.Start ();
//		startCoroutine (m_time, healingCoroutine);
	}

	void healingCoroutine(){
		actCharacter.addHealth ((int)((float)m_healthPerSecond * PrepClass.c_timeGap), ownerCharacter);
	}


	public override bool buffReplace ()
	{
		initTime ();
		return true;
	}
}


