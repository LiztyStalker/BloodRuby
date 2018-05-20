using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIBuffIconClass : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

	[SerializeField] Image m_backgroundImage;
	[SerializeField] Image m_coolTimeImage;
	[SerializeField] Image m_highLightImage;
	[SerializeField] Text m_timeText;

	UIPlayerCtrlClass m_playerCtrl;
	BuffDataClass m_buffData;
	Vector2 m_pos;

	float m_contentTime = 0f;
	bool m_isContentView = false;

	public BuffDataClass buffData{get{return m_buffData;}}

	public void buffUpdate (){
		
		cooltimeCalculate (PrepClass.ratioCalculator(buffData.maxTime - buffData.runTime, buffData.maxTime) , buffData.maxTime - buffData.runTime);
	}


	void Update(){
		if (m_isContentView) {
			m_contentTime += Time.deltaTime;
			if (m_contentTime > PrepClass.c_contentViewTime) {
				m_playerCtrl.setContentsView (m_buffData, m_pos);
			}
		}
	}

	public void setBuff(BuffDataClass buffData, UIPlayerCtrlClass playerCtrl){
		
		m_playerCtrl = playerCtrl;

		if (buffData == null) {
			m_buffData = null;
			setIcon (null);
			gameObject.SetActive (false);
		} else {
			m_buffData = buffData;
			setIcon (buffData.icon);
			gameObject.SetActive (true);
		}
	}

	void setIcon(Sprite icon){
		m_backgroundImage.sprite = icon;
		m_coolTimeImage.sprite = icon;
	}

	void setHighLight(){
		m_highLightImage.gameObject.SetActive (true);
	}

	void resetHighLight(){
		m_highLightImage.gameObject.SetActive (false);
	}

	public void cooltimeCalculate(float rate, float time){


		m_coolTimeImage.fillAmount = rate;


		switch (buffData.buffState) {
		case TYPE_BUFF_STATE.TIME:
			m_timeText.text = string.Format ("{0:f1}", time);
			break;
		case TYPE_BUFF_STATE.COUNT:
			m_timeText.text = string.Format ("{0}", buffData.count);
			break;
		default:
			m_timeText.text = "";
			break;
		}



//		if (rate != 1f) {
//			m_timeText.text = string.Format ("{0:f1}", time);
//		} else {
//			m_timeText.text = "";
//		}
	}

	/// <summary>
	/// 버프 설명 보이기
	/// </summary>
	/// <param name="data">Data.</param>
	public void OnPointerDown(PointerEventData data){
		m_isContentView = true;
		m_pos = data.position;
	}

	public void OnPointerUp(PointerEventData data){
		m_isContentView = false;
		m_contentTime = 0f;
		m_pos = Vector2.zero;
	}

}


