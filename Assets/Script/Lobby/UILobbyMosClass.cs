using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UILobbyMosClass : UILobbyParentClass//, IPointerDownHandler
{
	[SerializeField] Button[] m_mosButton;
//	[SerializeField] UILobbyMosInforClass m_mosInforPanel;
//	[SerializeField] UIReadyEquipmentClass m_equipmentPanel;

	MOSFactoryClass m_mosFactory;

	/// <summary>
	/// 선택한 병과 가져오기
	/// 기본값 검투사
	/// </summary>
	/// <value>The mos.</value>
//	public TYPE_MOS mos{
//		get{
//			for(int i = 0; i < m_mosButton.Length; i++){
//				if(m_mosButton[i].isOn){
//					return (TYPE_MOS)(int.Parse(m_mosButton[i].name.Substring(0, 1)));
//				}
//			}
//			return TYPE_MOS.DUALIST;
//		}
//	}

	void Start(){
		m_mosFactory = MOSFactoryClass.GetInstance; //GameObject.Find ("Game@MOSFactory").GetComponent<MOSFactoryClass>();

		for(int i = 0; i < m_mosButton.Length; i++){
			m_mosButton [i].targetGraphic.GetComponent<Image> ().sprite = m_mosFactory.getMOS ((TYPE_MOS)i).illustrator;
			m_mosButton [i].GetComponentInChildren<Text> ().text = TranslatorClass.GetInstance.mosTranslator ((TYPE_MOS)i);
//			m_mosButton [i].onClick.AddListener(() => MOSselectedEvent());
		}


		//MOSselectedEvent (true);
	}


	//	public void setEquipmentPanel(UIReadyEquipmentClass equipmentPanel){
	//		m_equipmentPanel = m_equipmentPanel;
	//	}

//	void MOSselectedEvent(bool isOn){
//		for(int i = 0; i < m_mosButton.Length; i++){
//			if (m_mosButton [i].isOn) {
//				m_mosInforPanel.initMosInfor (m_mosFactory.getMOS ((TYPE_MOS)i));
//				//계정에 저장되어있는 정보로 전송
//				m_equipmentPanel.initEquipment ((TYPE_MOS)i);
//			}
//		}
//	}

//	public void OnPointerDown(PointerEventData data){
//		Debug.Log (data.pointerEnter.name);
//
//
//	}

}

