using UnityEngine;
using System.Collections;

public class PlayerClass : CharacterCommonClass, IPlayerCharacterInterface {



	UICameraCtrlClass m_camera;

//	[SerializeField] Transform m_aimSize;


	bool m_isTelescope = false;
//	bool m_isBuild = false;
	bool m_isPause = false;

	//float m_shootDelay = 0f;


	public bool isTelescope{ get { return m_isTelescope; } }
//	public bool isBuild{ get { return m_isBuild; } }
	public bool isPause{ get { return m_isPause; } }
//	public UICameraCtrlClass cameraSet{ get { return m_camera; } }

	bool m_isMove = false;
//	bool m_isAttack = false;

	public bool isMove{ get { return m_isMove; } private set{m_isMove = value;}}
//	public bool isAttack{ get { return m_isAttack; } private set{m_isAttack = value;}}


	public void setCamera(Vector2 rect, Vector2 telescopeRect){
		m_camera = Camera.main.GetComponent<UICameraCtrlClass>();
		m_camera.setCamera (this, rect, telescopeRect);
//		m_camera.setTelescope (m_isTelescope);
	}


	protected override void Update ()
	{
		base.Update ();

		if (m_guideLine.isActiveAndEnabled) {
			//망원경이 꺼져있으면
			if (!m_isTelescope)
				m_guideLine.viewTarget ();
			//망원경이 켜져있으면
			else
				m_guideLine.viewTarget ((Vector2)Camera.main.transform.position);
		}

	}
	
