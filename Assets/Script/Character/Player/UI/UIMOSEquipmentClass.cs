using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class UIMOSEquipmentClass : MonoBehaviour
{
	[SerializeField] TYPE_EQUIPMENT m_typeEquipment;

	[SerializeField] Text m_equipNameText;
	[SerializeField] Image m_equipIcon;
	[SerializeField] Text[] m_titleTexts;
	[SerializeField] Text[] m_contentsTexts;
	[SerializeField] Toggle[] m_equipBtns;

	[SerializeField] Sprite m_emptyIcon;

	EquipmentFactoryClass m_equipFactory;
	TYPE_MOS m_mos;

	List<Toggle> m_toggleList = new List<Toggle> ();

	public int equipSlot{get{return getEquipmentSlot();}}

	//원거리
	//근거리


	//갑옷

	//액세서리




	void factoryLink(){


		m_equipFactory = EquipmentFactoryClass.GetInstance; //GameObject.Find ("Game@EquipFactory").GetComponent<EquipmentFactoryClass> ();

		foreach(Toggle tg in m_equipBtns){
			tg.onValueChanged.AddListener((bool isOn) => equipmentChanged(isOn));
		}


	}


	/// <summary>
	/// 장비 선택 슬롯 가져오기
	/// </summary>
	/// <returns>The equipment slot.</returns>
	int getEquipmentSlot(){
		for (int i = 0; i < m_equipBtns.Length; i++) {
			if (m_equipBtns [i].isOn)
				return i;
		}
		return 0;
	}


	/// <summary>
	/// 캐릭터 선택
	/// </summary>
	/// <param name="mos">Mos.</param>
	public void initMOS(TYPE_MOS mos){

		m_mos = mos;

		//toggleList 임의생성 - 기본 수보다 많으면 새로 생성
		//toggleList 가 0이 아니면 값만 변환

		if (m_equipFactory == null) {
			factoryLink ();
		}

		for (int i = 0; i < m_equipBtns.Length; i++) {
//			Debug.Log (" i " + i);
			EquipmentClass equip = m_equipFactory.getEquipment (m_mos, m_typeEquipment, i);

			if (equip != null) {
				m_equipBtns [i].targetGraphic.GetComponent<Image> ().sprite = equip.equipIcon;
				m_equipBtns [i].interactable = true;
			} else {
				m_equipBtns [i].targetGraphic.GetComponent<Image> ().sprite = m_emptyIcon;
				m_equipBtns [i].interactable = false;
			}
			m_equipBtns [i].name = string.Format ("{0}_{1}", i, m_typeEquipment);
			initEquipment (equip);
			//액세서리는 default가 있음
		}

		for (int i = 0; i < m_equipBtns.Length; i++) {
			if (m_equipBtns [i].targetGraphic.GetComponent<Image> ().sprite == m_emptyIcon) {
				m_equipBtns [0].isOn = true;
				equipmentChanged (true);
				break;
			}
			else{
				if (m_equipBtns [i].isOn) {
					equipmentChanged (true);
					break;
				}
			}
		}

	}


	void equipmentChanged(bool isOn){
		initEquipment(m_equipFactory.getEquipment (m_mos, m_typeEquipment, getEquipmentSlot ()));
	}

	string valueToString(float value){
		if(value > 0f) return string.Format ("+{0}", value);
		return string.Format ("{0}", value);
	}

	/// <summary>
	/// 장비선택
	/// </summary>
	/// <param name="equipment">Equipment.</param>
	void initEquipment(EquipmentClass equipment){

		if (equipment == null) {
			m_equipIcon.sprite = null;
			m_equipNameText.text = "-";

			for (int i = 0; i < m_titleTexts.Length; i++) {
				m_titleTexts [i].text = "-";
				m_contentsTexts [i].text = "-";
			}
		} else {

			m_equipIcon.sprite = equipment.equipIcon;
			m_equipNameText.text = equipment.equipName;

			switch (equipment.equipType) {
			case TYPE_EQUIPMENT.WEAPON:
				m_titleTexts [0].text = "공격력";
				m_contentsTexts [0].text = string.Format("{0}", ((WeaponEquipmentClass)equipment).damage);
				m_titleTexts [1].text = "공격속도";
				m_contentsTexts [1].text = ((WeaponEquipmentClass)equipment).shootDelay + "s";

				if (((WeaponEquipmentClass)equipment).typeRange == TYPE_RANGE.LONG) {
					m_titleTexts [2].text = "장탄수";
					m_contentsTexts [2].text = string.Format("{0}", ((WeaponEquipmentClass)equipment).ammo);

				} else {
					m_titleTexts [2].text = "사정거리";
					m_contentsTexts [2].text = string.Format("{0}", ((WeaponEquipmentClass)equipment).range);
				}

				m_titleTexts [3].text = "옵션";
				if (((WeaponEquipmentClass)equipment).option != null)
					m_contentsTexts [3].text = ((WeaponEquipmentClass)equipment).option.contents;
				else
					m_contentsTexts [3].text = "";
				break;

			case TYPE_EQUIPMENT.ARMOR:
				m_titleTexts [0].text = "체력";
				m_contentsTexts [0].text = valueToString((float)((ArmorEquipmentClass)equipment).health);
				m_titleTexts [1].text = "이동속도";
				m_contentsTexts [1].text = valueToString(((ArmorEquipmentClass)equipment).moveSpeed);
				m_titleTexts [2].text = "옵션";
				m_contentsTexts [2].text = "";

				if (((ArmorEquipmentClass)equipment).option != null)
					m_contentsTexts [2].text = ((ArmorEquipmentClass)equipment).option.contents;
				else
					m_contentsTexts [2].text = "";
				

			//체력
			//이동속도
			//옵션
				break;

			case TYPE_EQUIPMENT.ACCESSERY:
			//옵션
				m_titleTexts [0].text = "옵션";
				if (((AccesseryEquipmentClass)equipment).option != null)
					m_contentsTexts [0].text = ((AccesseryEquipmentClass)equipment).option.contents;
				else
					m_contentsTexts [0].text = "";

				break;
			}
		}
	}
}


