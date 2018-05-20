using System;
using UnityEngine;

public class ParticleAttackClass : MonoBehaviour, IBullet
{

	[SerializeField] ParticleSystem m_hitParticle;
	[SerializeField] bool m_isRotate;

	ParticleSystem m_particle;
	Collider2D m_collider;


	UICharacterClass m_characterCtrler;
	int m_damage;
	float m_radius;


	float m_time = 0f;
	float m_delayTime = 0f;

	Vector2 m_size;

	Sprite m_weaponSprite;

	ICharacterInterface character { get{ return m_characterCtrler.character; } }
	public UICharacterClass characterCtrler { get{ return m_characterCtrler; } }
	public Type type { get{return this.GetType (); } }
	public bool isPenetrate{ get{return false;} set{}}
	public int damage{ get{return m_damage;} set{}}
	public bool isInTrench{ get { return true; } }
	public Sprite weaponSprite{ get {return m_weaponSprite; } }
	TYPE_TEAM team {
		get {
			if (characterCtrler != null && character != null) {
				return character.team;
			}
			return TYPE_TEAM.ENEMY;
		}
	}

//
//	void Update(){
//		if (m_delayTime > 0f) {
//			m_time += Time.deltaTime;
//			if (m_time > m_delayTime) {
//				m_collider.enabled = true;
//			}
//		}
//	}

	public bool setParticleCircle(UICharacterClass characterCtrler, Sprite weaponSprite, int damage, float radius, float scale){

		m_characterCtrler = characterCtrler;


		try{

				
			//delayTime();

			m_collider = setCollider(typeof(CircleCollider2D)); //GetComponent<CircleCollider2D> ();
//
//			if (m_collider == null) {
//				m_collider = gameObject.AddComponent<CircleCollider2D> ();
//				m_collider.isTrigger = true;
//				m_collider.enabled = false;
//			}


			m_damage = damage;
			m_radius = radius;
			m_weaponSprite = weaponSprite;





			if(m_collider != null)
				((CircleCollider2D)m_collider).radius = radius;

			if(characterCtrler != null && characterCtrler.character != null)				
				transform.localScale *= character.addState.valueCalculator (scale, typeof(ExplosiveRangeAddStateClass));

	//		m_particle = GetComponent<ParticleSystem> ();

			return true;
		}
		catch (UnityException e){
			Debug.LogError ("particleAttackClass Circle : " + e.Message);
			return false;
		}
	}


	public bool setParticleBox(UICharacterClass characterCtrler, Sprite weaponSprite, int damage, float scale, float angle){



		m_characterCtrler = characterCtrler;

		Debug.Log ("setBox : " + m_characterCtrler);


		try{

			//delayTime();

			m_collider = setCollider(typeof(BoxCollider2D));// GetComponent<BoxCollider2D> ();


//			if (m_collider == null) {
//				m_collider = gameObject.AddComponent<BoxCollider2D> ();
//				m_collider.isTrigger = true;
//				m_collider.enabled = false;
//			}

			m_damage = damage;
			m_weaponSprite = weaponSprite;

			transform.eulerAngles = new Vector3 (0f, 0f, angle);

			m_particle = GetComponent<ParticleSystem> ();

			if (m_particle != null) {
				var particle = m_particle.main;
				particle.startRotation = -angle * Mathf.Deg2Rad;
			}

			if(characterCtrler != null && characterCtrler.character != null)				
				transform.localScale *= character.addState.valueCalculator (scale, typeof(ExplosiveRangeAddStateClass));
			
//			transform.localScale *= character.addState.valueCalculator (scale, typeof(ExplosiveRangeAddStateClass));
			return true;
		}
		catch (UnityException e){
			Debug.LogError ("particleAttackClass Box : " + e.Message);
			return false;
		}
	}

