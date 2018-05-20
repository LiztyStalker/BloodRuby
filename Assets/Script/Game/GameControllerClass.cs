using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameControllerClass : MonoBehaviour, IGamePublisherInterface {


	[SerializeField] UICPUClass m_cpu; //사전에서 가져와야 함
	[SerializeField] bool m_isCpu; //CPU 있음
	[SerializeField] bool m_isAlive;// 아군만 생성
//	[SerializeField] bool m_isEnemy;// 적군만 생성
	//[SerializeField] MapDataClass m_map; //사전에서 가져와야 함
	string m_bgmTimerKey = "UITimer";
	string m_bgmStartKey = "UIStart";

    MapFactoryClass m_factory;
    MapReportClass m_mapReport;
	TimeSpan m_time;

	const float m_timerMax = 1f;
	float m_timer = 0f;
	int m_level = 1;

	const int c_flagMaxValue = 9999;
    


	bool m_isGameRun = false; //falst 게임 준비 - true 게임 시작 - false 게임 종료

	List<UICharacterClass> m_players =  new List<UICharacterClass> ();
	MapDataClass m_mapData;


	TYPE_MODE m_mode; //모드
	int[] m_ticketScore; //팀 점수 - 모드에 따라 다름
	int[] m_flagList; //깃발 개수
	int m_flagCnt = 0;
//	float m_gameEndTime = 30f;


	SoundPlayClass m_soundPlayer;

	public TYPE_MODE mode{ get { return m_mode; } }
	public int level{get{return m_level;}}
	public int[] ticketScore{ get { return m_ticketScore; } }
	public MapDataClass mapData{get{return m_mapData;}}
	public TimeSpan time{ get { return m_time; } }
	public TimeSpan maxTime{ get { return TimeSpan.FromMinutes(m_mapReport.m_time); } }
    public bool isGameRun { get { return m_isGameRun; } }
    public UICharacterClass[] characters { get {return m_players.ToArray(); } }

	// Use this for initialization
	void Start () {
		//맵 생성


		m_soundPlayer = GetComponent<SoundPlayClass> ();

        //싱글이면 맵 데이터 가져옴
        m_mapReport = AccountClass.GetInstance.playPanel.mapReport;

		//팩토리 연결하기
		m_factory = MapFactoryClass.GetInstance; //GameObject.Find("Game@MapFactory").GetComponent<MapFactoryClass>();


		//완성된 맵팩토리에서 데이터를 가져오고 
		//맵 프리펩 가져오기
        //맵 생성

//        m_factory.


//        UnityEditor.GameObjectUtility.SetNavMeshArea(new GameObject(), 0);

		//맵 데이터 가져오기
        MapDataClass mapData = m_factory.getMap(m_mapReport.m_mapKey);
		m_level = m_mapReport.m_level;

        if (mapData == null)
        {
            Debug.Log("맵 없음");
			//로비로 돌아가기
            return;
        }



		//맵 초기화
        m_mapData = (MapDataClass)Instantiate(mapData);

		//모드 변경 - 맵 모드를 리포트에서 변경해야함
		m_mode = m_mapData.mode;

		//맵 깃발 초기화
        foreach (CaptureObjectClass flag in m_mapData.flags)
        {
            flag.setBroadCast(captureBroadCast);
			flag.setMode (mode);
        }


		//맵에 대한 티켓 초기화 및 타이머 생성
		m_ticketScore = new int[2];
		m_flagList = new int[2];

		//훈련모드가 아니면
		if (m_mode != TYPE_MODE.TRAINING) {
			m_ticketScore [(int)TYPE_TEAM.TEAM_0] = m_mapReport.m_ticket;
			m_ticketScore [(int)TYPE_TEAM.TEAM_1] = m_mapReport.m_ticket;

			m_time = new TimeSpan (0, 0, 10);
			m_time = m_time.Duration ();

			if (m_isCpu) {


				for (int i = 0; i < m_mapReport.m_population * 2 - 1; i++) {
					UICharacterClass cpu = (UICharacterClass)Instantiate (m_cpu);
					cpu.setNickname (string.Format("[Bot]{0}", PrepClass.m_cpuName[i]));

					if (i < m_mapReport.m_population)
						cpu.team = TYPE_TEAM.TEAM_1;
					else
						cpu.team = TYPE_TEAM.TEAM_0;

					cpu.gameUpdate (this);
					Debug.Log ("컴퓨터생성 : " + i);

					m_players.Add (cpu);
				}


			}


		} else {
			m_ticketScore [(int)TYPE_TEAM.TEAM_0] = c_flagMaxValue;
			m_ticketScore [(int)TYPE_TEAM.TEAM_1] = c_flagMaxValue;

			//맵에 대한 티켓 초기화 및 타이머 생성
			//훈련모드면
			for (int i = 0; i < 4 * 2 - 1; i++) {
				UICharacterClass cpu = (UICharacterClass)Instantiate (m_cpu);
				cpu.setNickname (string.Format ("[Bot]{0}", PrepClass.m_cpuName [i]));

				if (i < m_mapReport.m_population)
					cpu.team = TYPE_TEAM.TEAM_1;
				else
					cpu.team = TYPE_TEAM.TEAM_0;

				cpu.gameUpdate (this);
				Debug.Log ("컴퓨터생성 : " + i);

				m_players.Add (cpu);
			}

		}

		//컴퓨터 생성 


		//플레이어가 처음 시작인지 난입인지 알아야 함

		
        //처음 시작 -


		StartCoroutine(starting());
	}

	void OnGUI(){
		if (m_mode == TYPE_MODE.TRAINING) {


			//사망
//			if (GUI.Button (new Rect (10f, 10f, 100f, 80f), "캐릭터 선택")) {
//
//			}


			//적 생성
//			if (GUI.Button (new Rect (10f, 10f, 100f, 80f), "적 생성")) {
//				Debug.Log ("test");
//			}

			//적 제거
			//적 움직임
			//적 무적
			//적 공격
			//적 스킬
			//적 쿨타임 초기화

			//아군 생성
			//아군 제거
			//아군 움직임
			//아군 무적
			//아군 공격
			//아군 스킬
			//쿨타임 초기화
			//병과 변경

//			if (GUI.Button (new Rect (10f, 10f, 100, 80f), "test")) {
//				Debug.Log ("test");
//			}



		}
	}

	IEnumerator gameReady(){
		while (!m_isGameRun) {
			m_timer += PrepClass.c_timeGap;
			if(m_timerMax <= m_timer){	
				m_time -= TimeSpan.FromSeconds(1);
				m_timer = 0f;

				if (m_time.Equals (TimeSpan.Zero)) {
					m_time = maxTime;
					m_isGameRun = true;
				} else {
					m_soundPlayer.audioPlay (m_bgmTimerKey, TYPE_SOUND.EFFECT);
				}

			}

			//게임 정보
			foreach(UICharacterClass cpu in m_players){
				cpu.gameUpdate(this);
				cpu.setMsg(this);
                
			}
			yield return new WaitForSeconds(PrepClass.c_timeGap);
		}

	}

	/// <summary>
	/// 게임 진행 코루틴
	/// </summary>
	IEnumerator starting(){


		//적 캐릭터도 UIPlayer 기반으로 작성
		//CPU도 하나의 플레이어라고 생각
		//게임 첫 시작시 해당 인구에 맞춰 UIPlayer 생성
		//

        //코루틴이 끝날때까지 대기
		if (m_mode != TYPE_MODE.TRAINING)
			yield return StartCoroutine (gameReady ());
		else {
			m_time = new TimeSpan (1, 0, 0);
			m_isGameRun = true;
		}

		m_soundPlayer.audioPlay (m_bgmStartKey, TYPE_SOUND.EFFECT);


        foreach (UICharacterClass cpu in m_players)
        {
            cpu.setMsg(this);
        }

		while (m_isGameRun) {

			m_timer += PrepClass.c_timeGap;

			//게임 종료 여부
			//시간이 0이되면
			if(m_time.Equals(TimeSpan.Zero)){
				//m_isGameEnd = true;
				if (m_mode != TYPE_MODE.TRAINING) {
					gameEndResult ();
					StartCoroutine (gameEndCoroutine ());
				} else {
					//시간 초기화
					m_time = new TimeSpan (1, 0, 0);
				}
			}
			//코루틴 타이머가 1초가 되면
			else if(m_timerMax <= m_timer){

				m_time -= TimeSpan.FromSeconds(1);

				//티켓 계산시 어느한쪽 또는 양쪽다 0이되면
				if(ticketCalculator()){
					//m_isGameEnd = true;
					StartCoroutine(gameEndCoroutine());
				}
			}

			foreach(UICharacterClass cpu in m_players){
				cpu.gameUpdate(this);
			}

			//3초마다 티켓 소비
			//

			yield return new WaitForSeconds(PrepClass.c_timeGap);

		}
	}

    /// <summary>
    /// 티켓 감산 계산기
    /// </summary>
    /// <returns></returns>
	bool ticketCalculator(){

//		Debug.Log ("티켓");

		m_timer = 0f;
		m_flagCnt = 0;

		for (int i = 0; i < m_ticketScore.Length; i++) {
			//점수가 있으면 깃발 개수 가져오기
			//
			if(m_ticketScore[i] > 0){
				m_flagList[i] = m_mapData.getTecket((TYPE_TEAM)i);
			}
		}

		m_flagCnt = m_flagList [(int)TYPE_TEAM.TEAM_0] - m_flagList [(int)TYPE_TEAM.TEAM_1];

//		Debug.Log (m_flagCnt);

		//깃발차가 있으면 빼기
		//깃발점수가 상승하는 일은 없음
		if (m_flagCnt > 0) {
			m_ticketScore [(int)TYPE_TEAM.TEAM_1] = Mathf.Clamp (m_ticketScore [(int)TYPE_TEAM.TEAM_1] - m_flagCnt, 0, c_flagMaxValue);

			if(m_mode == TYPE_MODE.TRAINING && m_ticketScore [(int)TYPE_TEAM.TEAM_1] == 0){
				m_ticketScore [(int)TYPE_TEAM.TEAM_1] = c_flagMaxValue;
			}
		} else if (m_flagCnt < 0) {
			m_ticketScore [(int)TYPE_TEAM.TEAM_0] = Mathf.Clamp (m_ticketScore [(int)TYPE_TEAM.TEAM_0] + m_flagCnt, 0, c_flagMaxValue);

			if(m_mode == TYPE_MODE.TRAINING && m_ticketScore [(int)TYPE_TEAM.TEAM_0] == 0){
				m_ticketScore [(int)TYPE_TEAM.TEAM_0] = c_flagMaxValue;
			}
		}





		//티켓 점수가 0이하 이면 -1로 등록
		for (int i = 0; i < m_ticketScore.Length; i++) {
            if (m_ticketScore[i] <= 0)
            {
                m_flagList[i] = -1;
            }
		}


		if (m_flagList [0] == -1 && m_flagList [1] == -1) {
			//무승부
			foreach (UICharacterClass character in m_players) {
				character.gameEnd (TYPE_GAMEEND.DRAW);
			}
		} else if (m_flagList [0] == -1) {
			foreach (UICharacterClass character in m_players) {
				if ((int)character.team == 0)
					character.gameEnd (TYPE_GAMEEND.DEFEAT);
				else
					character.gameEnd (TYPE_GAMEEND.VICTORY);
			}

			//0 패배 1 승리

		} else if (m_flagList [1] == -1) {
			foreach (UICharacterClass character in m_players) {
				if ((int)character.team == 0)
					character.gameEnd (TYPE_GAMEEND.VICTORY);
				else
					character.gameEnd (TYPE_GAMEEND.DEFEAT);
			}
			//1 패배 0 승리
		} else {
			return false;
		}
		return true;


	}


	/// <summary>
	/// 게임 종료값
	/// </summary>
	void gameEndResult(){

		TYPE_TEAM winTeam = TYPE_TEAM.NONE;
		int maxTicket = 0;
		for (int i = 0; i < m_ticketScore.Length; i++) {
			if(m_ticketScore[i] > maxTicket){
				maxTicket = m_ticketScore[i];
				winTeam = (TYPE_TEAM)i;
			}
			else if(m_ticketScore[i] == maxTicket){
				winTeam = TYPE_TEAM.NONE;
				//무승부
			}
		}

		if (winTeam == TYPE_TEAM.NONE) {
			//무승부
			foreach (UICharacterClass character in m_players) {
				character.gameEnd (TYPE_GAMEEND.DRAW);
			}
		} else{
			foreach (UICharacterClass character in m_players) {
				if (character.team == winTeam) {
					character.gameEnd (TYPE_GAMEEND.VICTORY);
				} else {
					character.gameEnd (TYPE_GAMEEND.DEFEAT);
				}
			}
		}
	}

	void gameEndInit(){
		m_isGameRun = false;
		foreach (CaptureObjectClass cap in m_mapData.flags) {
			cap.isCaptured = false;
		}
	}

	/// <summary>
	/// 거점 점령 당했을 때 메시지
	/// </summary>
	/// <param name="msg">Message.</param>
	/// <param name="team">Team.</param>
    void captureBroadCast(string flagTag, TYPE_TEAM team)
    {


        foreach (UICharacterClass character in m_players)
        {
			

			//중립되면 
			//A 거점이 중립화
			if(team == TYPE_TEAM.NONE)
				character.setMsg(flagTag + " 거점 중립화");

			//아군 거점이 되면
			//아군 A 거점 점령
			else if(team == character.team)
				character.setMsg(flagTag + " 거점 점령!");

			//적군 거점이 되면
			//적군 A 거점 점령
			else if(team != character.team)
				character.setMsg(flagTag + " 거점 뺏김!");

        }
    }

	
	
	IEnumerator gameEndCoroutine(){
		//거점 점령 불가
		gameEndInit ();

		yield return new WaitForSeconds(5f);

		//결과창 보여줌 - 킬 데스 등


        m_time = new TimeSpan(0, 0, 31);

		//30초 후 다음 맵으로 이동
		//또는 버튼을 눌러 로비로 이동
        while (m_time != TimeSpan.Zero)
        {

            m_time = m_time.Subtract(TimeSpan.FromSeconds(1));

			m_soundPlayer.audioPlay (m_bgmTimerKey, TYPE_SOUND.EFFECT);

			foreach (UICharacterClass character in m_players)
            {
                character.gameHistory(this);
            }


            //Debug.Log("time : " + m_time);
			yield return new WaitForSeconds(PrepClass.c_timeGap * 10f);
		}


		nextStage ();
	}

	/// <summary>
	/// 다음 스테이지로 이동
	/// </summary>
	public void nextStage(){
		//로딩창
        AccountClass.GetInstance.playPanel.nextPanel = PrepClass.c_GamePanelScene;
		Application.LoadLevel (PrepClass.c_LoadPanelScene);
	}

	/// <summary>
	/// 플레이어 게임 입장
	/// </summary>
	/// <param name="player">Player.</param>
	public void gameEnter(UIPlayerClass player){
		m_players.Add (player);

	}

	/// <summary>
	/// 플레이어 게임 퇴장
	/// </summary>
	/// <param name="player">Player.</param>
	public void gameExit(UIPlayerClass player){
		m_players.Remove (player);
	}

	/// <summary>
	/// 킬 사망 방송
	/// </summary>
	/// <param name="character">사망자.</param>
	/// <param name="inflictCharacter">사살자.</param>
	/// <param name="type">타입.</param>
	public void broadcastKillDeathMsg(ICharacterInterface deathCharacter, ICharacterInterface inflictCharacter, IBullet bullet){
		foreach (UICharacterClass playCharacter in m_players) {
			playCharacter.broadcastKillDeathMsg (deathCharacter, inflictCharacter, bullet);
		}
	}
}
