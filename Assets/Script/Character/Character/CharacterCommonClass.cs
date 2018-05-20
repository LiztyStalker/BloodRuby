using UnityEngine;
using System;
using System.Collections;

public enum TYPE_TEAM{NONE = -10, ENEMY = -1, TEAM_0, TEAM_1, TEAM_2, TEAM_3}

public class CharacterCommonClass : ActionObjectClass, ICharacterInterface, IAction {

	protected UICharacterClass m_characterCtrler;

	CharacterFrameClass m_characterFrame;
	protected MOSDataClass m_mosData;
    //[SerializeField] protected readonly AddStateClass m_addStateObj;
	[SerializeField] string m_playerName;
	[SerializeField] protected float m_moveSpeed;
	[SerializeField] protected bool isTeam = false;
	[SerializeField] protected float m_shootDelay;
	[SerializeField] protected float m_shootTimeDelay;


	[SerializeField] protected int m_maxAmmo;
	[SerializeField] protected int m_useAmmo;
	[SerializeField] protected SkillGuideLineClass m_guideLine;
	[SerializeField] protected ShootAimClass m_shootAim;
	[SerializeField] protected CharacterSoundClass m_characterSound;
	[SerializeField] AutoTargetClass m_autoTarget;

	[SerializeField] bool m_isNotCoolTime = false;
	[SerializeField] string m_soundCharacterDeadKey;


	SoundPlayClass m_soundPlayer;

	bool m_isDead = false;
    float m_shootTime = 0f;


	float[] m_skillRunTime = new float[PrepClass.c_skillCnt];
    //	float[] m_skillCoolTime = new float[c_skillCnt]; 
    //	float[] m_skillCoolTimeRate = new float[c_skillCnt];

	bool m_isNoneTarget = false;

	bool m_isGameRun = false;
	//게임 진행

	protected TYPE_MOS m_mos;

	//테스트용
	//[SerializeField] int[] m_equipSlots;

	
	Vector2 m_velocity = Vector2.zero;
	Vector3 m_angle = Vector3.zero;

//	[SerializeField] SpriteRenderer render;

//	[SerializeField] GameObject m_shootPos;
	Vector2 m_shootPos;
	float m_reloadTime = 0f;

	//[SerializeField] GameObject m_character;
	//[SerializeField] GameObject m_weapon;

	//상태이상
	protected AddStateClass m_addState;
	//상태

	public AddStateClass addState{ get { return m_addState; } }
	//public ValueAddStateClass[] valueArray{ get { return GetComponents<ValueAddStateClass> (); } }
	//이속 = 이속 * 1/이속배율 + 이속추가
	public virtual float moveSpeed{ get { return 2f * m_addState.valueCalculator(mosData.moveSpeed, typeof(MoveSpeedAddStateClass));}}
	//공속 = 공속 * 1/공속배율 - 공속감소
	public float shootDelay {get {return m_addState.valueCalculator(mosData.shootDelay, typeof(AttackSpeedAddStateClass)); } }
	public TYPE_MOS mos{get{return m_mos;}}
	public MOSDataClass mosData{ get { return m_mosData; } }

//    public Vector3 position { get {return m_characterFrame.transform.position; } }
	public Transform transform{ get { return gameObject.transform; } }
	public Vector3 shootPos {get {return mosData.shootPos;}}
    public bool isGameRun { get { return m_isGameRun; } set { m_isGameRun = value; } }
	public int maxAmmo{ get { return (int)addState.valueCalculator((float)m_maxAmmo, typeof(AmmoAddStateClass)); } }
	public int useAmmo{ get { return m_useAmmo; } set {m_useAmmo = value;} }
    public float angle { get { return m_characterFrame.angle; } set { m_characterFrame.setAngle(value); } }
	public string playerName{ get { return (m_characterCtrler == null) ? "-" : m_characterCtrler.nickname; } }
	public bool isDead {get{return m_isDead;}}
	public float recoiling{ get { return m_shootAim.recoiling; } }

//	public ICharacterInterface target{ get { return m_autoTarget.target; } }// m_guideLine.getTarget();}}
	public AutoTargetClass autoTarget{get{return m_autoTarget;}}


