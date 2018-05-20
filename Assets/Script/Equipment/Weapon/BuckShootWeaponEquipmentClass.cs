using System;
using UnityEngine;

public class BuckShootWeaponEquipmentClass : WeaponEquipmentClass
{

	[SerializeField] int m_slugCnt = 1;

	public override void attackAction (UICharacterClass characterCtrler, UnityEngine.Vector2 shootPos)
	{
		int slugCnt = m_slugCnt;
		while (slugCnt-- > 0) {
			BulletClass tmpBullet = (BulletClass)Instantiate (bulletObject, shootPos, new Quaternion ());

			if (slugCnt < m_slugCnt - 1) //A0.7 슬러그 탄환 1개만 소리내기
				tmpBullet.GetComponent<AudioSource> ().mute = true;
			tmpBullet.attack (characterCtrler, this, accuracy);

		}
	}
}


