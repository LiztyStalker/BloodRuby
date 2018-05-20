using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public enum TYPE_JOYSTICK{MOVE, VIEW}

public class UIJoystickClass : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler {
	

	const float c_radiusOffset = 0.3f;
	
	private ICharacterInterface m_player;
	private UIJoystickClass m_mateJoystick;
	[SerializeField] TYPE_JOYSTICK m_joystick;
	
	
	//PrepScript.TYPE_JOYSTICK_MODE joystickMode = PrepScript.TYPE_JOYSTICK_MODE.PANEL;
	
//	[SerializeField] private GameObject activeSkillBtn; //스킬버튼L 활성화
	
	
//	[SerializeField] private GameObject displayJoyStickPanel; //조이스틱 패널
//	[SerializeField] private GameObject displayJoyStickActionPanel; //조이스틱 행동 패널
//	[SerializeField] private RectTransform joystickPanel;
	[SerializeField] private RectTransform joystickActionPanel;
	//	[SerializeField] GameObject arrowJoystickPanel; //화살표 패널
	//	[SerializeField] GameObject arrowJoystickActionPanel;
	
	
//	[SerializeField] private GameObject logTextPanel;
	
	//이미지와 오브젝트 각도간 오차 각
	const float c_angleOffset = 0f;
	
	Vector2 nowPos = Vector2.zero; //현재 위치
	Vector3 dragPos = Vector3.zero; //드래그 위치
	bool isMove = false; //움직임 여부

	public bool joystickUse{ get { return isMove; } }

	Vector3 defaultCenter; //기본 위치
	Vector3 axis; //단위벡터 축
	
	float radius; //반지름
	
//	public bool isPause{ get { return m_isPause; } set { m_isPause = value; } }
	public void setPlayer(PlayerClass player, UIJoystickClass mateJoystick){
		m_player = player;
		m_mateJoystick = mateJoystick;
//		activeSkillBtn.GetComponent<Button> ().onClick.AddListener (new UnityAction (player.activeFire));
	}
	
	// Use this for initialization
	void Start () {
		//player = PrepScript.OBJ_PLAYER.GetComponent<PlayerScript>();// GameObject.FindGameObjectWithTag (PrepScript.TAG_PLAYER).GetComponent<PlayerScript> ();
		
		
		//플레이어 정보를 가지고 왔으면 스킬 이미지 삽입 필요
		//캐릭터 스킬 이벤트 등록

		//반지름 = 조이스틱 패널 세로 * 0.5f
//		Debug.Log ("default : " + defaultCenter);
		
		//joystickSelected ();
		//skillBtnPosSelected ();
//		float height = Screen.height * 0.33f;
//		GetComponent<RectTransform> ().sizeDelta = new Vector2 (height, height);
//
//		if(GetComponent<RectTransform> ().anchoredPosition.x > 0f)
//			GetComponent<RectTransform> ().anchoredPosition = new Vector2 (height * 0.5f + 20f, height * 0.5f + 20f);
//		else
//			GetComponent<RectTransform> ().anchoredPosition = new Vector2 (-height * 0.5f - 20f, height * 0.5f + 20f);
//
//
//		joystickActionPanel.sizeDelta = new Vector2 (height * 0.35f, height * 0.35f);


		radius = GetComponent<RectTransform> ().sizeDelta.x * c_radiusOffset;
		//조이스틱 패널 중앙 = 조이스틱 패널 중앙 위치
		defaultCenter = transform.position;

		//Debug.Log("DefaultCenter : " + defaultCenter);

	}
	
	
	
