using System;
using System.Collections;
using UnityEngine;




public class UIPlayMsgClass : MonoBehaviour
{

    [SerializeField] UIPlayMsgAlarmClass m_msgAlarm;
    [SerializeField] UIPlayMsgFlagClass m_msgFlag;
	[SerializeField] UIKillListClass m_killListManager;
	[SerializeField] UIPlayMsgCastingClass m_castManager;



	/// <summary>
	/// 캐스팅 메시지
	/// </summary>
	/// <param name="msg">Message.</param>
//	public void setCastMassage(string msg){
//		m_castManager.gameObject.SetActive (true);
//		m_castManager.setCastAlarm (msg);
//	}

	/// <summary>
	/// 캐스팅 메시지
	/// </summary>
	/// <param name="msg">Message.</param>
	public void setCastMassage(string msg, bool isLoop = false){

		m_castManager.gameObject.SetActive (true);
		m_castManager.setCastAlarm (msg, isLoop);
	}

	/// <summary>
	/// 캐스팅 메시지
	/// </summary>
	/// <param name="msg">Message.</param>
	/// <param name="castingBuff">Casting buff.</param>
	public void setCastMassage(string msg, CastingParticleBuffDataClass castingBuff){

		//버프 삽입

		m_castManager.gameObject.SetActive (true);
		m_castManager.setCastAlarm (msg, castingBuff);
	}


	/// <summary>
	/// 캐스팅 메시지 닫기
	/// </summary>
	public void closeCastingMassage(){
		m_castManager.gameObject.SetActive (false);
	}


	/// <summary>
	/// 알람 메시지
	/// </summary>
	/// <param name="msg">Message.</param>
    public void setMsgAlarm(string msg)
    {
        m_msgAlarm.gameObject.SetActive(true);
        m_msgAlarm.setMsgAlarm(msg);
    }

	/// <summary>
	/// 깃발 메시지 열기
	/// </summary>
	/// <param name="msg">Message.</param>
	/// <param name="flag">Flag.</param>
    public void setMsgFlag(string msg, CaptureObjectClass flag)
    {

        m_msgFlag.gameObject.SetActive(true);
        m_msgFlag.setMsgFlag(msg, flag);

    }

	/// <summary>
	/// 깃발 메시지 닫기
	/// </summary>
	public void closeMsgFlag(){
		m_msgFlag.gameObject.SetActive(false);
	}


	public void broadcastKillDeathMsg(ICharacterInterface character, ICharacterInterface inflictCharacter, IBullet bullet){ 
		m_killListManager.setKillListBar (character, inflictCharacter, bullet);
	}
//    void OnDisable()
//    {
//        m_msgAlarm.gameObject.SetActive(false);
//        m_msgFlag.gameObject.SetActive(false);
//    }


    
    //게임 준비
    //게임 시작
    //지휘
    //사망자 로그
    //거점 뺏김
    //거점 점령
    //거점 뺏기는 중
    //거점 점령중
    //시간 얼마 안 남음
    //승점 100 이하
    //채팅 기능
    //알림 기능
    //
}

