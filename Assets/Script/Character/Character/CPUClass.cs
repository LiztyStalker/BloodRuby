using UnityEngine;
using System.Collections;
//using UnityEngine.AI;


public class CPUClass : CharacterCommonClass, ICharacterInterface {

	//[SerializeField] bool m_isDummy;
	bool m_isDummy;

    const float c_length = 7f;
	const float c_skillProbabilityPoint = 0.5f;
	const float c_skillProbability = 3.5f;
	const float c_moveSpeedOffset = 1.25f;
	const float c_minWeaponRangeRate = 0.05f;

	float m_skillProbability = 0f;

//	const float c_longLiftTime = 300f;
//	float m_lifeTime = 0f;

    NavMeshAgent2D m_navMesh;

	TYPE_SKILL m_typeSkill;
	int m_skillSlot = -1;
	int m_level = 1;
//	bool isCapture = false;
//    NavMeshAgent m_navMesh;


//	[SerializeField] int m_health;
//	[SerializeField] int m_speed;

//	[SerializeField] int m_bulletCnt;
//	[SerializeField] int m_bulletMaxCnt;

//	[SerializeField] GameObject m_shootPos;

	//[SerializeField] GameObject m_bullet;

//	[SerializeField] TYPE_TEAM m_team;

//	[SerializeField] TYPE_MOS m_mosD;

	GameObject m_character = null;

//	float m_time = 0f;
//	float m_delay = 2f;

	const float m_destinationMaxTime = 1f;
	float m_destinationTime = 0f;

	float m_stopTime = 0f;

	Transform m_destination = null;

	//int m_slot = 0;

//    public ICharacterInterface target { get { return m_target; } }
//	public ICharacterInterface ally { get { return m_ally; } }
	float skillProbability{get{return m_skillProbability + c_skillProbability + m_level;}}
	public bool isDummy{ get { return m_isDummy; } set { m_isDummy = value; } }
	public void warpPosition(Vector2 pos){ m_navMesh.Warp (pos); }
	public override float moveSpeed {
		get {
			return base.moveSpeed * c_moveSpeedOffset;
		}
	}

	/// <summary>
	/// 목적지 제거 A0.8
	/// </summary>
//	public void resetDestination(){
//		m_destination = null;
//	}

	/// <summary>
	/// 목적지 부여 A0.8
	/// </summary>
	/// <returns><c>true</c>, if destination was set, <c>false</c> otherwise.</returns>
	/// <param name="destination">Destination.</param>
	public bool setDestination(){
//
//		if (!isGameRun)
//			return false;
//		if (m_isDummy)
//			return false;

		//A0.8 - 경로가 없으면 경로 부여
//		if (!isDestination ()){
			m_destination = ((UICPUClass)characterCtrler).point;
			return moveDestination (m_destination);
//		}

//		return false;
	}

	public bool enemyDestination(){
//		bool isEnemy = false;
//		if (!isGameRun)
//			return isEnemy;
//		if (m_isDummy)
//			return isEnemy;
//		if (isCapture)
//			return false;

//		Debug.LogWarning ("targetLose : " + m_target);
		if (autoTarget.target != null) {
//			Debug.LogError ("targetDes");
			if (!autoTarget.target.isDead) {
				moveDestination (autoTarget.target.transform);
			}
		} 
		else{
			setDestination ();
		}
//		Debug.LogError ("des : " + isEnemy);
		return true;

	}

	/// <summary>
	/// 목적지 유무 A0.8
	/// </summary>
	/// <returns><c>true</c>, if destination was ised, <c>false</c> otherwise.</returns>
	public bool isDestination(){
		return m_navMesh.hasPath;
	}

	/// <summary>
	/// 목적지 도착 유무 A0.8
	/// true : 도착
	/// </summary>
	/// <returns><c>true</c>, if destination arrive was ised, <c>false</c> otherwise.</returns>
	public bool isDestinationArrive(){
		if (m_navMesh.remainingDistance <= m_navMesh.stoppingDistance) {
			m_navMesh.ResetPath ();
			return true;
		}
		return false;
	}

    /// <summary>
    /// 목표로 이동
    /// true : 목표에 도착
    /// false : 목표에 도착하지 않음
    /// </summary>
    /// <param name="destination"></param>
    /// <returns></returns>
    bool moveDestination(Transform destination) 
    {
//		m_navMesh.ResetPath ();
		m_navMesh.SetDestination (destination.position);
		return !m_navMesh.hasPath;
    }

