using System;
using UnityEngine;
using UnityEngine.UI;

public class UILobbyAccountSimpleClass : MonoBehaviour
{
	[SerializeField] Image m_icon;
	[SerializeField] Text m_nicknameText;
	[SerializeField] Text m_battlePointText;
	[SerializeField] Text m_rubyText;
	[SerializeField] Text m_levelText;
	[SerializeField] Text m_expText;
	[SerializeField] Slider m_expSlider;




	void OnEnable(){
		accountUpdate ();
	}


	public void accountUpdate(){
		m_icon.sprite = AccountClass.GetInstance.accountReport.icon;
		m_nicknameText.text = AccountClass.GetInstance.accountReport.name;
		m_battlePointText.text = string.Format ("{0}", AccountClass.GetInstance.accountReport.battlePoint);
		m_rubyText.text = string.Format ("{0}", AccountClass.GetInstance.accountReport.bloodRuby);
		m_levelText.text = string.Format ("Lv{0}", AccountClass.GetInstance.accountReport.level);

		m_expText.text = AccountClass.GetInstance.accountReport.experianceValue;
		m_expSlider.value = AccountClass.GetInstance.accountReport.experianceRate;
	}
}


 