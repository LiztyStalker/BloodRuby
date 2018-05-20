using System;
using UnityEngine;
using UnityEngine.UI;

public class UIResultAccountClass : MonoBehaviour
{
	[SerializeField] Image m_icon;
	[SerializeField] Slider m_expSlider;
	[SerializeField] Text m_expText;
	[SerializeField] Text m_levelText;
	[SerializeField] Text m_nameText;
	[SerializeField] Text m_bpText;
	[SerializeField] Text m_rbText;



	void OnEnable(){
		gameResult ();
	}

	public void gameResult()
	{

		Debug.Log ("결과 보이기");

		m_icon.sprite = AccountClass.GetInstance.accountReport.icon;
		m_expSlider.value = AccountClass.GetInstance.accountReport.experianceRate;
		m_expText.text = AccountClass.GetInstance.accountReport.experianceValue;

		m_levelText.text = string.Format("Lv{0}", AccountClass.GetInstance.accountReport.level);
		m_nameText.text = AccountClass.GetInstance.accountReport.name;
		m_bpText.text = string.Format("{0}",AccountClass.GetInstance.accountReport.battlePoint);
		m_rbText.text = string.Format("{0}",AccountClass.GetInstance.accountReport.bloodRuby);

		//캐릭터 정보 레벨 아이콘 이름


		//KDA 가한데미지, 피해량
		//치유량, 점령횟수 중립횟수
		//게임 플레이 시간

		//각종 경험치 획득량
		//자주 사용한 캐릭터



	}
}


