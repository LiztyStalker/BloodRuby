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
using System.Linq;

public enum TYPE_SKILL{PASSIVE, ENEMY, ALLY, FLAG}

/// <summary>
/// 스킬 작동 타입
/// PLAYER 플레이어만
/// AREA 범위
/// TARGET 표적
/// SET 설치형
/// ARROW 방향
/// </summary>
public enum TYPE_SKILL_POS{
	NONE,  //없음
	AREA_TARGET, //범위 선택
	TARGET, //표적
	BUILD, //설치형
	ARROW, //방향
	AREA_SELF //자기 범위
}

public abstract class SkillClass : MonoBehaviour, IContentView{

	//protected ICharacterInterface m_player;

	TextInfoClass m_textInfo;

	[SerializeField] string m_key;
	[SerializeField] string m_name;
	[SerializeField] TYPE_MOS m_mos;
	[SerializeField] TYPE_SKILL m_typeSkill;
	[SerializeField] Sprite m_skillIconRect;
	[SerializeField] Sprite m_skillIconRound;
	[SerializeField] float m_coolTime;
	[SerializeField] string m_contents;
//	[SerializeField] string m_soundEffectKey;

	[SerializeField] TYPE_SKILL_POS m_typeSkillPos;
	[SerializeField] Sprite m_guideLineSprite;
	[SerializeField] Sprite m_areaSprite;
	[SerializeField] float m_guideLineRange;
	[SerializeField] bool m_isAnimationEvent;
	[SerializeField] ParticleSystem m_castingParticle;
//	[SerializeField] bool m_isTargetRun;
	//목표가 없음
	[SerializeField] bool m_isNoneTarget = false;
	bool m_isAlly;
	bool m_isMyself;
	bool m_isDead;

	TYPE_BUFF_STATE m_typeSkillState;


//	protected float m_guideLineRange;

	SkillBuffClass m_skillBuffData;


	protected SkillBuffClass skillBuffData {
		get {
			if(m_skillBuffData == null)
				m_skillBuffData = GetComponent<SkillBuffClass> ();
			return m_skillBuffData;
		}
	}
	public string key{get{return textInfo.key;}}
	public string name{ get { return textInfo.name; } }// m_name; } }
	public Sprite iconRect{get{return m_skillIconRect;}}
	public Sprite iconRound{get{return m_skillIconRound;}}
	public TYPE_SKILL typeSkill{get{return m_typeSkill;}}
	public float coolTime{get{return m_coolTime;}}
	public float time{get{return coolTime;}}


	public TYPE_SKILL_POS typeSkillPos{ get { return m_typeSkillPos; } }
	public Sprite guideLineSprite{ get { return m_guideLineSprite; } }
	public Sprite areaSprite{ get { return m_areaSprite; } }
	public float guideLineRange{ 
		get 
		{ 
			if(skillBuffData != null && skillBuffData.getBuffData () != null)
				return skillBuffData.getBuffData ().range;
			if (m_guideLineRange > 0f)
				return m_guideLineRange;
			return -1;
//			return m_guideLineRange; 
		} 
	}
	public string contents{ get { return textInfo.contents; } }// m_contents;}}
//	public string soundEffectKey{ get { return m_soundEffectKey; } }
	public bool isAnimationEvent{ get { return m_isAnimationEvent; } }

	public bool isAlly{ get { return m_isAlly; } }
	public bool isMyself{ get { return m_isMyself; } }
	public bool isDead{ get {  return m_isDead; } }
	public bool isNoneTarget{ get { return m_isNoneTarget; } }

	TextInfoClass textInfo{ 
		get { 
			if (m_textInfo == null) {
				m_textInfo = TextInfoFactoryClass.GetInstance.getTextInfo (gameObject.name);
				Debug.Log("text : " +  m_textInfo);
			}
			return m_textInfo; 
		} 
	}

	protected ParticleSystem castingParticle{get{return m_castingParticle;}}

	public TYPE_BUFF_STATE typeSkillState
	{
		get
		{
			if (skillBuffData != null && skillBuffData.getBuffData() != null)
				return skillBuffData.getBuffData ().buffState;
			return TYPE_BUFF_STATE.CONTINUE;
		}
	}


	protected void setParticle(Vector3 pos, Transform parent = null){
		if (castingParticle != null) {
			ParticleSystem particle = Instantiate (castingParticle, pos, Quaternion.identity);
//			SoundPlayClass particleSound = particle.GetComponent<SoundPlayClass> ();
			if (parent != null)
				particle.transform.SetParent (parent);
		}
	}

