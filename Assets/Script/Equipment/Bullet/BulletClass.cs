using UnityEngine;
using System.Collections;
using System;

//A0.8 관통여부 및 탄환 이펙트
public enum TYPE_BULLET_EFFECT{HIT, SLASH, THRUST, EXPLOSIVE}
public enum TYPE_BULLET_PENETRATE{NONE, LIFE, OBJECT, WALL}

public class BulletClass : MonoBehaviour, IBulletPrototype, IBullet{

	//집탄율


	const float c_shortRangeWeaponTime = 2.5f;
	const float c_sizeOffset = 2f;

//	ICharacterInterface m_character;
	UICharacterClass m_characterCtrler;

	[SerializeField] int m_damage;
	[SerializeField] float m_moveSpeed;
	[SerializeField] float m_range;
	[SerializeField] ParticleSystem m_hitParticle;
//	[SerializeField] ParticleSystem m_shootParticle;
	[SerializeField] TYPE_TEAM m_team;
	[SerializeField] bool m_isRotate;
	[SerializeField] float m_rotate;
	[SerializeField] float m_rotateOffset;
	//A0.8 탄환 이펙트
	[SerializeField] TYPE_BULLET_EFFECT m_typeBulletEffect;

	bool m_isInTrench = false;

	//A0.8 관통여부 세분화
	bool m_isPenetrate = false;
//	TYPE_BULLET_PENETRATE m_typeBulletPenetrate;
	TYPE_RANGE m_typeRange;
	TYPE_MOS m_mos;

	Vector3 startPos;
	Sprite m_weaponSprite;

//	SpriteRenderer m_bulletRenderer;
	ParticleSystem m_bulletParticle;

	SoundPlayClass m_soundPlayer;
//
//	SpriteRenderer bulletRenderer{
//		get{
//			if (m_bulletRenderer == null)
//				m_bulletRenderer = GetComponent<SpriteRenderer> ();
//			return m_bulletRenderer;
//		}
//	}

	ParticleSystem bulletParticle{
		get{
			if (m_bulletParticle == null)
				m_bulletParticle = GetComponent<ParticleSystem> ();
			return m_bulletParticle;
		}
	}
			


//    public ICharacterInterface character { get { return m_character; } }
	public UICharacterClass characterCtrler{ get { return m_characterCtrler; } }
	ICharacterInterface character{ get { return characterCtrler.character; } }

    public Type type { get { return this.GetType(); } }
	public int damage { get { return m_damage; } set{m_damage = value; }}
	public float range { get { return m_range;}}

	//A0.8 관통여부 세분화
	public bool isPenetrate{get { return m_isPenetrate;	}set{ m_isPenetrate = value; }}
//	public bool typeBulletPenetrate{get { return m_typeBulletPenetrate;	}set{ m_typeBulletPenetrate = value; }}

	public bool isInTrench{ get { return m_isInTrench; } }
	public Sprite weaponSprite{ get { return m_weaponSprite; } }


	void Start(){
//		Debug.Log ("start : " + m_typeBullet.BaseType);
	}


	void setBulletSound(string key, bool is3DSound){
		SoundPlayClass soundPlayer = GetComponent<SoundPlayClass> ();
		if (soundPlayer != null)
			soundPlayer.audioPlay (key, TYPE_SOUND.EFFECT, is3DSound);
	}

