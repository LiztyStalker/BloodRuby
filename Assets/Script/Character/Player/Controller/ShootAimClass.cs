using System;
using System.Collections;
using UnityEngine;

public class ShootAimClass : MonoBehaviour
{
	//목표 기본거리
	const float c_defaultFocusRange = 4f;
	const float c_recoilingMax = 1f; //반동력 및 쿨타임 최대치 = 100%
	const float c_recoilingRecovery = 0.075f;//반동력 및 쿨타임 회복력

	bool isRun = false;
	[SerializeField] SpriteRenderer m_focus;
	[SerializeField] LineRenderer m_mainLineRenderer;
	[SerializeField] LineRenderer m_leftLineRenderer;
	[SerializeField] LineRenderer m_rightLineRenderer;

	float m_accuracy;
	float m_recoiling = 0f; //반동력 또는 쿨타임

	public float recoiling{ get { return m_recoiling; } }

	void Start(){
//		m_aimRenderer = GetComponentsInChildren<LineRenderer> ();

		if(m_mainLineRenderer != null)
			m_mainLineRenderer.sortingLayerName = "Effect";
		if(m_leftLineRenderer != null)
			m_leftLineRenderer.sortingLayerName = "Effect";
		if(m_rightLineRenderer != null)
			m_rightLineRenderer.sortingLayerName = "Effect";

		isRun = true;
		StartCoroutine (aimRecoveryCoroutine ());
	}

	public void setShootPos(Vector2 pos){
		gameObject.transform.position = pos;
	}


	public void setWeapon(WeaponEquipmentClass weapon){
		m_accuracy = weapon.accuracy;
		//에임 길이 보정
		//근거리는 범위
		if (m_mainLineRenderer != null) {
//			m_mainLineRenderer.GetPosition (m_mainLineRenderer.positionCount - 1) = 
			float range = weapon.range;

			if (range > 5f) range = 5f;
			else if (range < 2f) range = 2f;

			m_mainLineRenderer.SetPosition(m_mainLineRenderer.positionCount - 1, new Vector3 (range, 0f, 0f));

			if (m_leftLineRenderer != null) 
				m_leftLineRenderer.SetPosition(m_leftLineRenderer.positionCount - 1, new Vector3 (range * 0.6f, 0f, 0f));
			
			if (m_rightLineRenderer != null) 
				m_rightLineRenderer.SetPosition(m_rightLineRenderer.positionCount - 1, new Vector3 (range * 0.6f, 0f, 0f));
//			Debug.Log ("m_mainLineRenderer : " + m_mainLineRenderer.positionCount);
		}

//		

		//원거리는 길이
	}

	//사격시 뚜렷하게 보이기
	//사격시 에임 벌어짐
	public void setRecoil(float recoil){
		m_recoiling += (recoil * 0.01f);
		if (m_recoiling > c_recoilingMax) m_recoiling = c_recoilingMax;
	}

	IEnumerator aimRecoveryCoroutine(){
		while (isRun) {
			m_recoiling -= c_recoilingRecovery;
			if (m_recoiling < 0f) m_recoiling = 0f;
			aimCalculator ();
			yield return new WaitForSeconds (PrepClass.c_timeGap);
		}
		
	}

	void aimCalculator(){
		//
		//기본 에임

		//recoil에 따른 추가 에임
		if (m_mainLineRenderer != null) {

			float angle = m_mainLineRenderer.transform.eulerAngles.z;


			//명중률 = 현재 명중률 - 남은명중률 * 리코일 비율
			float calAngle = PrepClass.accuracyCalculator (m_accuracy, recoiling);


			if (m_leftLineRenderer != null)
				m_leftLineRenderer.transform.eulerAngles = new Vector3 (0f, 0f, angle + calAngle);
			if (m_rightLineRenderer != null)
				m_rightLineRenderer.transform.eulerAngles = new Vector3 (0f, 0f, angle - calAngle);
		}
	}



	void OnDestroy(){
		isRun = false;
	}


	/// <summary>
	/// 에임 위치 A0.7
	/// </summary>
	/// <param name="angle">Angle.</param>
	public void aimPosition(float angle, ICharacterInterface target){
		if (target != null)
			m_focus.transform.position = target.transform.position;
		else if (m_focus != null) {
				
//				미사용 A0.7
//				float dirX = Mathf.Cos (angle * Mathf.Deg2Rad);
//				float dirY = Mathf.Sin (angle * Mathf.Deg2Rad);
//
//
//
//				Vector2 focusPos = new Vector2 (dirX, dirY);
//				Debug.Log ("focusPos : " + focusPos);
				m_focus.transform.localPosition = Vector2.right * c_defaultFocusRange;
		}
	}



}


