using UnityEngine;
using System;
using System.Collections;
using System.Linq;
using Spine.Unity;

public abstract class MOSDataClass //: IMOSInterface
{




	ICharacterInterface m_character;
	UICharacterClass m_characterCtrler;
	//병과
	MOSClass m_mosData;

	//장비슬롯
	IEquipmentInterface[] m_equipements = new EquipmentClass[3];

	MOSSkeletonClass m_MOSSkeleton = null;

	//몸 애니메이션 상태
	TYPE_ANIMATION m_mosBodyAnimation;

	//다리 애니메이션 상태
	//TYPE_ANIMATION m_mosLegAnimation;





	//	bool m_isAttack = false; //true : 공격중
	//	bool m_isTelescope = false; //true : 망원경중
	//	bool m_isReload = false; //장전중
	//	int m_slot = 0;
	bool m_isMove = false; //true : 이동중
	float m_magnitude = 0f; // 이동상태
	string m_animationExtend = "";
	int m_activeSlot = 0; //가동 스킬 슬롯
	int m_toggleSlot = -1;

	protected TYPE_BUFF_STATE m_typeSkillState;



	public MOSClass mosData{ get { return m_mosData; } }
	protected MOSSkeletonClass mosSkeleton {get {return m_MOSSkeleton;}}



	public Vector2 shootPos{ get { return mosSkeleton.shootPos; } }
	public int health { get { return mosData.health + armor.health; } }
	public float moveSpeed { get { return mosData.speed + armor.moveSpeed; } }
	public IEquipmentInterface[] equipments{ get { return m_equipements; } }
	public float shootDelay{ get { return ((WeaponEquipmentClass)m_equipements [0]).shootDelay; } }

	public Sprite[] skillRectIcons{ get { return mosData.skillActions.Select(skilldata => skilldata.iconRect).ToArray<Sprite>(); } }
	public Sprite[] skillRoundIcons{ get { return mosData.skillActions.Select(skilldata => skilldata.iconRound).ToArray<Sprite>(); } }
	public SkillClass[] skillData{ get { return mosData.skillActions; } }

	public TYPE_MOS mos { get { return mosData.mos; } }
	public ICharacterInterface character{ get { return m_character; } }
	public UICharacterClass characterCtrler{ get { return m_characterCtrler; } protected set { m_characterCtrler = value; } }


	public TYPE_ANIMATION mosAnimation{ get { return m_mosBodyAnimation; } set { m_mosBodyAnimation = value; } }
//	protected TYPE_ANIMATION mosBodyAnimation{ get { return m_mosBodyAnimation; } set { m_mosBodyAnimation = value; } }
//	protected TYPE_ANIMATION mosLegAnimation{ get { return m_mosLegAnimation; } set { m_mosLegAnimation = value; } }


	public WeaponEquipmentClass weapon{ get { return (WeaponEquipmentClass)m_equipements [(int)TYPE_EQUIPMENT.WEAPON]; } set { m_equipements [(int)TYPE_EQUIPMENT.WEAPON] = value;} }
	public ArmorEquipmentClass armor{get{return (ArmorEquipmentClass)m_equipements [(int)TYPE_EQUIPMENT.ARMOR];}}
//	public WeaponEquipmentClass accessory{get{return (WeaponEquipmentClass)m_equipements [(int)TYPE_EQUIPMENT.ACCESSERY];}}


	public int activeSlot{ get { return m_activeSlot; } protected set { m_activeSlot = value; } } //가동 스킬 슬롯
	public int toggleSlot{ get { return m_toggleSlot; } protected set { m_toggleSlot = value; } } //가동 스킬 슬롯

	protected string animationExtend{get{ return m_animationExtend; } set{m_animationExtend = value;}}
	protected bool isMove{ get { return m_isMove; } set { m_isMove = value; } }

	int getEquipType(TYPE_EQUIPMENT equip){return (int)equip;}


