using System;
using UnityEngine;

public class DamageBuffDataClass : BuffDataClass, IBullet
{
	
	[SerializeField] int m_damagePerSecond;
//	[SerializeField] float m_time;



	ICharacterInterface character { get { return ownerCharacter; } }
	public UICharacterClass characterCtrler { get { return ownerCharacter.characterCtrler; } }
	public Type type { get { return this.GetType(); } }
	public bool isPenetrate{ get { return false; } set{ }}
	public int damage{ get { return (int)((float)m_damagePerSecond * PrepClass.c_timeGap); } set{ }}
	public bool isInTrench{get{return true;}}
	public Sprite weaponSprite{ get { return m_icon; } }


	protected override void Start(){
		setBuffLoopDelegate (burningBuff);
		base.Start ();
//		startCoroutine (m_time, burningBuff);
	}


	public override void buffStart (ICharacterInterface ownerCharacter, ICharacterInterface actCharacter)
	{
		base.buffStart (ownerCharacter, actCharacter);
		addValueState (this);
	}

	public override bool buffEnd ()
	{
		returnValueState (this);
		return base.buffEnd ();
	}

	void burningBuff(){
		actCharacter.hitAction (character.team, this);
	}


}