	public float skillAngle { get { return m_guideLine.getAngle(); } }
	public Vector3 skillPos{ get { return m_guideLine.getSkillPos(); } }
	public UICharacterClass characterCtrler{ get { return m_characterCtrler; } }


	public SoundPlayClass soundPlayer{get{return m_soundPlayer;}}

    /// <summary>
    /// 리포트 값 삽입 후 가져오기
    /// </summary>
    /// <param name="value"></param>
    /// <param name="typeReport"></param>
    /// <returns></returns>
	public int addReport(int value, TYPE_MOS mos, TYPE_REPORT typeReport) {return m_characterCtrler.addReport(value, mos, typeReport);}

    /// <summary>
    /// 리포트값 가져오기
    /// </summary>
    /// <param name="typeReport"></param>
    /// <returns></returns>
	public int getReport(TYPE_MOS mos, TYPE_REPORT typeReport)  {return m_characterCtrler.getReport(mos, typeReport);}
	public int getReport(TYPE_REPORT typeReport) { return m_characterCtrler.getReport(typeReport); }

	/// <summary>
	/// 적 가져오기
	/// </summary>
	/// <returns>The target.</returns>
	/// <param name="angle">Angle.</param>
	/// <param name="team">Team.</param>
	/// <param name="isAlly">If set to <c>true</c> is ally.</param>
	/// <param name="isMyself">If set to <c>true</c> is myself.</param>
	/// <param name="isDead">If set to <c>true</c> is dead.</param>
	public ICharacterInterface getTarget(float angle, TYPE_TEAM team, bool isAlly, bool isMyself, bool isDead){
		return m_autoTarget.getTarget (angle, team, isAlly, isMyself, isDead);
	}


	public int nowHealth{get{return m_health;} }
    public int maxHealth
    {
        get
        {
			return (int)addState.valueCalculator((float)mosData.health, typeof(HealthAddStateClass));
        }
    }

	/// <summary>
	/// 스킬 쿨타임 비율
	/// </summary>
	/// <returns>The cool time rate.</returns>
	/// <param name="slot">Slot.</param>
	public float skillCoolTimeRate(int slot){
		if (slot < 0)
			slot = 0;
		else if (slot >= PrepClass.c_skillCnt)
			slot = 3;
				
		if (m_skillRunTime [slot] < 0f) return 1f;
		return PrepClass.ratioCalculator(m_skillRunTime[slot],  m_mosData.getSkillCoolTime(slot));
	}

	/// <summary>
	/// 스킬 쿨타임
	/// </summary>
	/// <returns>The run time.</returns>
	/// <param name="slot">Slot.</param>
	public float skillRunTime(int slot){

		if (slot < 0)
			slot = 0;
		else if (slot >= PrepClass.c_skillCnt)
			slot = 3;

		if (m_skillRunTime [slot] <= 0f) return 0f;
		return mosData.getSkillCoolTime(slot) - m_skillRunTime [slot];
	}


	void Awake(){
		m_characterFrame = GetComponent<CharacterFrameClass> ();
        m_addState = GetComponent<AddStateClass>();

		m_addState.initAddState (this);
			
	}

	/// 초기화 
	protected virtual void Start () {}

	/// 


	/// <summary>
	/// 플레이어 게임 준비
	/// </summary>
	/// <param name="parent">Parent.</param>
	/// <param name="mos">Mos.</param>
	/// <param name="equipmentSlots">Equipment slots.</param>
	public virtual void gameReady(UICharacterClass parent, TYPE_MOS mos, int[] equipmentSlots){


		m_soundPlayer = GetComponent<SoundPlayClass> ();

		if (parent == null){
			m_team = TYPE_TEAM.ENEMY;
		}
		else {
			m_characterCtrler = parent;
			m_team = m_characterCtrler.team;
		}
		//Debug.Log ("team : " + m_team);



		gameReady (mos, equipmentSlots);
		m_playerName = playerName;
		closeSkillGuideLine ();
	}