	protected override void Start(){


		base.Start ();
        m_navMesh = GetComponent<NavMeshAgent2D>();
        m_navMesh.speed = moveSpeed;
		m_navMesh.stoppingDistance = mosData.weapon.range * c_minWeaponRangeRate;
		m_navMesh.acceleration = c_moveSpeedOffset * moveSpeed;

		m_health = m_health;
	}

/// <summary>
/// 적 발견
/// true 적 선택
/// flase 적 없음
/// </summary>
/// <returns></returns>
	public bool setTarget(){
		bool isTarget = false;
		if (autoTarget.target == null)
			isTarget = autoTarget.isSetTarget (team);
		else
			isTarget = true;
		return isTarget;
	}


	bool setAlly(){
		return autoTarget.isSetAlly (team);
	}


    /// <summary>
    /// 타겟을 향해 보기
    /// </summary>
    public void targetRotate()
    {
        try
        {

			/// 

			if(!addState.isConstraint(TYPE_CONSTRAINT.NOT_ROTATE)){

				Vector2 vec = transform.position - autoTarget.target.transform.position;
	            angle = PrepClass.angleCalculator(vec.x, vec.y, 180f);
			}

            //공격
            //attackAction(angle);
        }
        catch
        {
			autoTarget.resetTarget ();
        }

    }

    /// <summary>
    /// 이동방향을 향해 보기
    /// </summary>
    void viewRotate()
    {
        Vector2 vec = GetComponent<NavMeshAgent2D>().velocity * -1f;
        angle = PrepClass.angleCalculator(vec.x, vec.y, 180f);

        //공격
        viewAction(angle);

    }



    protected override void Update()
    {

		if (!isGameRun || isDead)	return;

		base.Update();

//		if (m_navMesh.velocity == Vector2.zero) {
//			Debug.LogWarning ("cpu : " + playerName + " " + m_navMesh.hasPath + " " + m_navMesh.destination + " " + m_navMesh.isActiveAndEnabled);
//		}

		if (!addState.isConstraint (TYPE_CONSTRAINT.NOT_ROTATE)) {
			if (autoTarget.target == null)
				viewRotate ();
			else
				targetRotate ();
		}

		if (addState.isConstraint (TYPE_CONSTRAINT.NOT_MOVE)) {
			m_navMesh.speed = 0f;
		} else {
			m_navMesh.speed = moveSpeed;
		}

    }


	public override void gameReady (UICharacterClass parent, TYPE_MOS mos, int[] equipmentSlots){
		base.gameReady (parent, mos, equipmentSlots);
		m_level = parent.level;
	}
	
	/// <summary>
	/// 게임 시작
	/// </summary>
	public override void gameStart (){
		base.gameStart ();
	}
	
	/// <summary>
	/// 게임 입장
	/// </summary>
	public override void gameEnter (){
		base.gameEnter ();
	}
	
	/// <summary>
	/// 게임 퇴장
	/// </summary>
	public override void gameExit (){
		base.gameExit ();
	}

	/// <summary>
	/// 게임 종료
	/// </summary>
	public override void gameEnd (){
		base.gameEnd ();
	}
	
	/// <summary>
	/// 재장전
	/// </summary> 
	public override void reloadAction (){
		base.reloadAction ();
	}

	/// <summary>
	/// 공격 액션
	/// 각도 : 캐릭터가 바라보고 있는 방향
	/// </summary>
	public void attackAction(){
		if(m_isDummy) return;
		attackAction(angle);
	}

	/// <summary>
	/// 공격
	/// </summary>
	public override void attackAction (float angle){
		//Debug.Log ("적 공격");
		//return;
//		if(m_isDummy) return;
		base.attackAction (angle);
	}
	
	/// <summary>
	/// 바라보기
	/// </summary>
	/// <param name="angle">Angle.</param>
	public override void viewAction(float angle){}



	/// <summary>
	/// 이동
	/// </summary>
//	public bool moveAction(){
//		return moveDestination (m_destination);
//	}

	/// <summary>
	/// 이동
	/// </summary>
	/// <param name="dirX">Dir x.</param>
	/// <param name="dirY">Dir y.</param>
	//public override void moveAction(float dirX, float dirY){}
	
	/// <summary>
	/// 스킬
	/// </summary>
	/// <param name="slot">Slot.</param>
	//public override void skillAction (int slot){
		//각각의 스킬에 맞게 행동해야 함
	//}

	/// <summary>
	/// 스킬 사용
	/// </summary>
	/// <returns><c>true</c>, if action was skilled, <c>false</c> otherwise.</returns>
	public bool skillAction(){
//		Debug.LogWarning ("skillUse : " + m_skillSlot);
		if (m_skillSlot >= 0 && m_skillSlot <= 3) {
			mosData.skillData [m_skillSlot].skillAction (this);
			resetSkillCoolTime (m_skillSlot);
			m_skillSlot = -1;
			m_typeSkill = TYPE_SKILL.PASSIVE;
//			m_navMesh.enabled = false;
			return true;
		}
		return false;
	}
		
	/// <summary>
	/// 사망
	/// </summary>
	public override void deadAction (IBullet bullet){
		base.deadAction (bullet);
		m_navMesh.enabled = false;
	}
	
