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
public class FirebatSkill2Class : SkillClass, ISkillMOSInterface, IBullet
{

//	[SerializeField] 버프
//	[SerializeField] float m_time;
//	[SerializeField] float m_rate;

	[SerializeField] ParticleLifeClass m_bulletParticle;
	[SerializeField] float m_movement;
	[SerializeField] int m_damage;
	[SerializeField] float m_radius;
	[SerializeField] Vector2 m_fireSize;
//	[SerializeField] BulletClass m_bullet;
//	[SerializeField] BuffDataClass m_buff;

	UICharacterClass m_characterCtrler;
//	ICharacterInterface m_player;

	ICharacterInterface character{get{return m_characterCtrler.character;}}
	public UICharacterClass characterCtrler{get{return m_characterCtrler;}}
	public Type type { get { return this.GetType(); } }
	public bool isPenetrate{ get { return false; } set{}}
	public int damage{ get { return m_damage; } set{}}
	public bool isInTrench{ get { return true; } }
	public Sprite weaponSprite{ get { return iconRound; } }

	float m_range;

	Vector2 m_moveVector;
	Vector2 m_pastPos;
	Vector2 m_attackPos;

//	public override void initSkill (ICharacterInterface player){
		//player.buffAdd (m_buff);
//	}

	public override bool skillAction(ICharacterInterface player){
		
		m_pastPos = new Vector2(player.transform.position.x, player.transform.position.y);


		if (player.GetType () == typeof(CPUClass)) {
			((CPUClass)player).targetRotate ();
			m_attackPos = PrepClass.movementCalculator (m_radius * 0.5f, player.angle);
		} else {
			m_attackPos = PrepClass.movementCalculator (m_radius * 0.5f, player.skillAngle);
		}
		m_attackPos.Set (player.transform.position.x + m_attackPos.x, player.transform.position.y + m_attackPos.y);

		//역각도를 알아내서 뒤로 3밀림
		float re_angle = PrepClass.reverseAngleCalculator(player.angle);

		m_moveVector = PrepClass.movementCalculator (m_movement, re_angle);
		m_moveVector.Set (m_moveVector.x + player.transform.position.x, m_moveVector.y + player.transform.position.y);
//		player.transform.position = m_moveVector;


		RaycastHit2D[] rayHits = Physics2D.LinecastAll  (m_pastPos, m_moveVector);
		bool isChk = false;

		foreach (RaycastHit2D rayHit in rayHits) {
			Debug.Log ("pos : " + rayHit.collider.tag + " " + rayHit.centroid);
			if (PrepClass.isCharacterTag (rayHit.collider.tag))
				continue;
			else if (rayHit.collider.tag == "Wall" || rayHit.collider.tag == "ActObject") {
				Vector2 centroid = PrepClass.movementCalculator (0.1f, PrepClass.reverseAngleCalculator(re_angle));
				centroid.Set (centroid.x + rayHit.centroid.x, centroid.y + rayHit.centroid.y);
				m_range = Vector2.Distance (m_pastPos, centroid) * 0.5f;
				player.transform.position = centroid;
				isChk = true;
				break;
			} 

		}

		//탄환 생성 및 공격
		ParticleLifeClass bullet = Instantiate(m_bulletParticle, player.shootPos, Quaternion.identity);
		bullet.setParticleBox(player.characterCtrler, iconRound, m_damage, m_fireSize.x, m_fireSize.y, false, false, m_radius, player.angle);
		bullet.transform.SetParent (player.transform);

		if (!isChk) {
			m_range = Vector2.Distance (m_pastPos, m_moveVector) * 0.5f;
			if (player.GetType () == typeof(CPUClass))
				((CPUClass)player).warpPosition (m_moveVector);
			else
				player.transform.position = m_moveVector;
		}


		return base.skillAction (player);
	}

	public override void skillGuideLine(ICharacterInterface player){
		//플레이어의 앞에 가이드라인 보이기
		//


		//적 찾기
		//가장 가까운 적 목표로 가져오기
		player.setSkillGuideLine (this);//, getTarget(player, TYPE_TEAM.ENEMY));


	}

}