	MOSDataClass getMOSClass(TYPE_MOS mos){

		m_mos = mos;


		switch (mos) {
		case TYPE_MOS.DUALIST:
			return new MOSDualistDataClass ();
		case TYPE_MOS.GUARDIAN:
			return new MOSGuadianDataClass ();
		case TYPE_MOS.FIREBAT:
			return new MOSFirebatDataClass ();
		case TYPE_MOS.ASSAULT:
			return new MOSAssaultDataClass();
		case TYPE_MOS.HEAVY:
			return new MOSHeavyDataClass ();
		case TYPE_MOS.SNIPER:
			return new MOSSniperDataClass ();
		case TYPE_MOS.MAGICIAN:
			return new MOSMagicianDataClass ();
		case TYPE_MOS.CLERIC:
			return new MOSClericDataClass ();
		case TYPE_MOS.BUILDER:
			return new MOSBuilderDataClass ();
		}
		return null;
	}

	/// <summary>
	/// 게임 준비
	/// </summary>
	void gameReady(TYPE_MOS mos, int[] equipmentSlots){
		//병과 가져오기
		//병과 내의 장비 세팅하기

		//인스턴스화하여 실행할 경우 병과데이터는 인스턴스된 데이터만 가지고 있으면 됨
		//무기도 마찬가지 - 무기와 갑옷은 무조건 기본값 있음
		m_shootAim.gameObject.SetActive (true);

		//병과 데이터 초기화
//		m_mosData = new MOSDataClass ();
		m_mosData = getMOSClass (mos);

		if (mosData == null) {
			Debug.LogError ("병과 생성 불가 : " + m_mos);
			return;
		}


		//병과 데이터 셋팅
		mosData.gameReady (this, equipmentSlots);

		//Debug.Log (m_mosData.mos);

		//병과 이미지 붙이기
//		m_characterFrame.setSprite (this);
		weaponChange(mosData.weapon, mosData.weapon.ammo);

//		m_shootDelay = m_mosData.weapon.shootDelay;
//		m_maxAmmo = m_mosData.weapon.ammo;
//		m_useAmmo = m_maxAmmo;

		m_health = maxHealth;
//		m_health = 1;
//		m_moveSpeed = mosData.moveSpeed;

		m_autoTarget.setRange (8f);

		//스킬 쿨타임 초기화
		for (int i = 0; i < mosData.skillData.Length; i++) {
			if (mosData.skillData[i].typeSkill == TYPE_SKILL.PASSIVE) {
				m_skillRunTime [i] = -1f;
			}
			else{
				m_skillRunTime [i] = mosData.getSkillCoolTime (i);
			}
			//Debug.Log("스킬 초기화 : " + m_skillRunTime[i] + " " + i);
		}
		

        m_characterFrame.setHPBar(m_health, maxHealth);
		m_characterFrame.teamMarkerView (true);

		m_shootPos = mosData.shootPos;

	}

	/// <summary>
	/// 스켈레톤 애니메이션 설정
	/// </summary>
	/// <param name="skeletonAnimation">Skeleton animation.</param>
	public void setSkeletonAnimation(MOSSkeletonClass MOSSkeleton){
		m_characterFrame.setSkeletonAnimation (m_team, MOSSkeleton);
	}

	public WeaponEquipmentClass weaponChange(WeaponEquipmentClass weapon, int bulletCount){
		WeaponEquipmentClass pastWeapon = mosData.weapon;

		if (mosData.weapon != weapon) 
			mosData.weapon = weapon;
		

		//m_characterFrame.setWeaponSprite (weapon.equipImage);
		m_shootDelay = weapon.shootDelay;
		m_maxAmmo = weapon.ammo;
		m_useAmmo = bulletCount;

//		m_shootAim.setShootPos (mosData.shootPos);
		m_shootAim.setWeapon (weapon);


		return pastWeapon;
	}

	/// <summary>
	/// 게임 시작
	/// </summary>
	public virtual void gameStart(){}

	/// <summary>
	/// 게임 입장
	/// </summary>
	public virtual void gameEnter(){}

	/// <summary>
	/// 게임 퇴장
	/// </summary>
	public virtual void gameExit(){}

	/// <summary>
	/// 게임 종료
	/// </summary>
	public virtual void gameEnd(){}

	/// <summary>
	/// 재장전
	/// </summary>
	public virtual void reloadAction(){

		if (!isGameRun || isDead) return;

		if (m_reloadTime == 0f) {
			m_reloadTime = mosData.weapon.reloadDelay;
			mosData.reloadAction (1f / m_reloadTime);


			soundPlayer.audioPlay (mosData.weapon.reloadSoundKey, TYPE_SOUND.EFFECT);


			StartCoroutine (reloadActionCoroutine ());
		}
	}

