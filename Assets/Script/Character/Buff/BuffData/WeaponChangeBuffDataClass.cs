using System;
using UnityEngine;

public class WeaponChangeBuffDataClass : BuffDataClass
{
	[SerializeField] WeaponEquipmentClass m_equipment;
//	[SerializeField] bool m_isTelescopeAttack = false; //false 발동시 공격불가

	WeaponEquipmentClass m_tmpEquipment;

	int bulletCount = 0;

//	void Awake(){
//		buffStateSet(TYPE_BUFF_STATE.TOGGLE);
//
//	}

//	void Start(){
//		startCoroutine (-1f, null);
//	}

	public override void buffStart (ICharacterInterface ownerCharacter, ICharacterInterface actCharacter)
	{
		base.buffStart (ownerCharacter, actCharacter);
		setConstraint ();
//		actCharacter.addState.isTelescopeAttack = m_isTelescopeAttack;
		addValueState (this);

		bulletCount = actCharacter.useAmmo;
		m_tmpEquipment = actCharacter.weaponChange ((WeaponEquipmentClass)m_equipment.Clone (), m_equipment.ammo);

		Debug.Log ("무기 : " + actCharacter.mosData.weapon.name);
	}

	public override bool buffEnd ()
	{
//		actCharacter.addState.isTelescopeAttack = !m_isTelescopeAttack;
		resetConstraint();
		returnValueState (this);
		actCharacter.weaponChange (m_tmpEquipment, bulletCount);
		return base.buffEnd ();
	}

}


