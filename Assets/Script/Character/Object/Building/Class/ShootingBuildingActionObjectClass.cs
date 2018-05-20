using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBuildingActionObjectClass : BuildingActionObjectClass {

	[SerializeField] bool m_isCtrl; //직접조종 - true
	[SerializeField] int m_damage; //데미지
	[SerializeField] float m_moveSpeed; //발사 속도
	[SerializeField] float m_range; //사정거리
	[SerializeField] float m_delayTime; //후속 발사 시간
	[SerializeField] float m_accurary; //명중률
	[SerializeField] float m_viewRange; //시야

	TYPE_TEAM m_team;
	TYPE_MOS m_mos;

	[SerializeField] BulletClass m_bullet; //탄환
	[SerializeField] Transform m_shootPos; //발사 위치
	[SerializeField] ParticleSystem m_shootParticle; //발사 효과

	//	ICharacterInterface m_character;

	Coroutine coroutine_Turret = null;
	ICharacterInterface m_target = null;


	public int damage{get{return m_damage;} set{ }}
	public bool isPenetrate{ get { return false; } set { } }
	public ICharacterInterface character { get{ return (m_characterCtrler == null) ? null : m_characterCtrler.character; } }
	public Type type{ get { return GetType (); } }



	public override void initAction(UICharacterClass characterCtrler, BuildingObjectClass buildingObject, CharacterFrameClass buildingFrame){
		Debug.Log ("turret");

		base.initAction (characterCtrler, buildingObject, buildingFrame);
		if (character != null)
			m_team = character.team;
		else
			m_team = TYPE_TEAM.ENEMY;
		coroutine_Turret = StartCoroutine (turretCoroutine ());
	}


	IEnumerator turretCoroutine(){


		while (gameObject.activeSelf) {

			if (m_target == null) {

				RaycastHit2D[] hits = Physics2D.CircleCastAll (transform.position, m_viewRange, Vector2.zero);

				foreach (RaycastHit2D hit in hits) {
					if (PrepClass.isCharacterTag(hit.collider.tag)) {

						ICharacterInterface enemyCharacter = hit.collider.GetComponent<ICharacterInterface> ();

						if (enemyCharacter != character) {
							if (enemyCharacter.team != m_team) {
								if (!enemyCharacter.isDead) {
									Debug.Log ("타겟 : " + hit.collider.name);

									RaycastHit2D[] rays = Physics2D.RaycastAll (transform.position, hit.transform.position - transform.position, m_viewRange);

									foreach (RaycastHit2D ray in rays) {
										if (PrepClass.isCharacterTag(ray.collider.tag)) {

											ICharacterInterface rayCharacter = ray.collider.GetComponent<ICharacterInterface> ();

											if (rayCharacter.team != m_team) {
												if (!rayCharacter.isDead) {
													Debug.Log ("리얼타겟 : " + ray.collider.name);
													m_target = rayCharacter;
													break;
												}
											}
										} else if (ray.collider.tag == "ActObject") {
											if (ray.collider.transform.parent.GetComponent<BuildingObjectClass> ().team == m_team)
												continue;
											//관통
										} else if (ray.collider.tag == "Object" || ray.collider.tag == "Wall") {
											break;
										}
									}

								}
							}
						}
					}


				}
			} else if (m_viewRange < Vector2.Distance (m_target.transform.position, transform.position)) {
				m_target = null;
			} else {

				if (m_target.isDead) {
					m_target = null;
					continue;
				}

				Vector2 dirVec = m_target.transform.position - transform.position;
				float angle = Mathf.Atan2 (dirVec.y, dirVec.x) * Mathf.Rad2Deg;
				m_buildingFrame.setAngle (angle);


				BulletClass bullet = (BulletClass)Instantiate (m_bullet, m_shootPos.position, Quaternion.identity);
				bullet.attack (m_characterCtrler, m_weaponSprite, m_damage, m_range, m_moveSpeed, angle);

				ParticleSystem shootParticle = (ParticleSystem)Instantiate (m_shootParticle, m_shootPos.position, Quaternion.identity);
				var shootMain = shootParticle.main;
				shootMain.startRotation = -angle * Mathf.Deg2Rad;


//				Debug.Log ("weaponSprite : " + m_weaponSprite);
			}

			yield return new WaitForSeconds (PrepClass.c_timeGap);
		}

		//
	}


	void OnDisable(){
		if (coroutine_Turret != null)
			StopCoroutine (coroutine_Turret);				
	}



}