	/// <summary>
	/// 스킬 초기화 - 패시브 스킬 작동
	/// </summary>
	/// <param name="player">Player.</param>
	public virtual void initSkill (ICharacterInterface player){

		try{
			if(skillBuffData.getBuffData() != null){
				m_isAlly = skillBuffData.getBuffData().isAlly;
				m_isDead = skillBuffData.getBuffData().isDead;
				m_isMyself = skillBuffData.getBuffData().isMyself;
			}
		}
		catch{
			m_skillBuffData = null;
		}

		Debug.Log ("skillBuff" + m_skillBuffData);
	}

	/// <summary>
	/// 스킬 발동 - 액티브 스킬 작동
	/// </summary>
	/// <param name="player">Player.</param>
	public virtual bool skillAction (ICharacterInterface player){

		if (skillBuffData == null)
			initSkill (player);

		return true;
	}

	public virtual void skillGuideLine(ICharacterInterface player){
		player.setSkillGuideLine (this);
	}

	public virtual BuffDataClass getBuffData(){
		if (skillBuffData == null) return null;
		return skillBuffData.getBuffData();
	}



	/// <summary>
	/// 목표 가져오기
	/// </summary>
	/// <returns>The target.</returns>
	/// <param name="typeSkillPos">Type skill position.</param>
//	protected ICharacterInterface getTarget(ICharacterInterface character, TYPE_TEAM team){
//		//팀이 같으면 아군과 본인용, 다르면 적군용
//		return character.skillTarget; //getTarget (character, character.team.CompareTo(team));
//	}



//	ICharacterInterface getTarget(ICharacterInterface character, int isEnemy){
//
//
//		Debug.Log ("skillRange : " + m_guideLineRange);
//		RaycastHit2D[] hits = Physics2D.CircleCastAll (character.transform.position, m_guideLineRange, Vector2.zero);
//
////		Debug.Log ("hits : " + hits.Length);
//
//		foreach (RaycastHit2D hit in hits) {
//
//			if (PrepClass.isCharacterTag (hit.collider.tag)) {
//
//				//자신이면 재루프
//				if (hit.collider.GetComponent<ICharacterInterface> () == character)
//					continue;
//
//
////				Debug.Log ("distance : " + Vector2.Distance (character.transform.position, hit.transform.position));
////
////				if(m_guideLineRange != 0f)
////					if(m_guideLineRange < Vector2.Distance (character.transform.position, hit.transform.position))
////						continue;
//
//
//				//에어리어 타겟 - 적일시 적방향으로, 아군일시 아군 방향으로 - 망원경
//
//
//				//플레이어 - 플레이어 중심으로 적일시 적, 아군일시 아군
//
////			switch (m_typeSkillPos) {
////			case TYPE_SKILL_POS.PLAYER:
//
//				if (isEnemy == 0) {
//					if (hit.collider.GetComponent<ICharacterInterface> ().team == character.team) {
//						if (!hit.collider.GetComponent<ICharacterInterface> ().isDead) {
//							RaycastHit2D[] rays = Physics2D.RaycastAll (character.transform.position, hit.transform.position - character.transform.position);
//
//							foreach (RaycastHit2D ray in rays) {
//								if (ray.collider.tag == "Character") {
//
//									if (ray.collider.GetComponent<ICharacterInterface> () == character)
//										continue;
//
//									return ray.collider.GetComponent<ICharacterInterface> ();
//								} else if (ray.collider.tag == "ActObject") {
//									continue;
//								} else if (ray.collider.tag == "Wall" || ray.collider.tag == "Object") {
//									continue;
//
//								}
//
//							}
//						}
//					}
//				} else {
//					if (hit.collider.GetComponent<ICharacterInterface> ().team != character.team) {
//						if (!hit.collider.GetComponent<ICharacterInterface> ().isDead) {
//						
//							RaycastHit2D[] rays = Physics2D.RaycastAll (character.transform.position, hit.transform.position - character.transform.position);
//
//							foreach (RaycastHit2D ray in rays) {
//								if (ray.collider.tag == "Character") {
//
//									if (ray.collider.GetComponent<ICharacterInterface> () == character)
//										continue;
//							
//									return ray.collider.GetComponent<ICharacterInterface> ();
//								} else if (ray.collider.tag == "ActObject") {
//									continue;
//								} else if (ray.collider.tag == "Wall" || ray.collider.tag == "Object") {
//									continue;
//
//								}
//							}
//						}
//					}
//				}
//			}
//
//
////				break;
////			case TYPE_SKILL_POS.AREA:
////
////
////
////				break;
////			case TYPE_SKILL_POS.TARGET:
////
////
////
////
////				break;
//		}
//
//
//
//
//	
//
//
//
//		return null;
//	}




}


