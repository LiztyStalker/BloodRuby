using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MosReportStruct
{
    public TYPE_MOS s_mos;
    public TimeSpan s_time;
    public Dictionary<TYPE_REPORT, int> s_report;
}



public enum TYPE_REPORT{
    KILL, //K 100
    DEAD, //D 10
    ASSIST, //A 50
    DMG_GET, //가한 피해
    DMG_SET, //받은 피해
    HEAL_GET, //가한 치유
    HEAL_SET, //받은 치유
    REB_GET, //가한 부활
    REB_SET, //받은 부활
    FLAG_NAT, //거점 중립
    FLAG_CAP, //거점 획득
    FLAG_DEF, //거점 방어
    MOVE, //이동거리
    KILL_CHAIN, //연속킬
	ITEM_GET, //아이템 획득
	RUBY_GET, //루비 획득
	RUBY_USE, //루비 사용
	BTP_GET, //배틀포인트 획득
	BTP_USE, //배틀포인트 사용
	ESCAPE, //탈주 횟수
	ESCAPE_POINT, //탈주 포인트
}

public class GameReportClass
{

	const float c_bpRate = 0.001f;

	public delegate int IntDelegate(int exp);
	IntDelegate m_expDelegate;
	IntDelegate m_bpDelegate;

    //public int[] m_report = new int[Enum.GetValues(typeof(TYPE_REPORT)).Length];

    Dictionary<TYPE_MOS, MosReportStruct> m_mosReport = new Dictionary<TYPE_MOS, MosReportStruct>();
//        new MosReportStruct[Enum.GetValues(typeof(TYPE_MOS)).Length];

	public int m_victory = 0;
	public int m_defeat = 0;
	public int m_draw = 0;

    public Dictionary<TYPE_MOS, MosReportStruct> mosReport { get { return m_mosReport; } }

    public string totalKDA { get { return string.Format("{0}/{1}/{2}", totalKillCal(), totalDeathCal(), totalAssistCal()); } }
    public TimeSpan totalTime { get { return totalTimeCal(); } }
    public string totalDamage { get { return string.Format("{0}/{1}", totalSetDamage(), totalGetDamage()); } }
    public string totalHealth { get { return string.Format("{0}/{1}", totalSetHealth(), totalGetHealth()); } }
	public string totalFlag { get { return string.Format("{0}/{1}/{2}", totalCapture(), totalNatural(), totalDefence()); } }

	public int victory{ get { return m_victory; } }
	public int defeat{ get { return m_defeat; } }
	public int draw{ get { return m_draw; } }

	TimeSpan m_playTime;

	public GameReportClass(){initReport();}


	public void setExperiance(IntDelegate del){
		m_expDelegate = del;
	}

	public void setBattlePoint(IntDelegate del){
		m_bpDelegate = del;
	}

	/// <summary>
	/// 총합 시간 계산
	/// </summary>
	/// <returns>The time cal.</returns>
    TimeSpan totalTimeCal()
    {
        return m_mosReport.Values.Select(mos => mos.s_time).ToArray<TimeSpan>().Aggregate(TimeSpan.Zero, (t1, t2) => t1 + t2);
    }

	/// <summary>
	/// 총합 킬수
	/// </summary>
	/// <returns>The kill cal.</returns>
    int totalKillCal(){
        return totalReport(TYPE_REPORT.KILL);
    }

    int totalDeathCal()
    {
        return totalReport(TYPE_REPORT.DEAD);
    }

    int totalAssistCal()
    {
        return totalReport(TYPE_REPORT.ASSIST);
    }

    int totalSetDamage()
    {
        return totalReport(TYPE_REPORT.DMG_SET);
    }

    int totalGetDamage()
    {
        return totalReport(TYPE_REPORT.DMG_GET);
    }

    int totalSetHealth()
    {
        return totalReport(TYPE_REPORT.HEAL_SET);
    }

    int totalGetHealth()
    {
        return totalReport(TYPE_REPORT.HEAL_GET);
    }