	// Update is called once per frame
	void FixedUpdate () {

		if (isMove) {
			joystickMove (dragPos);
		}

		if (m_joystick == TYPE_JOYSTICK.MOVE) {

			float dirX = Input.GetAxis ("Horizontal");
			float dirY = Input.GetAxis ("Vertical");

			if (dirX == 0f && dirY == 0f) return;

			float angle = Mathf.Atan2 (dirY, dirX) * Mathf.Rad2Deg;
			m_player.moveAction (dirX, dirY);
			m_player.viewAction(angle);

//			if (!m_mateJoystick.joystickUse) {
//				//				Debug.Log ("angle : " + angle);
//				m_player.viewAction (angle);
//			}

		}
			
		
	}
	
	
//	public void skillBtnPosSelected(){
//		RectTransform skillBtnRect = activeSkillBtn.GetComponent<RectTransform> ();
//		if (!PrepScript.SET_SKILL_BTN_POS) {
//			skillBtnRect.anchorMin = new Vector2(1f, 0f);
//			skillBtnRect.anchorMax = new Vector2(1f, 0f);
//			skillBtnRect.pivot = new Vector2(1f, 0f);
//			
//			float skillDirX = Screen.width - 20f;
//			Debug.Log("pos " + skillBtnRect.position);
//			skillBtnRect.position = new Vector2(skillDirX, 20f);
//			Debug.Log("pos " + skillBtnRect.position);
//			
//		} else {
//			skillBtnRect.anchorMin = new Vector2(0f, 0f);
//			skillBtnRect.anchorMax = new Vector2(0f, 0f);
//			skillBtnRect.pivot = new Vector2(0f, 0f);
//			skillBtnRect.position = new Vector2(20f, 20f);
//			
//		}
//	}
	
//	public void joystickSelected(){
//		//		joystickStop ();
//		switch (PrepScript.joystickMode) {
//		case PrepScript.TYPE_JOYSTICK_MODE.DISPLAY:
//			joystickPanel.SetActive(false);
//			joystickActionPanel.SetActive(false);
//			break;
//			
//		case PrepScript.TYPE_JOYSTICK_MODE.JOYSTICK:
//			joystickPanel.SetActive(true);
//			joystickActionPanel.SetActive(true);
//			displayJoyStickPanel.SetActive(false);
//			displayJoyStickActionPanel.SetActive(false);
//			break;
//		}
//	}
	
//	private void touchAction(Vector2 touchPos, TouchPhase touchPhase){
//		
//		
//		//조이스틱 제약
//		//첫 터치일때 4가 넘으면 무시
//		//움직이고 있을때 4가 넘으면 가능
//		//
//		
//		
//		//조이스틱
//		//터치 입력이 있으면 Begin 시작
//		//드래그하면 Move
//		//떼면 Stop
//		
//		//
//		
//		touchPos.y -= Camera.main.transform.position.y;
//		
//		
//		//		Debug.Log("Pos : " + touchPos);
//		
////		if (m_isPause)
////			return;		
////		switch (touchPhase) {
////		case TouchPhase.Began:
////			joystickStart();
////			break;
////		case TouchPhase.Stationary:
////			break;
////		case TouchPhase.Moved:
////			if(isMove){
////				joystickMove();
////			}
////			break;
////		case TouchPhase.Ended:
////			if(isMove){
////				joystickStop();
////			}
////			break;
////		case TouchPhase.Canceled:
////			goto case TouchPhase.Ended;
////		}
//	}
	
	
	//	public void joystickBegin(){}

	public void joystickStart(){
		isMove = true;
	}

	public void joystickMove(Vector2 drag){
		//if (!m_isPause) {

		Vector2 touchPos = drag;

		//벡터 정규화 0~1 (단위벡터로 만들어 방향성만 부여)
		axis = ((Vector3)touchPos - defaultCenter).normalized;

		//거리
		float distance = Vector3.Distance (touchPos, defaultCenter);

		//반지름보다 크면
		if (distance > radius)
			//최대값
			joystickActionPanel.position = defaultCenter + axis * radius;
		else
			//거리
			joystickActionPanel.position = defaultCenter + axis * distance;
		
		//isMove = true;
		
		float dirX = (joystickActionPanel.position.x - defaultCenter.x) / radius;
		float dirY = (joystickActionPanel.position.y - defaultCenter.y) / radius;



		float angle = Mathf.Atan2 (dirY, dirX) * Mathf.Rad2Deg - c_angleOffset;
            

		switch (m_joystick) {
		case TYPE_JOYSTICK.MOVE:
			//플레이어 이동 명령
			m_player.moveAction (dirX, dirY);

			//사격조이스틱에 손을 대지 않았을 경우
			if (!m_mateJoystick.joystickUse) {
				m_player.viewAction (angle);
			}
			break;
		case TYPE_JOYSTICK.VIEW:
			//플레이어 바라보기 명령
			m_player.viewAction(angle);

			//플레이어 슈팅 명령
			m_player.attackAction(angle);
			((PlayerClass)m_player).cameraAction (dirX, dirY);
			break;
		}

	}
	
	public void joystickStop(){
		joystickActionPanel.position = defaultCenter;
		isMove = false;

		switch (m_joystick) {
		case TYPE_JOYSTICK.MOVE:
			m_player.moveAction(0f, 0f);
			break;
		case TYPE_JOYSTICK.VIEW:
			((PlayerClass)m_player).cameraAction (0f, 0f);
			break;
		}
	}
	


	public void OnPointerDown(PointerEventData data){

		if (m_joystick == TYPE_JOYSTICK.VIEW) {
			if (setBtnClicked (0.75f)) {
				m_player.attackAction(0f);
				return;
			}
		} 

		dragPos = data.position;
		joystickStart ();

	}

	public void OnPointerUp(PointerEventData data){

		if (m_joystick == TYPE_JOYSTICK.VIEW) {
			if (setBtnClicked ()) {
				return;
			}
		} 
		dragPos = Vector3.zero;
		joystickStop ();
	}


	bool setBtnClicked(float scale = 1f){
		if (m_player.GetType () == typeof(PlayerClass)) {
			if (((PlayerClass)m_player).isTelescope) {
				joystickActionPanel.localScale = Vector2.one * scale;
				return true;
			}
		}
		return false;
	}

	public void OnDrag(PointerEventData data){
		dragPos = data.position;
	}


	//	public void arrowJoystickBegin(){}
	/*
	public void arrowJoystickMove(){
		joystickMove ();
	}

	public void arrowJoystickStop(){
		joystickStop ();
	}
*/


	void OnDisable(){
		if(m_player != null) OnPointerUp (null);
	}

}
