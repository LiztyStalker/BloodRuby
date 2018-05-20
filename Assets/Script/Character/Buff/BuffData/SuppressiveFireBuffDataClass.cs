using System;
using UnityEngine;

public class SuppressiveFireBuffDataClass : BuffDataClass
{
	//	[SerializeField] int m_healthPoint;

	[SerializeField] BuffDataClass m_buff;
//	[SerializeField] int m_time;

//	void Awake(){
//		buffStateSet(TYPE_BUFF_STATE_ACT.ATTACK);
//	}


//	void Start(){
//		//Debug.Log ("Start : " + GetInstanceID());
//		startCoroutine (m_time, null);
//	}


//	public override void buffStart (ICharacterInterface character)
//	{
//		base.buffStart (character);
//	}
//
//	public override void buffEnd ()
//	{
//		base.buffEnd ();
//	}

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
		useActCharacter.buffAdd(m_buff, ownerCharacter, useActCharacter);
		return false;
	}

	public override bool buffReplace ()
	{
		initTime ();
		return true;
	}

}