	public bool setParticleBox(UICharacterClass characterCtrler, Sprite weaponSprite, int damage, float x, float y, bool isOffsetRevX, bool isOffsetRevY, float scale, float angle){


		setParticleBox (characterCtrler, weaponSprite, damage, scale, angle);


//		m_characterCtrler = characterCtrler;
//
//		Debug.Log ("setBox : " + m_characterCtrler);


		try{
//			m_collider = GetComponent<BoxCollider2D> ();
//
//
//			if (m_collider == null) {
//				m_collider = gameObject.AddComponent<BoxCollider2D> ();
//				m_collider.isTrigger = true;
//			}
//
//			m_damage = damage;
//			m_weaponSprite = weaponSprite;
//
//			transform.eulerAngles = new Vector3 (0f, 0f, angle);
//
//			m_particle = GetComponent<ParticleSystem> ();
//
//			if (m_particle != null) {
//				var particle = m_particle.main;
//				particle.startRotation = -angle * Mathf.Deg2Rad;
//			}


			delayTime();

			if(m_collider != null){
				((BoxCollider2D)m_collider).size = new Vector2 (x, y);

				if(isOffsetRevX) x *= -1f;
				if(isOffsetRevY) y *= -1f;

				((BoxCollider2D)m_collider).offset = new Vector2(x * 0.5f, 0f);
			}


//			transform.localScale *= character.addState.valueCalculator (scale, typeof(ExplosiveRangeAddStateClass));
			return true;
		}
		catch (UnityException e){
			Debug.LogError ("particleAttackClass Box : " + e.Message);
			return false;
		}
	}


	void delayTime(){
		
		ParticleSystem objParticle = GetComponent<ParticleSystem>();

		if(objParticle != null){
			m_delayTime = objParticle.main.startDelay.constant;
//			Debug.LogWarning ("delay : " + m_delayTime);
		}
	}


	Collider2D setCollider(Type colliderType){
		Collider2D collider = GetComponent (colliderType) as Collider2D;

		if (collider == null) {
			collider = gameObject.AddComponent(colliderType) as Collider2D;
			collider.isTrigger = true;

		}
//		if(m_delayTime > 0f) collider.enabled = false;
//		else collider.enabled = true;

		return collider;
	}


	void OnTriggerEnter2D(Collider2D col){

//		Debug.LogWarning ("setCollider");

		if (PrepClass.isCharacterTag (col.tag)) {

			ICharacterInterface enemyCharacter = col.GetComponent<CharacterCommonClass> ();

			if (team != enemyCharacter.team) {
				if (!enemyCharacter.isDead) {
					enemyCharacter.hitAction (team, this);
					particleSet (enemyCharacter);
				}
			}
		} else if (col.tag == "ActObject") {
			if (col.transform.parent != null) {
				col.transform.parent.GetComponent<BuildingObjectClass> ().hitAction (TYPE_TEAM.ENEMY, this);
			}
		}
	}


	/// <summary>
	/// 파티클 정의
	/// </summary>
	/// <param name="character">Character.</param>
	void particleSet(ICharacterInterface character){
		if (m_hitParticle != null) {
			Instantiate (m_hitParticle, character.transform.position, Quaternion.identity).transform.SetParent (character.transform);
		}
	}

	//사용하지 않음
	//isTrigger가 false여야 이벤트 발생
	//	void OnCollisionEnter2D(Collision2D col){
	//
	//
	//		if (!isExplosive) return;
	//
	//		Debug.Log ("파티클 : " + col.collider.name);
	//
	//
	//		if (PrepClass.isCharacterTag(col.collider.tag)) {
	//			if (col.gameObject.GetComponent<ICharacterInterface> ().hitAction (m_characterCtrler.team, this)){
	//				//사용자 버프 공격 읽기
	//				character.useBuff(character, this, TYPE_BUFF_STATE_ACT.ATTACK);
	//			}
	//		}
	//
	//
	//	}

	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere  (transform.position, m_radius);
	}
}