	/// <summary>
	/// 장전 쿨타임
	/// </summary>
	/// <returns>The action coroutine.</returns>
	IEnumerator reloadActionCoroutine(){
//		float reloadCnt = m_reloadTime / (float)m_maxAmmo;
//		m_useAmmo = 0;
//		while (m_useAmmo < m_maxAmmo) {
//			m_useAmmo++;
			yield return new WaitForSeconds(m_reloadTime);
//		}
		m_reloadTime = 0f;
		m_useAmmo = m_maxAmmo;
//		m_mosData.moveAction (GetComponent<Rigidbody2D> ().velocity.magnitude);

	}


	protected virtual void Update(){

		m_characterFrame.setHPBar (m_health, maxHealth);

		if (!isGameRun || isDead) return;

		m_shootTime += Time.deltaTime;

		for (int i = 0; i < PrepClass.c_skillCnt; i++) {

			if (m_isNotCoolTime) {
				m_skillRunTime [i] = mosData.getSkillCoolTime (i);
			} else {
				if (m_skillRunTime [i] >= 0f) {
					m_skillRunTime [i] += Time.deltaTime;
//					Debug.Log ("skillCooltime : " + m_skillRunTime [i] + i);
					if (m_skillRunTime [i] >= mosData.getSkillCoolTime (i))
						m_skillRunTime [i] = mosData.getSkillCoolTime (i);
				}
			}
		}
	}


	/// <summary>
	/// 공격
	/// </summary>
	public virtual void attackAction(float angle){
		//return;

		if (!isGameRun || isDead) return;

		//공격 불가나 장전중이면
		if (addState.isConstraint(TYPE_CONSTRAINT.NOT_ATTACK) || m_reloadTime > 0f) return;

		m_shootTimeDelay = m_shootTime;

		//쿨타임이 되었으면
		if (isAttackDelay()) {

			//탄이 있으면
			if (m_useAmmo > 0) {
				m_shootTime = 0f;


				if (useBuff (this, TYPE_BUFF_STATE_ACT.ATTACK)) {
					switch (mosData.weapon.typeRange) {
					case TYPE_RANGE.LONG:
						if(mosData.weapon.isConsume) m_useAmmo--;
						break;
					}
				} else {

					//탄 상태에 따라 공격 방식 다름

					mosData.attackAction (m_characterCtrler, shootPos, 1f /  shootDelay);



					//autoTarget사용여부
					switch (mosData.weapon.typeRange) {
					case TYPE_RANGE.LONG:
						if(mosData.weapon.isConsume) m_useAmmo--;
						break;
					case TYPE_RANGE.SHORT:
//						m_mosData.attackAction (this, transform.position, 1f / shootDelay);
						break;
					}

					m_shootAim.setRecoil (mosData.weapon.recoil);
				}


			}
			else {
				reloadAction ();
			}

		} 


	}


	public bool isAttackDelay(){
		return (m_shootTime >= shootDelay);
	}


	/// <summary>
	/// 스킬 쿨타임 리셋
	/// </summary>
	/// <param name="slot">Slot.</param>
	/// <param name="rate">rate 0~1 (0%~100% 감소).</param>
	public void resetSkillCoolTime(int slot, float rate = 1f){
		//
		//1 = -100%
		//0.5 = -50%
		//+현재 쿨타임 더하기
		//

		//사용될 쿨타임
		if (rate == 1f)
			m_skillRunTime [slot] = 0f;
		else 
			m_skillRunTime [slot] += mosData.getSkillCoolTime (slot) * rate;
		
		m_isNoneTarget = false;

	}


	/// <summary>
	/// 스킬 발동
	/// </summary>
	/// <param name="slot">Slot.</param>
	public virtual void skillAction(int slot){

		//
		if (!isGameRun || isDead || addState.isConstraint(TYPE_CONSTRAINT.NOT_SKILL)) 
			return;

		if (m_skillRunTime [slot] >= 0f && skillCoolTimeRate (slot) == 1f) {

			//스킬 발동
			if (!mosData.skillAction (this, slot, 1f)) {
				Debug.Log ("SkillFalse");
				characterCtrler.setMsg (mosData.skillData [slot], false);
			} 

		}
	}