    int totalCapture()
    {
        return totalReport(TYPE_REPORT.FLAG_CAP);
    }

    int totalNatural()
    {
        return totalReport(TYPE_REPORT.FLAG_NAT);
    }

	int totalDefence(){
		return totalReport(TYPE_REPORT.FLAG_DEF);
	}

    int totalReport(TYPE_REPORT report)
    {
        return m_mosReport.Values.Sum(mos => mos.s_report[report]);

    }




    void initReport()
    {
        for (int i = 0; i < Enum.GetValues(typeof(TYPE_MOS)).Length; i++)
        {

            MosReportStruct tmpRep = new MosReportStruct();

            tmpRep.s_mos = (TYPE_MOS)i;
            tmpRep.s_time = TimeSpan.Zero;
            tmpRep.s_report = new Dictionary<TYPE_REPORT, int>();

            for (int j = 0; j < Enum.GetValues(typeof(TYPE_REPORT)).Length; j++)
            {
                tmpRep.s_report.Add((TYPE_REPORT)j, 0);
            }


            m_mosReport.Add(tmpRep.s_mos, tmpRep);

        }
    }




    /// <summary>
    /// 리포트 설정 후 가져오기
    /// </summary>
    /// <param name="value"></param>
    /// <param name="typeReport"></param>
    /// <returns></returns>
    public int addReport(int value, TYPE_MOS mos, TYPE_REPORT typeReport)
    {
        if (m_mosReport.ContainsKey(mos))
        {
            m_mosReport[mos].s_report[typeReport] += value;
            return getReport(mos, typeReport);
        }
        return -1;
    }

	public int addReport(TYPE_GAMEEND typeGameEnd){
		switch (typeGameEnd) {
		case TYPE_GAMEEND.VICTORY:
			return ++m_victory;
		case TYPE_GAMEEND.DRAW:
			return ++m_draw;
		case TYPE_GAMEEND.DEFEAT:
			return ++m_defeat;
		}
		return -1;
	}

	/// <summary>
	/// 병과 데이터 구조 합산
	/// </summary>
	/// <param name="mosReport">Mos report.</param>
    void addReport(MosReportStruct mosReport)
    {
		
        if (m_mosReport.ContainsKey(mosReport.s_mos))
        {
			m_mosReport[mosReport.s_mos].s_time += mosReport.s_time;

            foreach (TYPE_REPORT report in mosReport.s_report.Keys)
            {
                m_mosReport[mosReport.s_mos].s_report[report] += mosReport.s_report[report];
            }

        }
    }

	/// <summary>
	/// 가장 많이 사용한 병과 가져오기
	/// </summary>
	/// <returns>The best MOS time.</returns>
	public TYPE_MOS getBestMOS(){
		return m_mosReport.OrderByDescending (reportData => reportData.Value.s_time).First ().Value.s_mos;
	}

    /// <summary>
    /// 병과별 리포트 가져오기
    /// </summary>
    /// <param name="typeReport"></param>
    /// <returns></returns>
    public int getReport(TYPE_MOS mos, TYPE_REPORT typeReport)
    {
        if (m_mosReport.ContainsKey(mos))
        {
            return m_mosReport[mos].s_report[typeReport];
        }
        return -1;
    }

	/// <summary>
	/// 리포트 타입 합계 가져오기
	/// </summary>
	/// <returns>The report.</returns>
	/// <param name="typeReport">Type report.</param>
    public int getReport(TYPE_REPORT typeReport)
    {
        return m_mosReport.Sum(rep => rep.Value.s_report[typeReport]);
    }

