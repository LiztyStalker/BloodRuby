using System;
using UnityEngine;

public class HeadShotBuffDataClass : BuffDataClass
{
	[SerializeField] BulletClass m_bullet;
//	[SerializeField] int m_damage;
	[Range(0f, 1f)][SerializeField] float m_damagePercent;
	[SerializeField] int m_maxCount;


//	void Awake(){
//		buffStateSet(TYPE_BUFF_STATE_ACT.ATTACK);
//	}

//	void Start(){
//		startCoroutine (-1f, null);
//	}

	public override void buffStart (ICharacterInterface ownerCharacter, ICharacterInterface setCharacter)
	{
		base.buffStart (ownerCharacter, setCharacter);
		if(m_count < m_maxCount) m_count++;
	}
//
//	public override void buffEnd ()
//	{
//		base.buffEnd ();
//	}

	/// <summary>
	/// 버프 사용
	/// </summary>
	/// <returns>true</returns>
	/// <c>false</c>
	/// <param name="character">사용자</param>
	public override bool useBuff (ICharacterInterface character)
	{
		Debug.Log ("헤드샷 사용");

		//Debug.Log ("hit : " + m_addState.character.team);

		BulletClass bullet = (BulletClass)Instantiate (m_bullet, character.shootPos, new Quaternion ());
		bullet.attack (character.characterCtrler, character.mosData.weapon, character.mosData.weapon.accuracy);
		bullet.damage += (int)((float)bullet.damage * m_damagePercent);
		bullet.isPenetrate = true;

		m_count--;
		if(m_count <= 0) buffEnd ();
		return true;
	}

	public override bool buffReplace ()
	{
		if(m_count < m_maxCount) m_count++;
		return true;
	}
}


