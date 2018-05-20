using System;
using UnityEngine;

public class AmplifyingBuffDataClass : BuffDataClass
{


	[SerializeField] float m_damagePercent;
	[SerializeField] float m_explosiveRange;

	public float damagePercent{get{ return m_damagePercent; }}
	public float explosiveRange{get{ return m_explosiveRange; }}

//	void Awake(){
//		buffStateSet(TYPE_BUFF_STATE_ACT.ATTACK);
//	}

//	void Start(){
//		startCoroutine (-1f, null);
//	}
}