    /// <summary>
    /// 결과 리포트 합산
    /// </summary>
    /// <param name="report"></param>
    /// <returns></returns>
    public int addReport(GameReportClass report)
    {
		int experiance = 0;


		experiance += getExperianceValue (report.getGameResult());

		//병과에 대한 모든 값 가져오기
        foreach (MosReportStruct mosRep in report.mosReport.Values)
        {

            //Debug.Log("cnt : " + mosRep.s_report.Keys.Count);

			m_mosReport [mosRep.s_mos].s_time += mosRep.s_time;
			experiance += getExperianceValue (mosRep.s_time);

			//모든 기록 가져오기
            foreach(TYPE_REPORT rep in mosRep.s_report.Keys){
				m_mosReport [mosRep.s_mos].s_report [rep] += report.getReport (mosRep.s_mos, rep);
				experiance += getExperianceValue (rep, report.getReport (mosRep.s_mos, rep));
//				Debug.Log ("resultReport : " + m_mosReport [mosRep.s_mos].s_report [rep] + " " + rep);
				//경험치 총 정산
            }
        }

		//경험치 레벨로 전환
		m_expDelegate(experiance);

		//경험치 배틀포인트로 전환
		m_bpDelegate ((int)((float)experiance * c_bpRate));

        return 0;
    }

    /// <summary>
    /// 합산 데이터 가져오기
    /// </summary>
    /// <returns></returns>
    public GameReportClass totalReport()
    {
        GameReportClass totalReport = new GameReportClass();

        foreach (MosReportStruct mosrep in m_mosReport.Values)
        {
            totalReport.addReport(mosrep);
        }
        return totalReport;

    }

	/// <summary>
	/// 게임시작 타임
	/// </summary>
	/// <param name="startTime">Start time.</param>
	public void startTime(TimeSpan startTime){
		//병과 타이머 시작 - 현재 게임시간 기억
		//시작시간 등록
		m_playTime = TimeSpan.Zero;
		m_playTime += startTime;
		Debug.Log ("StartTime : " + m_playTime);
	}

	/// <summary>
	/// 게임종료 타임
	/// </summary>
	/// <param name="mos">Mos.</param>
	/// <param name="endTime">End time.</param>
	public void endTime(TYPE_MOS mos, TimeSpan endTime){
		//병과 일시정지 타이머 종료 - 
		//병과 타이머 종료
		//병과 타이머 기록
		m_playTime -= endTime;
		m_mosReport [mos].s_time += m_playTime;
		Debug.Log ("playTime : " + m_playTime);

	}


	int getExperianceValue(TYPE_REPORT typeReport, int value){
		switch (typeReport) {
		case TYPE_REPORT.KILL:
			return value * 100;
		case TYPE_REPORT.DEAD:
			return value * 10;
		case TYPE_REPORT.ASSIST:
			return value * 50;
		case TYPE_REPORT.DMG_GET:
			return (int)((float)value * 0.1f);
		case TYPE_REPORT.DMG_SET:
			return (int)((float)value * 0.1f);
		case TYPE_REPORT.HEAL_GET:
			return (int)((float)value * 0.1f);
		case TYPE_REPORT.HEAL_SET:
			return (int)((float)value * 0.1f);
		case TYPE_REPORT.REB_GET:
			return 0;
		case TYPE_REPORT.REB_SET:
			return value * 10;
		case TYPE_REPORT.FLAG_NAT:
			return value * 100;
		case TYPE_REPORT.FLAG_CAP:
			return value * 250;
		case TYPE_REPORT.FLAG_DEF:
			return value * 50;
		case TYPE_REPORT.ITEM_GET:
			return value * 10;
		}
		return 0;
	}

	int getExperianceValue(TYPE_GAMEEND typeGameEnd){
		switch (typeGameEnd) {
		case TYPE_GAMEEND.VICTORY:
			return 3000;
		case TYPE_GAMEEND.DEFEAT:
			return 1000;
		case TYPE_GAMEEND.DRAW:
			return 2000;
		}
		return 0;
	}

	TYPE_GAMEEND getGameResult(){
		if (victory > 0) return TYPE_GAMEEND.VICTORY;
		else if (defeat > 0) return TYPE_GAMEEND.DEFEAT;
		return TYPE_GAMEEND.DRAW;
	}

	int getExperianceValue(TimeSpan time){
		return time.Minutes;
	}

}

