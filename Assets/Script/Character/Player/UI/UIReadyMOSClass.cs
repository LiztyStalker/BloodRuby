//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.36373
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIReadyMOSClass : MonoBehaviour
{


	[SerializeField] Toggle[] m_mosToggles;
	[SerializeField] UIMOSInforClass m_mosInforPanel;
	[SerializeField] UIContentViewClass m_contentViewPanel;
//	[SerializeField] UILobbyMosSkillClass m_skillMosPanel;

	[SerializeField] UIReadyEquipmentClass m_equipmentPanel;

//	MOSFactoryClass m_mosFactory;

	/// <summary>
	/// 선택한 병과 가져오기
	/// 기본값 검투사
	/// </summary>
	/// <value>The mos.</value>
	public TYPE_MOS mos{
		get{
			for(int i = 0; i < m_mosToggles.Length; i++){
				if(m_mosToggles[i].isOn){
					return (TYPE_MOS)(int.Parse(m_mosToggles[i].name.Substring(0, 1)));
				}
			}
			return TYPE_MOS.DUALIST;
		}
	}

	void Start(){
//		m_mosFactory = MOSFactoryClass.GetInstance;// GameObject.Find ("Game@MOSFactory").GetComponent<MOSFactoryClass>();

		if (m_mosToggles.Length > 0) {
			for (int i = 0; i < m_mosToggles.Length; i++) {
				m_mosToggles [i].targetGraphic.GetComponent<Image> ().sprite = MOSFactoryClass.GetInstance.getMOS ((TYPE_MOS)i).illustrator;
				m_mosToggles [i].onValueChanged.AddListener ((bool isOn) => MOSselectedEvent (isOn));
			}


			MOSselectedEvent (true);
		}
	}


//	public void setEquipmentPanel(UIReadyEquipmentClass equipmentPanel){
//		m_equipmentPanel = m_equipmentPanel;
//	}

	void MOSselectedEvent(bool isOn){
		for(int i = 0; i < m_mosToggles.Length; i++){
			if (m_mosToggles [i].isOn) {
				initMOS ((TYPE_MOS)i);
//				m_mosInforPanel.initMosInfor (m_mosFactory.getMOS ((TYPE_MOS)i));
//				//계정에 저장되어있는 정보로 전송
//				m_equipmentPanel.initEquipment ((TYPE_MOS)i)
				if (m_contentViewPanel.isActiveAndEnabled)
					m_contentViewPanel.closeContentView ();
			}
		}
	}


	public void initMOS(TYPE_MOS mos){
		//if(m_mosFactory == null)
		//	m_mosFactory = MOSFactoryClass.GetInstance; //GameObject.Find ("Game@MOSFactory").GetComponent<MOSFactoryClass>();
		
		Debug.Log ("mos : " + mos);
		m_mosInforPanel.initMosInfor (MOSFactoryClass.GetInstance.getMOS (mos));
		m_equipmentPanel.initEquipment (mos);
	}




}


