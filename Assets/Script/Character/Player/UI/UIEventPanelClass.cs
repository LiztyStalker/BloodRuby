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

public enum TYPE_EVENT_PANEL{}

public class UIEventPanelClass : MonoBehaviour
{



	[SerializeField] UIReadyMinimapPanelClass m_readyMinimapPanel;
	[SerializeField] GameObject m_aimPanel;

	//승리화면
	//패배화면
	//게임종료화면
	//배치화면
	//미니맵화면
	//카운트다운화면
	//...


	public void gameUpdate(GameControllerClass ctrler){
		m_readyMinimapPanel.mapUpdate (ctrler, TYPE_TEAM.NONE);
	}

	public void setAim(bool isTelescope){
		m_aimPanel.SetActive (isTelescope);
	}

}