	/// <summary>
	/// 발사
	/// </summary>
	/// <param name="character">Character.</param>
	/// <param name="equipment">Equipment.</param>
	/// <param name="accuracy">Accuracy.</param>
	public void attack(UICharacterClass characterCtrler, WeaponEquipmentClass equipment, float accuracy){
		m_characterCtrler = characterCtrler;

		m_moveSpeed = equipment.bulletSpeed;

		m_weaponSprite = equipment.equipIcon;

		m_mos = (characterCtrler == null) ? TYPE_MOS.DUALIST : character.mos;
		m_damage = (characterCtrler == null) ? equipment.damage : (int)character.addState.valueCalculator((float)equipment.damage, typeof(AttackAddStateClass));
		m_range = (characterCtrler == null) ? equipment.range :  character.addState.valueCalculator(equipment.range, typeof(RangeAddStateClass));
		m_team = (characterCtrler == null)  ? TYPE_TEAM.ENEMY : m_characterCtrler.team;
		m_isPenetrate = (characterCtrler == null) ? false : character.addState.isConstraint(TYPE_CONSTRAINT.PENETRATE);
//		typeBulletPenetrate= (characterCtrler == null) ? false : character.addState.isConstraint(TYPE_CONSTRAINT.PENETRATE);


		Debug.Log ("damage : " + m_damage);
		//연속 발사시 집탄율이 떨어지는 현상 보여야함
		//최대 10% 낮아짐
		accuracy = (m_characterCtrler == null) ? accuracy : character.addState.valueCalculator (accuracy, typeof(AccuracyAddStateClass));

		float accuracyRange = PrepClass.accuracyCalculator (accuracy, character.recoiling);

//		Debug.Log ("accuracyRange : " + accuracyRange);
		float accuracyOffset = UnityEngine.Random.Range (-accuracyRange, accuracyRange);

		//폭발탄버프가있으면 파티클 폭발가능
		//if(m_character.addState.

//		Debug.Log ("velocity : " + GetComponent<Rigidbody2D> ().velocity);


//		transform.eulerAngles = new Vector3(0f, 0f, character.angle + accuracyOffset);
		m_typeRange = equipment.typeRange;

		angleCalculator (character.angle, accuracyOffset);



		startPos = transform.position;

		transform.localScale *= (characterCtrler == null) ? 1f : character.addState.valueCalculator (1f, typeof(SizeAddStateClass));

		if(character.GetType() == typeof(PlayerClass))
			setBulletSound (equipment.attackSoundKey, false);
		else
			setBulletSound (equipment.attackSoundKey, true);

		switch (m_typeRange) {
		case TYPE_RANGE.LONG:
			GetComponent<Rigidbody2D> ().velocity = angleVelocity (character.angle + accuracyOffset) * m_moveSpeed;

			StartCoroutine (LongRangeWeaponLoop ());
			break;
		case TYPE_RANGE.SHORT:
			GetComponent<Rigidbody2D> ().velocity = Vector2.zero;



			setBulletSize ();

//			m_bulletParticle = GetComponent<ParticleSystem> ();
//
//			if (m_bulletParticle != null) {
//
//				var particleModule = m_bulletParticle.main;
//
//				particleModule.startSize = m_range * c_sizeOffset;
//
//				ParticleSystem[] childParticles = GetComponentsInChildren<ParticleSystem> ();
//				if (childParticles.Length > 0) {
//					foreach (ParticleSystem childParticle in childParticles) {
//						var childParticleModule = childParticle.main;
//						childParticleModule.startSize = m_range * c_sizeOffset;
//					}
//				}
//			} else {
//				transform.localScale *= m_range * c_sizeOffset;
//			}
				



			StartCoroutine (ShortRangeWeaponLoop ());
			break;
		}
			
	}

	/// <summary>
	/// 발사
	/// </summary>
	/// <param name="character">Character.</param>
	/// <param name="damage">Damage.</param>
	/// <param name="range">Range.</param>
	/// <param name="moveSpeed">Move speed.</param>
	/// <param name="accuracy">Accuracy.</param>
	public void attack(UICharacterClass characterCtrler, Sprite weaponImage, int damage, float range, float moveSpeed, float angle, float accuracy = 100f, bool isPenetrate = false){
		m_characterCtrler = characterCtrler;
		m_moveSpeed = moveSpeed;

		m_weaponSprite = weaponImage;
		m_mos = (characterCtrler == null) ? TYPE_MOS.DUALIST : character.mos;
		m_damage = (characterCtrler == null) ? damage : (int)character.addState.valueCalculator((float)damage, typeof(AttackAddStateClass));
		m_range = (characterCtrler == null) ? range : character.addState.valueCalculator(range, typeof(RangeAddStateClass));
		m_team = (characterCtrler == null) ? TYPE_TEAM.ENEMY : characterCtrler.team;
		m_isPenetrate = (isPenetrate) ? isPenetrate : ((characterCtrler == null) ? isPenetrate : character.addState.isConstraint(TYPE_CONSTRAINT.PENETRATE));
//		typeBulletPenetrate= (characterCtrler == null) ? false : character.addState.isConstraint(TYPE_CONSTRAINT.PENETRATE);

		accuracy = (characterCtrler == null) ? accuracy : character.addState.valueCalculator (accuracy, typeof(AccuracyAddStateClass));

		float accuracyRange = (characterCtrler == null) ? 0f : PrepClass.accuracyCalculator (accuracy, character.recoiling);
//		Debug.Log ("accuracyRange : " + accuracyRange);
		float accuracyOffset = UnityEngine.Random.Range (-accuracyRange, accuracyRange);

		GetComponent<Rigidbody2D> ().velocity = angleVelocity (angle + accuracyOffset) * m_moveSpeed;
		//		Debug.Log ("velocity : " + GetComponent<Rigidbody2D> ().velocity);
//		transform.eulerAngles = new Vector3(0f, 0f, angle + accuracyOffset);
		m_typeRange = TYPE_RANGE.LONG;

		angleCalculator (angle, accuracyOffset);
		startPos = transform.position;


		transform.localScale *= (m_characterCtrler == null) ? 1f : character.addState.valueCalculator (1f, typeof(SizeAddStateClass));

		//setBulletSound (equipment.attackSoundKey);

		switch (m_typeRange) {
		case TYPE_RANGE.LONG:
			StartCoroutine (LongRangeWeaponLoop ());
			break;
		}
	}