	/// <summary>
	/// 스킬 가이드라인 가져오기
	/// </summary>
	/// <param name="slot">Slot.</param>
	public virtual void skillGuideLine(int slot){

		if (!isGameRun || isDead || addState.isConstraint(TYPE_CONSTRAINT.NOT_ATTACK)) return;

		if (m_skillRunTime [slot] >= 0f) {
			if (skillCoolTimeRate (slot) >= 1f) {
				mosData.skillGuideLine (this, slot);
			} 
		}
	}



	/// <summary>
	/// 스킬 가이드라인 보이기
	/// </summary>
	public virtual void setSkillGuideLine (SkillClass skillData){
		//이미지
		//플레이어 중심, 망원경 중심, 타겟 중심
		//에임 안보임
		m_shootAim.gameObject.SetActive(false);

		m_guideLine.gameObject.SetActive(true);

//		characterCtrler.setMsg ();



		///에어리어 - 망원경 활성화
		m_guideLine.setSkillGuideLine(skillData);

		m_isNoneTarget = skillData.isNoneTarget;

		//타겟팅이 필요없는 스킬이라면
//		if (!skillData.isNoneTarget)
			//적은 autotarget에서 가져와야 함 - 루프를 돌려 계속 가져와야 함
//			StartCoroutine (guideLineCoroutine (skillData));
//		else
//			m_guideLine.setTarget (null);
	}

//	IEnumerator guideLineCoroutine(SkillClass skillData){
//		
//		while (m_guideLine.gameObject.activeSelf) {
//
//			//타겟이 필수여야 하는가 아닌가
//			//필수 - 타겟팅 Target, Player
//			//미필수 - 망원경 Area
//			if(!(skillData.typeSkillPos == TYPE_SKILL_POS.AREA_TARGET || skillData.typeSkillPos == TYPE_SKILL_POS.BUILD))
//				m_guideLine.setTarget (m_autoTarget.getTarget (angle, team, skillData.isAlly, skillData.isMyself, skillData.isDead, 45f));
//			
//			Debug.Log ("target : " + m_guideLine.getTarget ());
//
//			yield return new WaitForSeconds (PrepClass.c_timeGap);
//		}
//	}

	/// <summary>
	/// 스킬 가이드라인 끄기
	/// </summary>
	public virtual void closeSkillGuideLine(){

		characterCtrler.closeCastingMsg ();
		m_guideLine.gameObject.SetActive(false);
		//에임 보임
		m_shootAim.gameObject.SetActive(true);

	}


	/// <summary>
	/// 사망
	/// </summary>
	public virtual void deadAction(IBullet bullet){


		//사망 카운트 증가
		//사살자 팀 티켓 상승
		//사망 리스폰 보이기
		//
		if (!isGameRun || isDead) return;

        //게임 컨트롤러에게 사망자와 팀 메시지 보내기

        //자신, character 현재, 
		m_isDead = true;
	
		mosData.deadAction (1f);

		GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		GetComponent<CircleCollider2D> ().isTrigger = true;


		//가해자 KILL Event 발생
		if(bullet.characterCtrler != null)
			bullet.characterCtrler.character.addState.useBuff (bullet.characterCtrler.character, TYPE_BUFF_STATE_ACT.KILL);

		//피해자 Dead Event 발생
		addState.useBuff (this, bullet, TYPE_BUFF_STATE_ACT.DEAD);

		m_characterFrame.teamMarkerView (false);
		//무기
		m_characterCtrler.gameRespawn(bullet.characterCtrler, bullet);
		closeSkillGuideLine ();

		m_shootAim.gameObject.SetActive (false);

		addState.buffEndAll();

		soundPlayer.audioPlay (m_soundCharacterDeadKey, TYPE_SOUND.EFFECT);
	}




	public void setParticle(GameObject particleObj, Vector2 shootPos){
		GameObject obj = (GameObject)Instantiate (particleObj, shootPos, new Quaternion ());
		var main = obj.GetComponent<ParticleSystem> ().main;
		main.startRotation = -angle * Mathf.Deg2Rad;

//		var rend = obj.GetComponent<ParticleSystemRenderer> ();
//		rend.pivot = new Vector2 (0.5f, 0f);

	}



