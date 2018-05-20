using System;
using UnityEngine;

public class ShieldBuffDataClass : BuffDataClass
{
	[SerializeField] int m_shieldPoint;
//	[SerializeField] float m_time;

	public int shieldMax{ get { return m_shieldPoint; } }
	//int m_shield;

//	void Awake(){
//		buffStateSet(TYPE_BUFF_STATE_ACT.HIT);
//	}

	protected override void Start(){
		//Debug.Log ("Start : " + GetInstanceID());
		m_count = m_shieldPoint;
//		startCoroutine (m_time, null);
		base.Start ();
	}

//	public override void buffStart (ICharacterInterface character)
//	{
//		base.buffStart (character);
//	}
//
//	public override bool buffEnd ()
//	{
//		return base.buffEnd ();
//	}



	public override bool useBuff (ICharacterInterface character, IBullet bullet)
	{
		if (m_count < bullet.damage) {
			buffEnd ();
		}
		m_count -= bullet.damage;
		return true;
	}


	public override bool buffReplace ()
	{
		initTime ();
		return true;
	}

}


