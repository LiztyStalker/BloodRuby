using System;
using UnityEngine;
using UnityEngine.UI;

public class SkillGuideLineClass : MonoBehaviour
{

	float m_guideLineRange;
	[SerializeField] SpriteRenderer m_guideLineImage;
	[SerializeField] SpriteRenderer m_areaImage;

	TYPE_SKILL_POS m_typeSkillPos;
	ICharacterInterface m_target;
	ICharacterInterface m_parent;

	ICharacterInterface parent{
		get{
			if (m_parent == null)
				OnEnable ();
			return m_parent;
		}
	}

	void OnEnable(){
		m_parent = transform.parent.GetComponent<ICharacterInterface> ();
		m_guideLineImage.transform.localScale = Vector3.one;
	}

	void Update(){
		setAngle (parent.angle);
	}

	void initPos(){
		//m_guideLineImage.sprite = null;
		//m_areaImage.sprite = null;
		m_guideLineImage.transform.position = parent.transform.position;
		m_guideLineImage.transform.eulerAngles = Vector3.zero;
	}

	public Vector3 getSkillPos(){


		//플레이어 위치
		//범위 위치
		//타겟 위치
		switch (m_typeSkillPos) {
		case TYPE_SKILL_POS.AREA_TARGET:
			if (parent.GetType () == typeof(CPUClass) && m_target != null) {
				return m_target.transform.position;
			}
			return m_areaImage.transform.position;
		case TYPE_SKILL_POS.BUILD:
			goto case TYPE_SKILL_POS.AREA_TARGET;
		}
		return m_guideLineImage.transform.position;
	}

	/// <summary>
	/// 타겟 지정하기 - 연속 실행
	/// </summary>
	/// <param name="target">Target.</param>
	public void setTarget(ICharacterInterface target){
		m_target = target;

		if (m_target == null) {
			//현재 위치
			viewTarget (parent.transform.position);
		}
		else
			//적 위치
			viewTarget (m_target.transform.position);
		
	}

	public ICharacterInterface getTarget(){
		return m_target;
	}

	public float getAngle(){
		return m_guideLineImage.transform.eulerAngles.z;
	}


	/// <summary>
	/// 목표를 중심으로 스킬 발동
	/// 전방으로 계속 레이캐스팅하여 적을 판단
	/// 캐릭터가 이동하는 방향
	/// </summary>
	public void viewTarget(){
		//목표가 있으면
		if (m_target != null) {


			//지역 선택이 있으면
			if (m_areaImage.sprite != null) {
				//목표를 가리킴
				m_areaImage.transform.position = m_target.transform.position;
//				m_target = null;
			}
			else {
				Vector3 dirVec = m_target.transform.position - parent.transform.position;
				setAngle (Mathf.Atan2 (dirVec.y, dirVec.x) * Mathf.Rad2Deg);
			}
			
			//Debug.Log ("target : " + m_target.name);
				

		} 
		//목표가 없으면
		else {
			//지역 선택이 있으면
			if (m_areaImage.sprite != null) {
				m_areaImage.transform.position = Camera.main.transform.position;
				Debug.Log ("pos : " + m_areaImage.transform.position);
			}
			else {
				setAngle (parent.angle);
			}
			//Debug.Log ("target : " + m_parent.name);
		}

	}

	/// <summary>
	/// 플레이어를 중심으로 스킬 발동
	/// </summary>
	/// <param name="viewPos">View position.</param>
	public void viewTarget(Vector3 viewPos){
		m_areaImage.transform.position = viewPos;
		setAngle (parent.angle);
	}

	void setAngle(float angle){
//		Debug.Log ("SetAngle : " + angle);
		m_guideLineImage.transform.eulerAngles = new Vector3 (0f, 0f, angle);
		if(m_areaImage.sprite != null)
			m_areaImage.transform.eulerAngles = new Vector3(0f, 0f, angle);
	}

	/// <summary>
	/// 스킬 가이드라인 보이기
	/// </summary>
	/// <param name="skillData">Skill data.</param>
	public void setSkillGuideLine(SkillClass skillData){
		initPos ();
		m_typeSkillPos = skillData.typeSkillPos; // 스킬 시작 위치 타입
		m_guideLineImage.sprite =  skillData.guideLineSprite; // 가이드라인 이미지
		m_areaImage.sprite =  skillData.areaSprite; // 범위 이미지
		m_guideLineRange = skillData.guideLineRange; // 가이드라인 크기
		m_guideLineImage.transform.localScale *= m_guideLineRange; // 가이드라인 크기
		m_guideLineImage.gameObject.SetActive (true); 

		guidelineAlarm (skillData); // 가이드라인시 UI로 보여지는 메시지

		//지역 선택이면
		if (m_areaImage.sprite != null) {
			m_areaImage.gameObject.SetActive (true);
			m_guideLineImage.transform.localScale *= 2f;
//			m_guideLineImage.transform.localScale = new Vector2 (m_guideLineRange, m_guideLineRange);
			//목표가 없으면
		} 
		//지역선택이 아니면
		else {
			m_areaImage.gameObject.SetActive (false);
			viewTarget ();
		}
	}

	/// <summary>
	/// 가이드라인 알람
	/// </summary>
	/// <param name="typeSkillPos">Type skill position.</param>
	void guidelineAlarm(SkillClass skillData){
		if (!(m_guideLineImage.sprite == null && m_areaImage.sprite == null)) {
			parent.characterCtrler.setMsg (skillData.typeSkillPos, skillData.isAlly);
		}
	}

	void OnDisable(){
		m_target = null;
	}
}