	/// <summary>
	/// 게임 준비
	/// </summary>
	public override void gameReady(UICharacterClass parent, TYPE_MOS mos, int[] equipmentSlots){
		base.gameReady (parent, mos, equipmentSlots);
		m_isTelescope = false;
		telescopeAction ();
		((UIPlayerClass)m_characterCtrler).setAim (false);
		//
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
	/// 공격
	/// 무기에 있는 탄환으로 공격
	/// </summary>
	public override void attackAction(float angle){

//		if (mosData.mosAnimation == TYPE_ANIMATION.ATTACK) isAttack = true;
//		else isAttack = false;

		//망원경 상태가 아니면 공격가능
		// 망원조준경상태이면 카메라 중심을 보고 공격
		if (m_isTelescope) {
			if (addState.isConstraint(TYPE_CONSTRAINT.TELESCOPE_ATTACK)) {
				Vector2 dirVec = m_camera.transform.position - transform.position;
				base.attackAction(PrepClass.angleCalculator (dirVec.x, dirVec.y));
			}
			return;
		}

		base.attackAction (angle);
	}

	public void cameraAction(float dirX, float dirY){
		m_camera.characterMove (dirX, dirY);
	}



	public void skillAction(int slot, bool isEnforce){

		if(!m_isTelescope || addState.isConstraint(TYPE_CONSTRAINT.TELESCOPE_ATTACK) || isEnforce || addState.getBuff(typeof(WeaponChangeBuffDataClass)) != null)
			skillAction (slot);
		
	}



	/// <summary>
	/// 스킬
	/// </summary>
	/// <param name="slot">Slot.</param>
	public override void skillAction(int slot){
		//망원경 사용시 공격 불가 - 저격수 가능

		//망원경 상태가 아니거나 망원조준경상태이면 공격 가능
		//if(!m_isTelescope || m_addState.isTelescopeAttack)
//		Debug.Log("스킬사용");
		base.skillAction (slot);

		if (addState.getBuff (typeof(WeaponChangeBuffDataClass)) != null)
			if (m_isTelescope) telescopeAction ();
		else {
			if (m_isTelescope && !addState.isConstraint (TYPE_CONSTRAINT.TELESCOPE_ATTACK)) {
				telescopeAction ();
			}
		}

//		if (m_isTelescope) m_isTelescope = !m_isTelescope;
//		if (m_isTelescope)
//			telescopeAction ();




	}

	public virtual void setSkillGuideLine (SkillClass skillData){
		//이미지
		//플레이어 중심, 망원경 중심, 타겟 중심
		base.setSkillGuideLine(skillData);

		if (skillData.typeSkillPos == TYPE_SKILL_POS.AREA_TARGET || skillData.typeSkillPos == TYPE_SKILL_POS.BUILD) {
			if (!isTelescope) {
				telescopeAction ();
				m_camera.transform.position = m_guideLine.getSkillPos() + (Vector3.back * 10f);
				m_camera.setSkillGuideRange (skillData.guideLineRange);
				m_guideLine.viewTarget (m_camera.transform.position);
			}
				
		}			


	}

	public override void closeSkillGuideLine ()
	{
		

		base.closeSkillGuideLine ();


//		if (addState.getBuff (typeof(WeaponChangeBuffDataClass)) != null) 
//			if (m_isTelescope)
//				telescopeAction ();
		
		if (addState.getBuff (typeof(TelescopesightBuffDataClass)) == null && m_isTelescope)
			telescopeAction ();
		//카메라 원상복구

	}


	public void cameraShake(float force){
		m_camera.cameraShake (force);
	}
	
	/// <summary>
	/// 사망
	/// </summary>
    //public override void deadAction(ICharacterInterface character)
    //{
    //    //망원경, 일시정지 초기화
    //    //return;
    //    base.deadAction(character);
    //}
	
	/// <summary>
	/// 부활
	/// </summary>
//	public override void rebirthAction(){
//		base.rebirthAction ();
//
//
//	}
	

	public override void moveAction (float dirX, float dirY)
	{

//		Gizmos.DrawRay(transform.position, transform.TransformDirection(m_pastPos - player.transform.position));


		if (dirX == 0f && dirY == 0f) isMove = false;
		else isMove = true;
		

		if (!m_isTelescope) {
			base.moveAction (dirX, dirY);
			//m_camera.setCharacterVelocity (0f, 0f);
		}

		//망원경 사용시 화면만 움직임
		else
			//스킬사용시 최대 사거리 필요
			m_camera.telescopeMove (dirX, dirY);

	}


	public override void viewAction (float angle)
	{


		if (m_isTelescope) {
			Vector2 dirVec = m_camera.transform.position - transform.position;
			base.viewAction (PrepClass.angleCalculator (dirVec.x, dirVec.y));
		} 
		else base.viewAction (angle);
	}

	/// <summary>
	/// 피격 true - 맞음
	/// </summary>
//	public override bool hitAction(TYPE_TEAM team, IBullet bullet){
//		return base.hitAction (team, bullet);
//	}


	/// <summary>
	/// 망원경
	/// </summary>
	public void telescopeAction(){

        if (!isGameRun) return;

		if (m_isTelescope) telescopeInactive ();
		else telescopeActive ();

		m_camera.setTelescope (m_isTelescope);
		m_mosData.telescopeAction (m_isTelescope);
		((UIPlayerClass)m_characterCtrler).setAim (m_isTelescope);
//		m_uiCharacter
	}

	void telescopeActive(){
		moveAction (0f, 0f);
		m_isTelescope = true;
		m_shootAim.gameObject.SetActive (!m_isTelescope);
		addState.useBuff (this, TYPE_BUFF_STATE_ACT.TELESCOPE);
	}

	void telescopeInactive(){
		m_isTelescope = false;
		m_shootAim.gameObject.SetActive (!m_isTelescope);
		addState.useBuff (this, TYPE_BUFF_STATE_ACT.TELESCOPE);
	}



//	void OnGizmosDraw(){
//		Gizmos.color = Color.cyan;
//		Gizmos.DrawLine (transform.position, m_camera.transform.position);
//	}

	/// <summary>
	/// 일시정지
	/// </summary>
	public void pauseAction(){
		m_isPause = !m_isPause;
	}





}