	void setBulletSize(){
		


		switch (m_typeRange) {
		case TYPE_RANGE.SHORT:

//			m_bulletParticle = GetComponent<ParticleSystem> ();

			if (bulletParticle != null) {

				var particleModule = bulletParticle.main;

				particleModule.startSize = m_range * c_sizeOffset;

				ParticleSystem[] childParticles = GetComponentsInChildren<ParticleSystem> ();
				if (childParticles.Length > 0) {
					foreach (ParticleSystem childParticle in childParticles) {
						var childParticleModule = childParticle.main;
						childParticleModule.startSize = m_range * c_sizeOffset;
					}
				}
			} else {
				transform.localScale *= m_range * c_sizeOffset;
			}

			BoxCollider2D collider = GetComponent<BoxCollider2D> ();
			if (collider != null) {
				collider.size *= m_range;
				collider.offset = new Vector2 (collider.size.x * 0.5f, collider.offset.y);
			}
			break;
		}
	}


	void angleCalculator(float angle, float accuracyOffset = 0f){

		switch (m_typeRange) {
		case TYPE_RANGE.SHORT:

			if (bulletParticle != null) {

				var particleModule = bulletParticle.main;

				particleModule.startRotationZ = (-angle + m_rotateOffset) * Mathf.Deg2Rad;
//				Debug.Log ("angle : " + angle);

				ParticleSystem[] childParticles = GetComponentsInChildren<ParticleSystem> ();
				if (childParticles.Length > 0) {
					foreach (ParticleSystem childParticle in childParticles) {
						var childParticleModule = childParticle.main;
						childParticleModule.startRotationZ = (-angle + m_rotateOffset) * Mathf.Deg2Rad;
					}
				}
			} 

			//transform.localScale *= m_range;
			break;
		}


		transform.eulerAngles = new Vector3(0f, 0f, angle + accuracyOffset);

	}


	IEnumerator ShortRangeWeaponLoop(){
		yield return new WaitForSeconds(PrepClass.c_timeGap * c_shortRangeWeaponTime);
		destroy ();
	}

	IEnumerator LongRangeWeaponLoop(){

//		Debug.Log ("bullet : " + GetInstanceID ());

		RaycastHit2D[] hits = Physics2D.CircleCastAll (transform.position, 0.1f, Vector2.zero);

//		Debug.Log ("hitsLength : " + hits.Length);
		//참호 안에서 탄환이 사격되면
		foreach (RaycastHit2D hit in hits) {
			if (hit.collider.tag == "TrenchTag") {
				m_isInTrench = true;
				//참호 안에서 사격. 참호안에서 피격되면 감소 무시
//				Debug.Log ("shootTrench");
				break;
			}
		}

		while(gameObject.activeSelf){

			if (m_isRotate) transform.Rotate (new Vector3 (0f, 0f, m_rotate));

			float dis = Mathf.Abs(Vector2.Distance(startPos, transform.position));
			if(range < dis){
				
				particleSystemSet(transform);
				break;
			}

			yield return new WaitForSeconds(PrepClass.c_timeGap);
		}

		destroy ();

	}