	/// <summary>
	/// 게임 준비 - 병과 초기화
	/// </summary>
	/// <param name="character">Character.</param>
	/// <param name="equipementSlots">Equipement slots.</param>
	public virtual void gameReady(ICharacterInterface character, int[] equipementSlots){
		//병과, 무기 가져오기

		m_character = character;

		m_mosData = MOSFactoryClass.GetInstance.getMOS (character.mos);

		if (m_mosData == null)
			Debug.LogError ("병과 데이터 가져오기 실패 : " + character.mos);


		foreach (TYPE_EQUIPMENT typeEquipment in Enum.GetValues(typeof(TYPE_EQUIPMENT))) {

			Debug.Log ("typeEquip : " + typeEquipment);

			m_equipements [getEquipType(typeEquipment)] = 
				EquipmentFactoryClass.GetInstance.getEquipment (
					mosData.mos, 
					typeEquipment, 
					equipementSlots[getEquipType(typeEquipment)]
				);			
		}


//		if(m_equipements [getEquipType(TYPE_EQUIPMENT.WEAPON)] == null)
//			Debug.LogError ("무기 데이터 가져오기 실패 : " + character.mos + " " + equipementSlots[getEquipType(TYPE_EQUIPMENT.WEAPON)]);
		
		//무기와 갑옷은 디폴트, 악세사리는 없음
		//m_mosData

		//스킬은 병과대로
		//m_skillAction = mosData.skillActions;
		//스킬 초기화




		m_MOSSkeleton = (MOSSkeletonClass)MonoBehaviour.Instantiate (mosData.MOSSkeleton, m_character.transform.position, new Quaternion());
		//스켈레톤 초기화
		if (mosSkeleton != null) {


			mosSkeleton.initAnimation (this);

			//스켈레톤 상태 이벤트 등록
			mosSkeleton.skeletonAnimationState.Event += actionEvent;
			mosSkeleton.skeletonAnimationState.Complete += endEvent;

			//스켈레톤 위치 등록
			m_character.setSkeletonAnimation (mosSkeleton);

			//대기상태 전환
			idleAction ();
		} else {
			Debug.Log ("스켈레톤 생성 불가 : " + character.mos);
		}



		initSkillAction(character);
		initItemAction (character);

	}

	/// <summary>
	/// 스킬 쿨타임 가져오기
	/// </summary>
	/// <returns>The skill cool time.</returns>
	/// <param name="slot">Slot.</param>
	public float getSkillCoolTime(int slot){
		return m_character.addState.valueCalculator (skillData[slot].coolTime, typeof(CoolTimeAddStateClass));
	}

	/// <summary>
	/// 공격 액션
	/// </summary>
	/// <param name="shootPos">Shoot position.</param>
	/// <param name="team">Team.</param>
	/// <param name="angle">Angle.</param>
	public virtual void attackAction(UICharacterClass characterCtrler, Vector3 shootPos, float time){
		//공격 애니메이션
		//무기에서 탄환 공격
		//쿨타임 진행
//		Debug.Log("weapon : " + weapon.name);
		this.characterCtrler = characterCtrler;

		//공격 등록 - 스켈레톤 애니메이션 실행
		//스켈레톤 애니메이션에서 이벤트 발생
		//이벤트 실행
		//weapon.attackAction (characterCtrler, shootPos);

		if (isUseSkill ())
			return;

		if (mosAnimation == TYPE_ANIMATION.RELOAD)
			return;

		mosAnimation = TYPE_ANIMATION.ATTACK;
		setBodyAnimation(mosData.getAnimationKey (TYPE_ANIMATION.ATTACK), false, time);

		if (m_isMove)
			mosSkeleton.addBodyAnimation (getNameKey (mosData.getAnimationKey (TYPE_ANIMATION.MOVE)), 1f);
		else
			mosSkeleton.addBodyAnimation (getNameKey (mosData.getAnimationKey (TYPE_ANIMATION.IDLE)), 1f);		
	}

