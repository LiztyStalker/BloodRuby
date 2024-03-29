//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.36373
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;
public class SniperSkill1Class : SkillClass, ISkillMOSInterface
{
	//헤드샷
	[SerializeField] BulletClass m_bullet;
//	[SerializeField] TYPE_VALUE m_valueType;
	[SerializeField] int m_damage;
	[SerializeField] float m_range;
	[SerializeField] float m_moveSpeed;

	//버스트샷
	[SerializeField] BulletClass m_bustBullet;
//	[SerializeField] TYPE_VALUE m_bustValueType;
	[SerializeField] int m_bustDamage;
	[SerializeField] float m_bustRange;
	[SerializeField] float m_bustMoveSpeed;
	[SerializeField] float m_bustCnt = 8;

	public override bool skillAction(ICharacterInterface player){
		//헤드샷 버프
		//player.buffAdd (m_skillBuffData.getBuffData (), player, player);



		setParticle (player.transform.position, player.transform);

		//헤드샷
		if (player.addState.getBuff (typeof(WeaponChangeBuffDataClass)) == null) {
			BulletClass bullet = Instantiate (m_bullet, player.shootPos, Quaternion.identity);
			bullet.attack (player.characterCtrler, iconRound, m_damage, m_range, m_moveSpeed, player.angle, 100f, true);
		}
		//버스트 샷
		else {
			for (int i = 0; i < m_bustCnt; i++) {
				BulletClass bullet = Instantiate (m_bustBullet, player.shootPos, Quaternion.identity);
				bullet.attack (player.characterCtrler, iconRound, m_bustDamage, m_bustRange, m_bustMoveSpeed, player.angle, 70f, false);
			}
			player.useAmmo = 0;
		}

		return base.skillAction (player);
	}

	public override void skillGuideLine(ICharacterInterface player){
		//플레이어의 앞에 가이드라인 보이기

		//적 찾기
		//가장 가까운 적 목표로 가져오기
		player.setSkillGuideLine (this);//, getTarget(player, TYPE_TEAM.ENEMY));


	}
}


