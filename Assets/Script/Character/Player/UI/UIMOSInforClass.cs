using System;
using UnityEngine;
using UnityEngine.UI;

public class UIMOSInforClass : MonoBehaviour
{
	[SerializeField] Text m_mosText;
	[SerializeField] Image m_mosIcon;
	[SerializeField] Text m_health;
	[SerializeField] Text m_moveSpeed;
	[SerializeField] Text m_contents;

	[SerializeField] Button[] m_skillImage;

	[SerializeField] UILobbyMosSkillClass m_skillMosPanel;

	public void initMosInfor(MOSClass mosData){


		m_mosText.text = TranslatorClass.GetInstance.mosTranslator (mosData.mos);
		m_mosIcon.sprite = mosData.illustrator;

		m_health.text = mosData.health.ToString();
		m_moveSpeed.text = mosData.speed.ToString();
		m_contents.text = mosData.contents;

		if (m_skillImage [0].GetComponent<UISkillInforBtnClass> () != null) {
			for (int i = 0; i < m_skillImage.Length; i++) {
				m_skillImage [i].GetComponent<UISkillInforBtnClass> ().setSkill (mosData.skillActions [i]);
//			m_skillImage [i].targetGraphic.GetComponent<Image> ().sprite = mosData.skillActions [i].iconRect;
			}
		} else {
			for (int i = 0; i < m_skillImage.Length; i++) {
				m_skillImage [i].targetGraphic.GetComponent<Image> ().sprite = mosData.skillActions [i].iconRect;
			}

		}

		if (m_skillMosPanel != null) {
			m_skillMosPanel.setSkillData (mosData);
		}
	}

}