	protected Vector2 angleVelocity(float angle){
		return new Vector2 (Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
	}

	/// <summary>
	/// 탄환 파괴
	/// </summary>
	public void destroy(){
		Destroy (gameObject);
	}




	void OnTriggerEnter2D(Collider2D col){
		//Debug.Log ("col");
		try{






			if (PrepClass.isCharacterTag(col.tag)) {
//				Debug.Log("피격 : " + damage + " - " + col.GetComponent<ICharacterInterface>().mos);

				//피격 당함
				//usebuff(hit)를 이미 실행
				//Debug.Log("bullet : " + this + " " + characterCtrler);

				ICharacterInterface enemyCharacter = col.GetComponent<ICharacterInterface> ();

				if (enemyCharacter.hitAction (m_team, this)){

					//null 에러
					//제어자에게 직접 삽입




					if(m_characterCtrler != null){
						characterCtrler.addReport(damage, m_mos, TYPE_REPORT.DMG_SET);

						//character.addState.useBuff(character, TYPE_BUFF_STATE_ACT.ATTACK);
					}

					//적 위치
					particleSystemSet(col.transform);

					//사용자 버프 공격 읽기
					//적이 피격되었을 때 발동

					//공격자의 탄이 적에 닿으면
//					Debug.Log("useBuff");

					//분석 필요 - 오류가 날 수 있음
					if(characterCtrler != null){
						if(characterCtrler.character.useBuff(enemyCharacter, this, TYPE_BUFF_STATE_ACT.ATTACK)){
							
						}
					}
//					if(m_character.useBuff(m_character, this, TYPE_BUFF_STATE.ATTACK)){
//						BuffDataClass buffData = m_character.addState.getBuff(typeof(SuppressiveFireBuffDataClass));
//						if(buffData != null){
//							col.gameObject.GetComponent<ICharacterInterface>().buffAdd();
//						}
//					}



					//공격에 성공했을 때 디버프 걸기

					destroyBullet(m_typeRange);
				}
			}
			else if(col.tag == "Wall" || col.tag == "Object"){


				//탄환 위치
				particleSystemSet(col.transform);

				destroyBullet(m_typeRange);

			}
			else if(col.tag == "ActObject"){
				
				if(col.transform.parent.GetComponent<BuildingObjectClass> ().hitAction (m_team, this)){
					//탄환 위치
					particleSystemSet(col.transform);
					destroyBullet(m_typeRange);
				}
			}

		}
		catch(UnityException e){
			Debug.Log("탄환 에러 : " + e.Message);
		}

	}


	void OnTriggerExit2D(Collider2D col){
		//참호 밖에서  사격 - false
		//참호에 들어감 - false
		//참호 밖으로 한번 나가면 - false
		//참호 안이면 - true;

		if(col.tag == "TrenchTag"){
			m_isInTrench = false;
//			if(m_isInTrench) m_isInTrench = false;
//			Debug.Log ("isTrench : " + isInTrench + " " + GetInstanceID());
		}
	}


	void destroyBullet(TYPE_RANGE m_typeRange){
		switch (m_typeRange) {
		case TYPE_RANGE.LONG:
			if (!isPenetrate) {
				//destroy ();

//				if (bulletRenderer != null)
//					bulletRenderer.enabled = false;

				if (bulletParticle != null) {
					bulletParticle.Clear ();
					bulletParticle.Stop ();
				}



				GetComponent<Collider2D> ().enabled = false;
			}
			break;
		}
	}


	void particleSystemSet(Transform target){
		if (m_hitParticle != null) {

//			int layerMask = 1 << 18;
//			layerMask = ~layerMask;

			Debug.Log ("particle");

			if (bulletParticle.isPlaying) {

				ParticleSystem hitParticle = Instantiate (m_hitParticle);

				switch (m_typeRange) {
				case TYPE_RANGE.LONG:
				//탄환 위치
					hitParticle.transform.position = transform.position;
					break;
				case TYPE_RANGE.SHORT:
				//근거리일 경우 레이를 쏴서 맞는 곳 중심으로 위치 생성
				//피격자와 동일해야 함

					float x = Mathf.Cos (character.angle * Mathf.Deg2Rad);
					float y = Mathf.Sin (character.angle * Mathf.Deg2Rad);

					RaycastHit2D[] rays = Physics2D.RaycastAll (character.transform.position, new Vector2 (x, y), m_range);
//				Debug.Log ("rays : " + rays.Length);
//				Debug.Log("pos : " + target.transform.position);
//				Debug.DrawLine (transform.position, target.gameObject.transform.position, Color.cyan);
					Vector2 hitPoint = transform.position;

					foreach (RaycastHit2D ray in rays) {
//					Debug.Log ("instance : " + ray.collider.tag + " " + target.tag);
						if (ray.collider.tag == target.tag) {//gameObject == target.gameObject){
							hitPoint = ray.centroid;
//						Debug.Log ("targetTransform : " + hitPoint);
							break;
						}
					}

					if (PrepClass.isCharacterTag (target.tag)) {
						hitParticle.transform.position = target.position;//hitPoint;
						hitParticle.transform.SetParent (target);
					} else {
						hitParticle.transform.position = hitPoint;//transform.position;//hitPoint;
					}
					break;
				}




				ParticleLifeClass particleLifeCycle = hitParticle.GetComponent<ParticleLifeClass> ();

				if (particleLifeCycle == null) {
					particleLifeCycle = hitParticle.gameObject.AddComponent<ParticleLifeClass> ();
				}



				if (characterCtrler != null) {
					///리팩토링 필요
					BuffDataClass buff = character.addState.getBuff (typeof(AmplifyingBuffDataClass));
					if (buff != null) {
//				Debug.Log ("buff : " + buff.GetType ());
						int damage = (int)((float)character.mosData.weapon.damage * ((AmplifyingBuffDataClass)buff).damagePercent);
						float range = ((AmplifyingBuffDataClass)buff).explosiveRange;
						//particle.GetComponent<ParticleLifeClass> ().particleExplosiveSet (characterCtrler, damage, range, true, weaponSprite);
						particleLifeCycle.setParticleCircle (characterCtrler, weaponSprite, damage, 1f, range);
					}
				}

			}
		}
	}









}