	/// <summary>
	/// 부활
	/// </summary>
	public override void rebirthAction (){
		base.rebirthAction ();
		m_navMesh.enabled = true;
	}
	
	/// <summary>
	/// 피격 true - 사망 판정
	/// </summary>
	/// <returns><c>true</c>, if action was hit, <c>false</c> otherwise.</returns>
	/// <param name="team">Team.</param>
	/// <param name="damage">Damage.</param>
	public override bool hitAction (TYPE_TEAM team, IBullet bullet){
		return base.hitAction (team, bullet);
	}

	/// <summary>
	/// 현재 위치가 거점인지 확인
	/// </summary>
	/// <returns><c>true</c>, if capture position was ised, <c>false</c> otherwise.</returns>
	public bool isCapturePosDestination(){
		if (isDestination()) {
			//거점이면 아군거점인지 아닌지 판단
			//아군거점이 아니면 점령 - Ai에서 판단

			//거점이면
			if (m_destination.tag == "Flag") {
				return true;
			}

		} 
		return false;
	}


	/// <summary>
	/// 거점 아군 판별
	/// </summary>
	/// <returns><c>true</c>, if capture team was ised, <c>false</c> otherwise.</returns>
	public bool isCaptureTeam(){
		
		if (isDestination ()) {
			//점령 가능 및 아군거점이 아닌 경우

			CaptureObjectClass capturePos = m_destination.GetComponent<CaptureObjectClass> ();
			if (capturePos != null) {
				if (capturePos.isCaptured && capturePos.team != team) {
					m_navMesh.Stop ();
					m_navMesh.autoBraking = true;
//					isCapture = true;
					return false;
				} 
			}
//			Debug.Log ("아군");
		}
//		isCapture = false;
		return true;

	}


	/// <summary>
	/// 망원경
	/// </summary>
	public void telescopeAction(){}
	
	/// <summary>
	/// 일시정지
	/// </summary>
	public void pauseAction(){}

	/// <summary>
	/// 적 등록
	/// </summary>
	/// <returns><c>true</c>, if set enemy was ised, <c>false</c> otherwise.</returns>
	public bool isSetEnemy(){
		bool isTarget = setTarget ();
		if (isTarget) {
			targetRotate ();
			m_typeSkill = TYPE_SKILL.ENEMY;
		}
//		Debug.LogError ("Enemy");
		return isTarget;
	}

	/// <summary>
	/// 아군 등록
	/// </summary>
	/// <returns><c>true</c>, if set ally was ised, <c>false</c> otherwise.</returns>
	public bool isSetAlly(){
		bool isAlly = setAlly ();
		if (isAlly) {
			m_typeSkill = TYPE_SKILL.ALLY;
		}
//		Debug.LogError ("Ally");
		return isAlly;
	}

	/// <summary>
	/// 거점 등록
	/// </summary>
	/// <returns><c>true</c>, if flag was ised, <c>false</c> otherwise.</returns>
	public bool isSetFlag(){
		bool isFlag = isCaptureTeam ();
		if (isFlag)
			m_typeSkill = TYPE_SKILL.FLAG;
		
//		Debug.LogError ("isFlag : " + isFlag);
		return isFlag;
	}

	/// <summary>
	/// 스킬 사용 가능한지 여부
	/// </summary>
	/// <returns><c>true</c>, if skill used was ised, <c>false</c> otherwise.</returns>
	public bool isSkillUsed(){
		bool isAct = false;
		switch (mosData.mosAnimation) {
		case TYPE_ANIMATION.IDLE:
			isAct = false;
			break;
		case TYPE_ANIMATION.MOVE:
			isAct = false;
			break;
		case TYPE_ANIMATION.ATTACK:
			isAct = false;
			break;
		default:
//			if(!m_navMesh.enabled)
//				m_navMesh.enabled = true;
			
			isAct = true;
			break;
		}
//
			
//		Debug.LogError ("isAct : " + isAct);
//		Debug.LogError ("SkillUse : " + mosData.mosAnimation);
		return isAct;
	}

	/// <summary>
	/// 스킬 사용 확률
	/// </summary>
	/// <returns><c>true</c>, if skill probability was ised, <c>false</c> otherwise.</returns>
	public bool isSkillProbability(){
		//확률밖이면 
		//스킬 발동확률 증가
		if (skillProbability < Random.Range (0f, 100f)) {
			m_skillProbability += c_skillProbabilityPoint * m_level;
			return false;
		} 	//스킬 발동시
			//발동확률 초기화
		m_skillProbability = 0f;
		return true;
	}

