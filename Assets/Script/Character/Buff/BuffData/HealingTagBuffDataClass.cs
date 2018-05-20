using System;
using UnityEngine;

public class HealingTagBuffDataClass : BuffDataClass
{

//	[SerializeField] int m_healthPoint;
//	[SerializeField] float m_time;
	[SerializeField] int m_healthPerSecond;
	[SerializeField] int m_maxCount;

//	int m_nowCnt = 0;
//	float m_healTime = 0f;

//	void Awake(){
//		buffStateSet (TYPE_BUFF_STATE.COUNT);
//	}

	int count = 0;

	protected override void Start(){
		//Debug.Log ("Start : " + GetInstanceID());
		setBuffLoopDelegate(healingCoroutine);
		base.Start ();
	}

	public override void buffStart (ICharacterInterface ownerCharacter, ICharacterInterface actCharacter)
	{
		base.buffStart (ownerCharacter, actCharacter);
		count++;
	}
//
	public override bool buffEnd ()
	{
		count--;
		if (count <= 0) {
			Debug.Log ("힐 버프 종료");
			return base.buffEnd ();
		}
		return false;
	}

	/// <summary>
	/// 1회 한정 치유
	/// </summary>
	void healingCoroutine(){

		if (count > m_maxCount) 
			m_count = m_maxCount;
		else
			m_count = count;

		int healthPerSecond = getAssistBuffData (m_healthPerSecond);

		int healingPoint = (int)((float)(healthPerSecond) * PrepClass.c_timeGap * m_count);
		actCharacter.addHealth (healingPoint, ownerCharacter);//		if (m_healTime < 0f) {
	}


	public override bool buffReplace ()
	{
		count++;
		return true;
	}





}