	/// <summary>
	/// 대기 상태
	/// </summary>
	protected virtual void idleAction(){

		if (isUseSkill ())
			return;

		switch (mosAnimation) {
		case TYPE_ANIMATION.RELOAD:
			return;
		case TYPE_ANIMATION.ATTACK:
			setLegAnimation (mosData.getAnimationKey (TYPE_ANIMATION.IDLE), true, 1f);
			return;
		}

		mosAnimation = TYPE_ANIMATION.IDLE;
		setLegAnimation (mosData.getAnimationKey(mosAnimation), true, 1f);
		setBodyAnimation (mosData.getAnimationKey(mosAnimation), true, 1f);

	}

	/// <summary>
	/// 사망 상태
	/// </summary>
	/// <param name="time">Time.</param>
	public virtual void deadAction(float time){
		mosAnimation = TYPE_ANIMATION.DEAD;
		mosSkeleton.skeletonAnimationState.ClearTracks ();
		setAnimation (mosData.getAnimationKey(mosAnimation), false, false, time, time);
	}

	/// <summary>
	/// 이동 상태
	/// </summary>
	/// <param name="magnitude">Magnitude.</param>
	/// <param name="time">Time.</param>
	public virtual void moveAction(float magnitude, float time){
		//이동 애니메이션
//		Debug.Log("magnitude : " + magnitude);

		if (isUseSkill ())
			return;

		m_magnitude = magnitude;

		if (m_magnitude > 0.1f && !isMove) {
			isMove = true;
			setLegAnimation (mosData.getAnimationKey (TYPE_ANIMATION.MOVE), true, time);
		} else if(m_magnitude <= 0.1f && isMove) {
			isMove = false;
			setLegAnimation (mosData.getAnimationKey (TYPE_ANIMATION.IDLE), true, time);
		}

	}

	/// <summary>
	/// 장전 상태
	/// </summary>
	/// <param name="time">Time.</param>
	public virtual void reloadAction(float time){
		//재장전 애니메이션

		if (isUseSkill ())
			return;

		mosAnimation = TYPE_ANIMATION.RELOAD;
		setBodyAnimation (mosData.getAnimationKey(mosAnimation), false, time);		


	}

	/// <summary>
	/// 피격 상태
	/// </summary>
	/// <param name="time">Time.</param>
	public virtual void hitAction(float time){
		//피격 애니메이션
	}

	/// <summary>
	/// 부활
	/// </summary>
	/// <param name="time">Time.</param>
	public virtual void rebirthAction(float time){
		//부활 애니메이션
		idleAction();
		initSkillAction(character);
	}


	/// <summary>
	/// 스킬 상태
	/// </summary>
	/// <param name="slot">Slot.</param>
	public virtual bool skillAction(ICharacterInterface player, int slot, float time){
//		Debug.Log ("스킬 : " + m_skillAction [slot].name);
		if (skillData [slot] != null) {
			Debug.Log("스킬 발동 : " + skillData[slot].name);

			//스킬이 적의 유무를 판단하는가
			//적의 유무를 판단할 때 적이 없으면 스킬 쿨타임 안 돔
			//적을 선택해야하는 메시지 출력

			//스킬 선택
			mosAnimation = TYPE_ANIMATION.SKILL0 + slot;

			//가이드라인 끄기
			player.closeSkillGuideLine ();





			//애니메이션이 있으면
			if (isAnimation (mosData.getAnimationKey (mosAnimation))) {
				//애니메이션 실행
				setAnimation (mosData.getAnimationKey (mosAnimation), false, false, time, time);

				//애니메이션 내에 있는 이벤트에 맞춰 행동
				if (skillData [slot].isAnimationEvent) {
					//있으면 스킬 슬롯 저장
					activeSlot = slot;
					return true;//skillData [slot].typeSkillState;
				}
				//애니메이션은 있으나 이벤트는 없어 즉시 시전
				else {
					return skillRun (player, slot);
				}
			} else {
				return skillRun (player, slot);
			}




			//애니메이션을 찾아보기 - 예정
			//없으면 즉시 시전
			//있으면 스킬 시전 후 애니메이션 시전 또는 애니메이션 도중 시전

			//애니메이션이 실행되는지 확인
//			if (!setAnimation (mosData.getAnimationKey (mosAnimation), false, false, time, time)) {
//				return skillRun (player, slot);
//			} 
//			//애니메이션이 있음
//			else {
//				//애니메이션 내에 있는 이벤트에 맞춰 행동
//				if (skillData [slot].isAnimationEvent) {
//					//있으면 스킬 슬롯 저장
//					activeSlot = slot;
//					return true;//skillData [slot].typeSkillState;
//				}
//				//애니메이션은 있으나 이벤트는 없어 즉시 시전
//				else {
//					return skillRun (player, slot);
//				}
//			}
		} 

//		Debug.Log ("typeState : " + m_typeSkillState);

		return false;
	}


