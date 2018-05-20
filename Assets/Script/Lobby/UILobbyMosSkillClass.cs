using System;
using UnityEngine;

public class UILobbyMosSkillClass : MonoBehaviour
{
	[SerializeField] UILobbyMosSkillDataClass[] m_skillDataPanel;

	public void setSkillData(MOSClass mosData){
		for (int i = 0; i < mosData.skillActions.Length; i++) {
			m_skillDataPanel [i].setSkillData (mosData.skillActions [i]);
		}
	}
}


