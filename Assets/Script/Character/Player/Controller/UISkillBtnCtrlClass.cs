using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UISkillBtnCtrlClass : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

	[SerializeField] int m_slot;

	[SerializeField] Image m_backgroundImage;
	[SerializeField] Image m_coolTimeImage;
	[SerializeField] Image m_useNotImage;
	[SerializeField] Image m_highLightImage;
	[SerializeField] Text m_timeText;

	UIPlayerCtrlClass m_playerCtrl;
	SkillClass m_skillData = null;
	Vector2 m_pos;

	public delegate void BtnDeleagte();
	BtnDeleagte m_del;

	bool m_skillReady = true;
	bool m_isContentView = false;

	float m_contentTime = 0f;

	void Start(){
		//GetComponent<RectTransform> ().sizeDelta = new Vector2 ();
		//GetComponent<RectTransform> ().anchoredPosition = new Vector2 ();
		m_highLightImage.gameObject.SetActive(false);


		if(m_useNotImage != null)
			m_useNotImage.gameObject.SetActive (false);
	}


	void Update(){
		if (m_isContentView) {
			m_contentTime += Time.deltaTime;
			if (m_contentTime > PrepClass.c_contentViewTime) {
				m_playerCtrl.setContentsView (m_skillData, m_pos);
			}
		}
	}

	void OnEnable(){
		resetHighLight ();
	}

	public void setParent(UIPlayerCtrlClass parent){
		m_playerCtrl = parent;

//		m_playerCtrl.player.mosData
	}

	public void setDelegate(BtnDeleagte del){
		m_del = del;
	}

	public void setSkill(SkillClass skillData){
		m_skillData = skillData;
		
		setIcon (m_skillData.iconRound);
		if (m_skillData.typeSkill == TYPE_SKILL.PASSIVE)
			GetComponent<Button> ().interactable = false;
		else
			GetComponent<Button> ().interactable = true;
	}

	public void setIcon(Sprite icon){
		if (icon != null) {
			m_backgroundImage.sprite = icon;
			m_coolTimeImage.sprite = icon;
		} else {
			m_backgroundImage.sprite = null;
			m_coolTimeImage.sprite = null;
		}
	}

	public void setHighLight(){
		m_highLightImage.gameObject.SetActive (true);
	}

	public void resetHighLight(){
		m_highLightImage.gameObject.SetActive (false);
	}
		
	public bool skillCooltimeCalculate(float rate, float time){



		m_coolTimeImage.fillAmount = rate;

		if (rate != 1f) {
			m_timeText.text = string.Format ("{0:f1}", time);
		} else {
			m_timeText.text = "";
			m_skillReady = true;
		}


		return m_skillReady;
	}


	public void OnPointerDown(PointerEventData data){
		//가이드라인

		if (m_slot != -1) {
			m_isContentView = true;
			m_pos = data.position;
		}

		//버튼을 사용중인지 여부 - 사용중이면 true
		if (m_playerCtrl.isNoneTypeCtrler())
			return;

		//스킬
		if(m_skillData != null && m_skillData.typeSkill == TYPE_SKILL.PASSIVE)
			return;

		//쿨타임이 안되어 있으면
		if (m_coolTimeImage.fillAmount != 1f)
			return;


		//스킬 사용 업

		//저격수 권총이면 헤드샷 스킬 불가
//		if (m_playerCtrl.player.mos == TYPE_MOS.SNIPER) {
//			if(m_playerCtrl.player.addState.getBuff(typeof(WeaponChangeBuffDataClass)) != null){
//				if (m_slot == 1 || m_slot == 3) {
//					//					Debug.Log ("build");
//					m_playerCtrl.player.skillAction (m_slot, true);		
//					return;
//				}
//			}
//		}

		//스킬 대리자가 없으면
		if (m_del == null) {
			//스킬 가이드라인 열기
			m_playerCtrl.player.skillGuideLine (m_slot);
		} else {
			//스킬 대리자 실행
			m_del ();
		}



		//재장전버튼이면 
		if (m_slot == -1)
			return;

		//캔슬 버튼 활성화
		m_playerCtrl.cancelBtnEvent (true);
		m_playerCtrl.setTypeCtrler ((TYPE_CTRLER)m_slot);

	}


	public void OnPointerUp(PointerEventData data){
		//스킬 발동
		//가이드라인이 있으면 가이드라인에 대한 스킬 발동

		m_isContentView = false;
		m_contentTime = 0f;

		//캔슬버튼 - 재장전버튼이 위에 있으면 캔슬

		//누르고 있던 버튼이 뗀 버튼과 같지 않으면 무시
		if (!m_playerCtrl.isTypeCtrler ((TYPE_CTRLER)m_slot))
			return;

		//스킬 사용 풀림
		m_playerCtrl.resetTypeCtrler ();

		#if UNITY_ANDROID
		//캔슬 버튼 비활성화
		m_playerCtrl.cancelBtnEvent (false);

		//뗏을때 캔슬버튼 위라면 스킬 취소
		for(int i = 0; i < Input.touches.Length; i++){
			if (Input.touches [i].phase == TouchPhase.Ended){
				RaycastHit2D ray = Physics2D.Raycast (Input.touches[i].position, Vector2.zero, 1f, 1 << 5);
				if (ray.collider != null) {
					m_playerCtrl.player.closeSkillGuideLine();
					return;
				}
			}
		}

		#else

		Debug.Log ("MousePos : " + Input.mousePosition);
		RaycastHit2D ray = Physics2D.Raycast (Input.mousePosition, Vector2.zero, 1f, 1 << 5);
		if (ray.collider != null) {
			Debug.Log ("name : " + ray.collider + " cancel");
			m_playerCtrl.player.closeSkillGuideLine();
			return;
		}


		#endif

		//대리자 없으면
		if (m_del == null) {


			m_skillReady = false;

			//건축가이면
			if (m_playerCtrl.player.mos == TYPE_MOS.BUILDER) {
				if (m_slot == 1 || m_slot == 3) {
//					Debug.Log ("build");
					m_playerCtrl.player.skillAction (m_slot, true);		
					return;
				}
			}

			//마법사이면
			else if (m_playerCtrl.player.mos == TYPE_MOS.MAGICIAN) {
				if (m_slot == 2) {
					m_playerCtrl.player.skillAction (m_slot, true);		
					return;
				}
			}

			//스킬 실행
			m_playerCtrl.player.skillAction (m_slot, false);
		}


	}



}


