using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
class UIHistoryLabelClass : MonoBehaviour
{
	[SerializeField] Image m_icon;
    [SerializeField] Text m_nameText;
    [SerializeField] Text m_levelText;
    [SerializeField] Text m_kdaText;


    /// <summary>
    /// 라벨 붙이기
    /// </summary>
    /// <param name="character"></param>
    public void setLabel(UICharacterClass character)
    {
		//아이콘
		m_icon.sprite = character.icon;
		m_nameText.text = character.nickname;
		m_levelText.text = string.Format("{0}", character.level);
        m_kdaText.text = string.Format("{0}/{1}/{2}", character.getReport(TYPE_REPORT.KILL), character.getReport(TYPE_REPORT.DEAD), character.getReport(TYPE_REPORT.ASSIST));
    }

    /// <summary>
    /// 라벨 붙이기
    /// </summary>
    /// <param name="name">이름</param>
    /// <param name="level">레벨</param>
    /// <param name="kda">KDA</param>
	public void setLabel(TYPE_TEAM team, string level, string kda)
    {
		if (team == TYPE_TEAM.NONE) {
			m_nameText.text = "";
		}
		else {
			m_nameText.text = team.ToString ();
			m_nameText.color = PrepClass.getFlagColor (team);
			m_kdaText.color = PrepClass.getFlagColor (team);
		}

        m_levelText.text = level;
        m_kdaText.text = kda;
    }
}

