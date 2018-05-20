using System;
using UnityEngine;
using UnityEngine.UI;

class UIReadyTeamClass : MonoBehaviour
{
    //팀선택후 바로 준비창으로 이동
    //개인전이면 팀선택 없음

    [SerializeField] UITicketDataClass m_ticketPanel;
	[SerializeField] Button m_leftTeamBtn;
	[SerializeField] Button m_rightTeamBtn;


    public delegate void teamDelegate(TYPE_TEAM team);
    teamDelegate m_del;


	void Start(){
		m_leftTeamBtn.GetComponentInChildren<Text> ().color = PrepClass.getFlagColor (TYPE_TEAM.TEAM_0);
		m_rightTeamBtn.GetComponentInChildren<Text>().color = PrepClass.getFlagColor (TYPE_TEAM.TEAM_1);
	}

    public void gameUpdate(GameControllerClass ctrler) {
        m_ticketPanel.gameUpdate(ctrler);
    }

    public void setTeamDelegate(teamDelegate del) {
        m_del = del;
    }


    public void leftTeamClick(){
        m_del(TYPE_TEAM.TEAM_0);
    }

    public void rightTeamClick()
    {
        m_del(TYPE_TEAM.TEAM_1);
    }

    public void randomTeamClick()
    {
        m_del((TYPE_TEAM)UnityEngine.Random.Range(0, 2));
    }


}