	/// <summary>
	/// 스킬 찾기
	/// </summary>
	/// <returns><c>true</c>, if search was skilled, <c>false</c> otherwise.</returns>
	public bool skillSearch(){
		//지정된 스킬이 패시브이면 false
		bool isSearch = false;
		if (m_typeSkill == TYPE_SKILL.PASSIVE)
			return isSearch;
/// 
		if (addState.isConstraint (TYPE_CONSTRAINT.NOT_SKILL))
			return isSearch;
		
		isSearch = searchSkill (m_typeSkill);
//		Debug.LogError ("isSearch : " + isSearch);
//		Debug.LogError ("Search");
		return isSearch;
	}


	/// <summary>
	/// 사정거리 내
	/// </summary>
	/// <returns><c>true</c>, if range was ised, <c>false</c> otherwise.</returns>
	public bool isSkillRange(){
//		Debug.Log ("SkillRange");

		if (m_skillSlot >= 0 && m_skillSlot <= 3) {
			switch (m_typeSkill) {
			case TYPE_SKILL.ENEMY:
				if (autoTarget.target != null && mosData.skillData [m_skillSlot].guideLineRange < Vector2.Distance (autoTarget.target.transform.position, transform.position)) {
					//m_navMesh.ResetPath ();
					autoTarget.resetTarget();
					return false;
				}
				break;
			case TYPE_SKILL.ALLY:
				if (autoTarget.ally != null && mosData.skillData [m_skillSlot].guideLineRange < Vector2.Distance (autoTarget.ally.transform.position, transform.position)) {
					//m_navMesh.ResetPath ();
					autoTarget.resetAlly();
					return false;					
				}
				break;
			case TYPE_SKILL.FLAG:
				if (mosData.skillData [m_skillSlot].guideLineRange < Vector2.Distance (m_destination.transform.position, transform.position)) {
					//m_navMesh.ResetPath ();
					m_destination = null;
					return false;
				}
				break;
			}
		}
		return true;
	}

	public bool isWeaponRange(){
//		Debug.LogError ("WeaponRange");
		if (mosData.weapon.range * 2f < Vector2.Distance (autoTarget.target.transform.position, transform.position)) {
			return false;
		}
		return true;
	}

	/// <summary>
	/// 스킬찾기 - 스킬 등록
	/// </summary>
	/// <returns><c>true</c>, if skill was searched, <c>false</c> otherwise.</returns>
	/// <param name="typeSkill">Type skill.</param>
	bool searchSkill(TYPE_SKILL typeSkill){
//		Debug.LogError ("SetSkill");
		for (int i = 0; i < mosData.skillData.Length; i++) {
			if (mosData.skillData[i].typeSkill == typeSkill) {
				if (mosData.skillData [i].typeSkillState != TYPE_BUFF_STATE.TOGGLE) {
//					Debug.LogError ("cooltime : " + skillCoolTimeRate (i));


					if (skillCoolTimeRate (i) >= 1f) {
						m_skillSlot = i;
						return true;
					}
				}
			}
		}
		return false;
	}

	/// <summary>
	/// 자신의 체력이 낮은지 여부
	/// </summary>
	/// <returns><c>true</c>, if low H was ised, <c>false</c> otherwise.</returns>
	public bool isLowHP(){
//		if (nowHealth <= maxHealth * 0.3f)
//			return true;
//		Debug.LogError("lowHP");
		return false;
	}




	/// <summary>
	/// 적이 보이는지 여부
	/// </summary>
	/// <returns><c>true</c>, if enemy view was ised, <c>false</c> otherwise.</returns>
	public bool isEnemyView(){
//		Debug.LogError ("EnemyView");
		if (autoTarget.target != null)
			return isEnemyView (autoTarget.target);
		return false;
	}

	bool isEnemyView(ICharacterInterface character){
		/// 

//		Debug.LogError ("target");
		RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, c_length, Vector2.zero);

		//적 발견 
		foreach (RaycastHit2D hit in hits)
		{
			if (PrepClass.isCharacterTag(hit.collider.tag))
			{

				if (character == hit.collider.GetComponent<ICharacterInterface> ()) {
					RaycastHit2D tmpTarget = Physics2D.Raycast(transform.position, hit.transform.position - transform.position, c_length);

					if (PrepClass.isCharacterTag (tmpTarget.collider.tag)) {
						if (character == hit.collider.GetComponent<ICharacterInterface> ()) {
							return true;
						}
					}
				}
			}
		}
		return false;
	}



	public bool isLoseFlag(){
		//점령당하는 가장 가까운 거점이 있는지 여부
		//35확률로 있으면 거점 등록
		return true;
	}

	//장거리 스킬 유무
	public bool isRangeSkill(){
		//장거리 스킬이 있으면
		//35 확률
		return true;
	}

	public bool skillRangeAction(){
		//장거리 스킬 거점을 향해 발사
		return true;
	}

	public bool setFlagDes(){
		//점령당하는 가장 가까운 거점으로 이동
		return true;
	}
}
