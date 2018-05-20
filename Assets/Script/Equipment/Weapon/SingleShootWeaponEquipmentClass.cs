using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShootWeaponEquipmentClass : WeaponEquipmentClass {



	public override void attackAction (UICharacterClass characterCtrler, Vector2 shootPos){
		BulletClass tmpBullet = (BulletClass)Instantiate (bulletObject, shootPos, new Quaternion ());
		tmpBullet.attack (characterCtrler, this, accuracy);
	}

}