	/// <summary>
	/// 스킬 시전
	/// </summary>
	/// <param name="player">Player.</param>
	/// <param name="slot">Slot.</param>
	protected virtual bool skillRun(ICharacterInterface player, int slot){
		Debug.Log ("SkillRun : " + slot);


		//스킬이 발동되지 않았으면
		//현재 토글버프 등록

		if (!skillData [slot].skillAction (player)) {
			mosAnimation = TYPE_ANIMATION.IDLE;
			return false;
		}

		m_typeSkillState = skillData [slot].typeSkillState;
		//스킬 즉시 사용 - 버프 걸림
		//토글형이면
			//버프가 걸려있으면 - 토글 활성화
			//버프가 걸려있지 않으면 - 토글 취소
		//토글형이 아니면
			//즉시 시전

		//Debug.Log ("typeSkillState : " + m_typeSkillState);

		//스킬 사운드 실행
//		character.soundPlayer.audioPlay (skillData [slot].soundEffectKey, TYPE_SOUND.EFFECT);


		//토글형이면
		if (m_typeSkillState == TYPE_BUFF_STATE.TOGGLE) {
			//



			//스킬 실행 후 다른 토글이 있으면


			//스킬 실행 후 버프가 있으면 토글 애니메이션 등록
			//토글형
			if (player.addState.getBuff (skillData [slot].getBuffData ().GetType ()) != null) {


				Debug.Log ("Toggle : " + toggleSlot);

				//전 토글이 걸려있으면
				if (toggleSlot != -1) {
					//토글 버프가 걸려있으면

					Debug.Log ("Toggle : " + player.addState.getBuff (skillData [toggleSlot].getBuffData ().GetType ()));

					if (player.addState.getBuff (skillData [toggleSlot].getBuffData ().GetType ()) != null) {
						//전 토글버프 해제


						skillData [toggleSlot].skillAction (player);
						Debug.Log ("Skill");

						//전 토글 쿨타임
						player.resetSkillCoolTime (toggleSlot);

						//토글슬롯 변경
						//activeSlot = slot;
						//toggleSlot = slot;
						animationExtend = "";
					}
				} 
				//토글이 걸려있지 않으면

//				switch(toggleSlot){
//				case 0:
//					break;
//				case 1:
//					if (player.addState.getBuff (typeof(CoverBuffDataClass)) != null) {
//						activeSlot = 2;
//						skillData [activeSlot].skillAction (player);
//						return true;//TYPE_BUFF_STATE.CONTINUE;
//					}
//					break;
//				case 2:
//					if (player.addState.getBuff (typeof(BayonetChargeBuffDataClass)) != null) {
//						//						animationExtend = "";
//						activeSlot = 1;
//						skillData [activeSlot].skillAction (player);
//						mosAnimation = TYPE_ANIMATION.IDLE;
//						return true;//TYPE_BUFF_STATE.CONTINUE;
//					}
//					break;
//				case 3:
//					break;
//				}






				animationExtend = "_" + skillData [slot].getBuffData ().GetType ().ToString ().Replace ("BuffDataClass", "");

				Debug.Log ("AnimationExtend : " + animationExtend);
				//토글 사용시 스킬 쿨타임 미 진행
				mosAnimation = TYPE_ANIMATION.IDLE;
				activeSlot = slot;
				toggleSlot = slot;
//				character.resetSkillCoolTime ();
				return true;
			} 

			//스킬 실행 후 버프가 없으면 애니메이션 초기화
			else {

				resetToggle ();
//				mosAnimation = TYPE_ANIMATION.IDLE;
//				m_typeSkillState = TYPE_BUFF_STATE.CONTINUE;
//				animationExtend = "";
//				toggleSlot = -1;
//				idleAction ();
			}
		} 
		//토글형이 아니면
//		else {
//			player.closeSkillGuideLine ();
//		}


		//스킬 완료
		mosAnimation = TYPE_ANIMATION.IDLE;

		activeSlot = slot;
		character.resetSkillCoolTime (activeSlot);
		return true;

	}

