using UnityEngine;
using System.Collections;

public class UIReadyPanelClass : UIParentClass {





    [SerializeField] UIReadyTeamClass m_teamPanel;
	[SerializeField] UIReadyMOSClass m_mosPanel;
	[SerializeField] UIReadyMinimapPanelClass m_minimapPanel;
	[SerializeField] UIReadyEquipmentClass m_equipPanel;

	TYPE_MOS m_mos;
	int[] m_equipments = new int[3];
	CaptureObjectClass m_flag;


    void Start()
    {


        m_teamPanel.gameObject.SetActive(true);
        m_minimapPanel.gameObject.SetActive(false);
//		m_mosPanel.setEquipmentPanel (m_equipPanel);


    }

    //팀 정하기 - readyTeam에서 delegate로 사용
    void setTeam(TYPE_TEAM team) { 
        m_parent.team = team; 
        //자동적으로 게임 화면 보여주기
        m_teamPanel.gameObject.SetActive(false);
        m_minimapPanel.gameObject.SetActive(true);
    }


    public override void setParent(UIPlayerClass parent)
    {
        base.setParent(parent);
        m_teamPanel.setTeamDelegate(setTeam);
    }


	public void gameUpdate(GameControllerClass ctrler, TYPE_TEAM team){
        if (m_minimapPanel.isActiveAndEnabled)
            m_minimapPanel.mapUpdate(ctrler, team);
        else if (m_teamPanel.isActiveAndEnabled)
            m_teamPanel.gameUpdate(ctrler);
	}


	void getData(){
		m_mos = m_mosPanel.mos;
		m_equipments = m_equipPanel.equipments;
		m_flag = m_minimapPanel.flag;
	}


	//배치 버튼 선택시 m_player에 필요한 모든 데이터를 붙임
	/// <summary>
	/// 배치
	/// </summary>
	void gameReady(){
		getData ();
		m_parent.gameReady (m_mos, m_equipments, m_flag);
	}

	/// <summary>
	/// 배치 버튼 선택
	/// </summary>
	public void gameEnter(){
		gameReady ();
	}

	/// <summary>
	/// 팀에 맞는 거점 개수 가져오기
	/// </summary>
	/// <returns>The flag count.</returns>
	/// <param name="team">Team.</param>
	public int getFlagCount(TYPE_TEAM team){return m_minimapPanel.getFlagCount (team);}




}
