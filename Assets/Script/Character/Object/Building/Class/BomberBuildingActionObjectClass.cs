using System;
using System.Collections;
using UnityEngine;

public class BomberBuildingActionObjectClass : BuildingActionObjectClass, IBullet
{
	[SerializeField] bool m_isAccess; //접근시 발동
	[SerializeField] bool m_isActive; //즉시 활성화
	[SerializeField] int m_bombCnt; //폭발 횟수
	[SerializeField] float m_areaRange; //폭발 가능 범위
	[SerializeField] float m_bombRange; //폭발 범위
	[SerializeField] int m_damage; //폭발 1회 데미지
	[SerializeField] float m_delayTime; //지연폭발
	[SerializeField] float m_gapTime = 0.1f; //후속 폭발
	[SerializeField] bool m_isRandom = true; //폭발 가능 범위 랜덤 여부 - false : 중앙에서만 폭발
	[SerializeField] ParticleSystem m_bombParticle; //폭발 효과

	ParticleSystem m_particle;
//	ICharacterInterface m_character;

	public int damage{get{return m_damage;} set{ }}
	public bool isPenetrate{ get { return false; } set { } }
	public UICharacterClass characterCtrler { get{ return m_characterCtrler; } }
	ICharacterInterface character { get{ return m_characterCtrler.character; } }
	public Type type{ get { return GetType (); } }
	public bool isInTrench{get{return true;}}
	public Sprite weaponSprite{get{ return m_weaponSprite; }}
	TYPE_TEAM team {
		get{
			if (character == null)
				return TYPE_TEAM.ENEMY;
			return character.team;
		}
	}
	
			
	Coroutine coroutine_bomber;



	public override void initAction(UICharacterClass characterCtrler, BuildingObjectClass buildingObject, CharacterFrameClass buildingFrame){
		base.initAction (characterCtrler, buildingObject, buildingFrame);
		if(m_isActive) useAction ();
	}

	public void setWeaponSprite(Sprite weaponSprite){
		m_weaponSprite = weaponSprite;
	}


	public override void useAction(){
		initBomber ();
	}



	void Update(){
		if (m_buildingAction != null) {

			if (m_isAccess) {

				RaycastHit2D[] rays = Physics2D.CircleCastAll (transform.position, m_bombRange, Vector2.zero);

				foreach (RaycastHit2D ray in rays) {
					try{
						if (PrepClass.isCharacterTag (ray.collider.tag)) {
							ICharacterInterface target = ray.collider.GetComponent<ICharacterInterface>();
							target = PrepClass.getCharacter(character, target, false, false, false);

							if(target != null){	
								m_buildingAction.useAction ();
								removeObject (gameObject);
							}
						}
					}
					catch(UnityException e){
						Debug.LogError ("buildingError : " + e.Message);
					}
				}
			}

		}
	}

	void initBomber(){
		m_particle = GetComponent<ParticleSystem> ();
		if(m_particle != null) m_particle.startLifetime = (float)m_bombCnt * m_gapTime + m_delayTime;

		StartCoroutine (bomberCoroutine ());
	}

	IEnumerator bomberCoroutine(){
		int bombCnt = m_bombCnt;
		float delayTime = m_delayTime;

		while (delayTime > 0f) {
			//포격 사운드
			delayTime -= PrepClass.c_timeGap;
			yield return new WaitForSeconds (PrepClass.c_timeGap);
		}

		float randTime = m_gapTime * 0.5f;

		while (bombCnt-- > 0) {
//
			if (m_isRandom) {
				float angle = UnityEngine.Random.Range (0, 360f);
				float distance = UnityEngine.Random.Range (0f, 1f);
				Vector2 angleDistance = angleVelocity (angle, distance * m_areaRange);
				Vector2 bombVec = new Vector2 (transform.position.x + angleDistance.x, transform.position.y + angleDistance.y);
				bombAction (bombVec);
			}
			else {
				bombAction (transform.position);
			}
//
//
			yield return new WaitForSeconds (m_gapTime + UnityEngine.Random.Range(-randTime, randTime));
		}

	}

	void bombAction(Vector2 bombPos){

		if (m_bombParticle != null) {
			ParticleSystem particle = (ParticleSystem)Instantiate (m_bombParticle, bombPos, new Quaternion ());
			ParticleLifeClass particleLifeCycle = particle.GetComponent<ParticleLifeClass> ();
			if (particleLifeCycle == null)
				particleLifeCycle = particle.gameObject.AddComponent<ParticleLifeClass> ();
			

			particleLifeCycle.setParticleCircle(characterCtrler, weaponSprite, damage, m_bombRange, 1f);
		}

//		RaycastHit2D[] hits = Physics2D.CircleCastAll (bombPos, m_bombRange, Vector2.zero);
//
//		foreach (RaycastHit2D hit in hits) {
//			if (PrepClass.isCharacterTag (hit.collider.tag)) {
//				if (!hit.collider.GetComponent<ICharacterInterface> ().isDead) {
//					if (characterCtrler == null) {
//						hit.collider.GetComponent<ICharacterInterface> ().hitAction (TYPE_TEAM.NONE, this);
//					} else if (hit.collider.GetComponent<ICharacterInterface> ().team != character.team) {
//						hit.collider.GetComponent<ICharacterInterface> ().hitAction (character.team, this);
//					}
//				}
//
//			} 
//			else if (hit.collider.tag == "ActObject") {
//				Debug.Log ("actObject : " + hit.collider.name);
//				if (hit.collider.transform.parent.GetComponent<BuildingObjectClass> () != m_buildingObject) {
//					hit.collider.transform.parent.GetComponent<BuildingObjectClass> ().hitAction (TYPE_TEAM.ENEMY, this);
//				}
//			}
//		}
	}

	Vector2 angleVelocity(float angle, float distance = 1f){
		return new Vector2 (Mathf.Cos(angle * Mathf.Deg2Rad) * distance, Mathf.Sin(angle * Mathf.Deg2Rad) * distance);
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere  (transform.position, m_areaRange);
	}

}