	public void resetToggle(){
		mosAnimation = TYPE_ANIMATION.IDLE;
		m_typeSkillState = TYPE_BUFF_STATE.CONTINUE;
		animationExtend = "";
		toggleSlot = -1;
		idleAction ();
	}

	/// <summary>
	/// 스킬 가이드라인
	/// </summary>
	/// <param name="player">Player.</param>
	/// <param name="slot">Slot.</param>
	public virtual void skillGuideLine(ICharacterInterface player, int slot){

		if (isUseSkill ())
			return;

		if (skillData [slot] != null) {
			Debug.Log("스킬 가이드 : " + skillData[slot].name);
			skillData [slot].skillGuideLine (player);
			//스킬 시전 준비가 있으면 키기 _Ready

		}
	}

	/// <summary>
	/// 스킬 초기화
	/// </summary>
	/// <param name="slot">Slot.</param>
	public void initSkillAction(ICharacterInterface character){
		//		Debug.Log ("스킬 : " + m_skillAction [slot].name);
		foreach (SkillClass skill in skillData) {
			if (skill != null) {
//				if(skill.typeSkill == TYPE_SKILL.PASSIVE){
					Debug.Log("스킬 초기화 : " + skill.name);
					skill.initSkill (character);
//				}
			}
		}
	}

	/// <summary>
	/// 아이템 옵션 초기화
	/// </summary>
	/// <param name="character">Character.</param>
	void initItemAction(ICharacterInterface character){
		foreach(EquipmentClass equipment in m_equipements){
			if(equipment != null && equipment.option != null) {
				character.buffAdd (equipment.option, character, character);
			}
		}
	}

	/// <summary>
	/// 망원경 상태
	/// </summary>
	/// <param name="isTelescope">If set to <c>true</c> is telescope.</param>
	public virtual void telescopeAction(bool isTelescope){


		if (!isTelescope) {
			idleAction ();
		}
		else{

			mosAnimation = TYPE_ANIMATION.TELESCOPE;
			setAnimation (mosData.getAnimationKey (mosAnimation), false, false, 1f, 1f);
		}

	}

	protected bool isAnimation(string animationName){
		return mosSkeleton.isAnimation (animationName);
	}

	protected bool setBodyAnimation(string animationName, int loop, float time){
		return mosSkeleton.setBodyAnimation (animationName, loop, time);
	}

	protected bool setAnimation(string animationName, bool isBodyLoop, bool isLegLoop, float bodytime, float legtime){
		Debug.Log ("animationName : " + animationName);
		return mosSkeleton.setAnimation (getNameKey (animationName), isBodyLoop, isLegLoop, bodytime, legtime);
	}

	protected bool setBodyAnimation(string animationName, bool isLoop, float time){
		return mosSkeleton.setBodyAnimation (getNameKey(animationName), isLoop, time);
	}

	protected bool setLegAnimation(string animationName, bool isLoop, float time){
		return mosSkeleton.setLegAnimation (getNameKey(animationName), isLoop, time);
	}

