using System;
using UnityEngine;
using UnityEngine.UI;

public class UISkillInforBtnClass : MonoBehaviour
{
	[SerializeField] UIContentViewClass m_contentViewPanel;
	SkillClass m_skillData;

	public void setSkill(SkillClass skillData){
		m_skillData = skillData;
		if (m_skillData != null) {
			gameObject.GetComponent<Button> ().targetGraphic.GetComponent<Image> ().sprite = m_skillData.iconRect;
			gameObject.GetComponent<Button> ().onClick.AddListener (() => skillInforClicked ());
		}
	}

	void skillInforClicked(){
		if(m_skillData != null)
			m_contentViewPanel.setContentView (m_skillData, transform.position);
	}

}