	/// <summary>
	/// 부활
	/// </summary>
	public virtual void rebirthAction(){
		m_isDead = false;
		GetComponent<CircleCollider2D> ().isTrigger = false;
		m_characterCtrler.gameRebirth (this);
		m_shootAim.gameObject.SetActive (true);
		m_characterFrame.teamMarkerView (true);
		mosData.rebirthAction (1f);
	}

	/// <summary>
	/// 이동
	/// </summary>
	/// <param name="dirX">Dir x.</param>
	/// <param name="dirY">Dir y.</param>
	/// <param name="angle">Angle.</param>
	public virtual void moveAction(float dirX, float dirY){
//		Debug.Log ("move");

		if (!isGameRun || addState.isConstraint(TYPE_CONSTRAINT.NOT_MOVE) || isDead) {
			GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			return;
		}


		useBuff (this, TYPE_BUFF_STATE_ACT.MOVE);


		m_velocity.Set (dirX, dirY);
		GetComponent<Rigidbody2D> ().velocity = m_velocity * moveSpeed * Time.deltaTime * PrepClass.c_moveSpeedOffset;

		//서버에 이동 전송

		mosData.moveAction (GetComponent<Rigidbody2D> ().velocity.magnitude, 1f / moveSpeed);

		m_characterSound.soundPlay (moveSpeed);



	}

	/// <summary>
	/// 보기
	/// </summary>
	/// <param name="angle">Angle.</param>
	public virtual void viewAction(float angle){


		if (addState.isConstraint (TYPE_CONSTRAINT.NOT_ROTATE))
			return;

		if (!m_isNoneTarget) {
			ICharacterInterface target = m_autoTarget.getTarget (angle, team, false, false, false);

			if (target != null) {
				//Debug.LogWarning ("viewtarget : " + target);
				Vector2 direction = target.transform.position - transform.position;
				angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
			}
			m_shootAim.aimPosition (angle, target);
		}

		this.angle = angle;

	}

	/// <summary>
	/// 거점 점령중
	/// </summary>
	/// <param name="flag">거점</param>
	/// <param name="isTeam">같은 팀 여부</param>
	public void setMsg(string flagName, CaptureObjectClass flag){
		if (m_characterCtrler != null){
			m_characterCtrler.setMsg("", flag);
		}
	}

	/// <summary>
	/// 피격
	/// </summary>
	/// <returns>true : 피격 </returns>
	/// <c>false</c>
	/// <param name="team">Team.</param>
	/// <param name="damage">Damage.</param>
	/// <param name="bullet">Bullet.</param>
	public override bool hitAction(TYPE_TEAM team, IBullet bullet){

		//피격 무시

		if (m_team != team) {
			//Debug.Log("hit");

			//무적 상태 또는 사망상태
			if (m_isDead) return false;

//			if (addState.valueCalculator (1f, typeof(DodgeAddStateClass))) {
//				//기본값 0, 100%
//			}


			//내가 적탄환에 당하면
			if (m_addState.useBuff (this, bullet, TYPE_BUFF_STATE_ACT.HIT))
				return true; //-
			

			if (addState.isConstraint(TYPE_CONSTRAINT.INVISIBLE))
				return true;


			Debug.Log ("hit");

			//데미지 피격감소
			int damage = 0;

			//참호판전
			//데미지 감소 판정

			//참호 외에서 공격
			if (!bullet.isInTrench) {
				//데미지 감소 있음
				damage = (!bullet.isPenetrate) ? (int)(m_addState.valueCalculator ((float)bullet.damage, typeof(DamageReductionAddStateClass))) : bullet.damage;
			}
			
			//참호 내에서 공격
			else {
				damage = (!bullet.isPenetrate) ? (int)(m_addState.valueCalculator ((float)bullet.damage, typeof(DamageReductionAddStateClass), new Type[]{ typeof(TrenchBuffDataClass) })) : bullet.damage;
			}
			

			//데미지가 0 이하이면 1로 고정
			if (damage <= 0) damage = 1;

			//데미지 받음
			addReport (damage, mos, TYPE_REPORT.DMG_GET);

			if (hitAction (damage)) {
				Debug.Log ("사망 : " + bullet + " - " + bullet.characterCtrler);

				deadAction (bullet);
			}

//			Debug.Log ("health : " + m_health);

			m_characterFrame.setHPBar (m_health, maxHealth);
			return true;

		}
		return false;
	}