	/// <summary>
	/// 키 반환
	/// </summary>
	/// <returns>The name key.</returns>
	/// <param name="animationName">Animation name.</param>
	protected string getNameKey(string animationName){
		if (mosAnimation == TYPE_ANIMATION.TELESCOPE)
			return animationName;

		if(isAnimation(animationName + animationExtend))
			return animationName + animationExtend;
		return animationName;
	}




	/// <summary>
	/// 행동 이벤트 - AnimateState.Event에 등록
	/// </summary>
	/// <param name="trackEntry">Track entry.</param>
	/// <param name="e">E.</param>
	protected virtual void actionEvent(Spine.TrackEntry trackEntry, Spine.Event e){


		switch (e.Data.name) {
		case "Attack":
			Debug.Log ("Attack : " + characterCtrler.character);
			weapon.attackAction (characterCtrler, shootPos);


			//사용하지 않음 - 어지러움 A0.8
			//if (character.GetType () == typeof(PlayerClass))
			//	((PlayerClass)character).cameraShake (weapon.recoil * 0.01f);


			if(weapon.shootParticle != null)
				character.setParticle (weapon.shootParticle, shootPos);
			//공격 이벤트
			break;
		case "Skill":
			Debug.Log ("Skill");
			//스킬 이벤트
			skillRun (character, activeSlot);

			break;
		}

	}

	/// <summary>
	/// 종료 이벤트 - AnimationState.End에 등록
	/// </summary>
	/// <param name="trackEntry">Track entry.</param>
	protected virtual void endEvent(Spine.TrackEntry trackEntry){

		//재장전 + 확장이름


		string reloadExtend = "Reload" + animationExtend;

//		Debug.Log (trackEntry.ToString() + " " + reloadExtend);

		//A0.8 토글시 재장전 확장 애니메이션 존재 확인
		if (!isAnimation (reloadExtend)) {
			reloadExtend = "Reload";
		}

		if(trackEntry.ToString() == reloadExtend) 
			mosAnimation = TYPE_ANIMATION.IDLE;


		//토글 사용중이면
		if (toggleSlot != -1)
			return;

		//스킬 사용중
		if (isUseSkill ()) 
			return;

		//시전 종료
//		switch (trackEntry) {
//		case "Skill0_Ready":
//			break;
//		case "Skill1_Ready":
//			break;
//		case "Skill2_Ready":
//			break;
//		case "Skill3_Ready":
//			break;
//		}
//			
//		//스킬 종료
//		switch (trackEntry) {
//		case "Skill0":
//			break;
//		case "Skill1":
//			break;
//		case "Skill2":
//			break;
//		case "Skill3":
//			break;
//		}

		//현재 애니메이션 종료시
		switch (mosAnimation) {
		case TYPE_ANIMATION.ATTACK:			
			return;
		case TYPE_ANIMATION.TELESCOPE:
			return;
		case TYPE_ANIMATION.DEAD:
			return;
		case TYPE_ANIMATION.RIDE:
			return;
		}


		//이동중 여부
		if (m_isMove)
			mosSkeleton.addBodyAnimation (getNameKey (mosData.getAnimationKey (TYPE_ANIMATION.MOVE)), 1f);
		else
			mosSkeleton.addBodyAnimation (getNameKey (mosData.getAnimationKey (TYPE_ANIMATION.IDLE)), 1f);		

	}

	/// <summary>
	/// 스킬 사용중
	/// </summary>
	/// <returns><c>true</c>, if use skill was ised, <c>false</c> otherwise.</returns>
	bool isUseSkill(){
		switch (mosAnimation) {
		case TYPE_ANIMATION.SKILL0:
//			Debug.Log ("skill use true");
			return true;
		case TYPE_ANIMATION.SKILL1:
			goto case TYPE_ANIMATION.SKILL0;
		case TYPE_ANIMATION.SKILL2:
			goto case TYPE_ANIMATION.SKILL0;
		case TYPE_ANIMATION.SKILL3:
			goto case TYPE_ANIMATION.SKILL0;
		}

//		Debug.Log ("skill use false");

		return false;
	}



}
