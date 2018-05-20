using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountSerialClass {

	GameReportSerialClass m_gameReportSerial;


	//계정 기본 AccountReportClass
	AccountReportSerialClass m_accountReportSerial;


	//병과 정보 MosReportClass - 병과 아이템 기록
	MosReportSerialClass m_mosReportSerial;


	//업적 AchivementClass
	AchivementReportSerialClass m_achivementReport;

	/// <summary>
	/// 데이터 저장하기
	/// </summary>
	/// <returns><c>true</c>, if data was saved, <c>false</c> otherwise.</returns>
	public bool saveData(AccountClass accountData){
		return false;	
	}

	/// <summary>
	/// 데이터 불러오기
	/// </summary>
	/// <returns>The data.</returns>
	public AccountClass loadData(){
		return null;
	}
}