	/// <summary>
	/// 버프 삽입
	/// </summary>
	/// <param name="buff">Buff.</param>
	/// <param name="buffData">버프.</param>
	/// <param name="character">버프 주인</param>
	public virtual BuffDataClass buffAdd(BuffDataClass buffData,ICharacterInterface ownerCharacter, ICharacterInterface actCharacter){
		Debug.Log ("buff");

		if (actCharacter == null)
			return null;

		if (m_characterCtrler != null)
			return m_characterCtrler.buffAdd (m_addState.buffAdd (buffData, ownerCharacter, actCharacter));
		else
			return m_addState.buffAdd (buffData, ownerCharacter, actCharacter);
	}

	/// <summary>
	/// 버프 강제 종료
	/// </summary>
	/// <returns><c>true</c>, if end was buffed, <c>false</c> otherwise.</returns>
	/// <param name="buffData">강제 종료할 버프.</param>
	public bool buffEnd(BuffDataClass buffData){
		/// 
		if (buffData.buffEnd ()) {
			return buffEndPanel (buffData);
		}
		return false;
	}


	public bool buffEndPanel(BuffDataClass buffData){
		if(m_characterCtrler != null)
			return m_characterCtrler.buffEnd(buffData);
		return false;
	}

	/// <summary>
	/// 체력회복
	/// </summary>
	/// <param name="health">치유량</param>
	/// <param name="ownerCharacter">치유자.</param>
	public void addHealth(int healthPoint, ICharacterInterface ownerCharacter){

		if (addState.useBuff (this, TYPE_BUFF_STATE_ACT.SKILL)) {
			return;
		}

		if (addHealth (healthPoint, maxHealth)) {
			//회복 받음
			addReport (healthPoint, mos, TYPE_REPORT.HEAL_GET);

			//치유자가 있으면
			if(ownerCharacter != null) ownerCharacter.addReport (healthPoint, ownerCharacter.mos, TYPE_REPORT.HEAL_SET);
		}
		

		m_characterFrame.setHPBar(m_health, maxHealth);

	}

	/// <summary>
	/// 버프 사용하기
	/// </summary>
	/// <param name="character">버프에 당하는 캐릭터.</param>
	/// <param name="buffState">Buff state.</param>
	public bool useBuff(ICharacterInterface character, IBullet bullet, TYPE_BUFF_STATE_ACT buffStateAct){
		return m_addState.useBuff (character, bullet, buffStateAct);
	}

	/// <summary>
	/// 버프 사용하기
	/// </summary>
	/// <param name="character">Character.</param>
	/// <param name="buffState">Buff state.</param>
	public bool useBuff(ICharacterInterface character, TYPE_BUFF_STATE_ACT buffStateAct){
		return m_addState.useBuff (character, buffStateAct);
	}

	/// <summary>
	/// 아이템 습득
	/// </summary>
	public void itemAction(ItemObjectClass itemObject){
		Debug.Log ("아이템 습득");

		m_characterCtrler.addReport (1, m_mos, TYPE_REPORT.ITEM_GET);
		/// 
		switch (itemObject.typeItem) {
		case TYPE_ITEM.HEALTH:
			addHealth (itemObject.value, null);
			break;
		case TYPE_ITEM.SHIELD:
			buffAdd(itemObject.buffData, this, this);
			break;
		case TYPE_ITEM.RUBY:
			m_characterCtrler.addReport (itemObject.value, m_mos, TYPE_REPORT.RUBY_GET);
			break;
		case TYPE_ITEM.COIN:
			m_characterCtrler.addReport (itemObject.value, m_mos, TYPE_REPORT.BTP_GET);
			break;
		case TYPE_ITEM.COOLTIME:
			//쿨타임 감소 버프
			buffAdd(itemObject.buffData, this, this);
			break;
		case TYPE_ITEM.INVICIBLE:
			//무적화 버프
			buffAdd(itemObject.buffData, this, this);
			break;
		}

	}
}
