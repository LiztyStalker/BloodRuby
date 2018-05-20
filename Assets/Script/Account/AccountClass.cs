using System;
using System.Collections.Generic;

public class AccountClass : SingletonClass<AccountClass>
{


    //계정 내역 ReportClass
    GameReportClass m_gameReport = new GameReportClass();


    //계정 기본 AccountReportClass
    AccountReportClass m_accountReport = new AccountReportClass();


    //병과 정보 MosReportClass - 병과 아이템 기록
    MosReportClass m_mosReport = new MosReportClass();


    //업적 AchivementClass
    AchivementReportClass m_achivementReport = new AchivementReportClass();


    //UI 위치
    PlayPanelClass m_playPanel = new PlayPanelClass();





    //
    public AccountReportClass accountReport { get { return m_accountReport; } }
    public MosReportClass mosReport { get { return m_mosReport; } }
    public GameReportClass gameReport { get { return m_gameReport; } }
    public AchivementReportClass achivementReport { get { return m_achivementReport; } }
    public PlayPanelClass playPanel { get { return m_playPanel; } }
	public string nickname{ get { return m_accountReport.name; } }

	public AccountClass(){
		initAccount ();
	}

    void initAccount()
    {
        //m_report = new ReportClass();
        //m_achivement = new AchivementClass();
        //m_playPanel = new PlayPanelClass();
		gameReport.setExperiance(m_accountReport.addExperiance);
		gameReport.setBattlePoint(m_accountReport.addBattlePoint);
    }






}
