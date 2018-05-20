using System;
using UnityEngine;
using UnityEngine.UI;

public class UILobbyMosSkillDataClass : MonoBehaviour
{
	[SerializeField] Image m_skillIcon;
	[SerializeField] Text m_skillName;
	[SerializeField] Text m_skillType;
	[SerializeField] Text m_skillTime;
	[SerializeField] Text m_skillContents;


	public void setSkillData(SkillClass skillData){
		m_skillIcon.sprite = skillData.iconRect;
		m_skillName.text = skillData.name;
		m_skillType.text = skillData.typeSkill.ToString ();
		if(skillData.coolTime == 0f)
			m_skillTime.text = "-";
		else
			m_skillTime.text = skillData.coolTime.ToString () + "s";
		m_skillContents.text = skillData.contents;
	}

}

