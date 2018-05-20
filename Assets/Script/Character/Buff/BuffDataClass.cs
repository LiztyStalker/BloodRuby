using UnityEngine;
using System;
using System.Collections;



public enum TYPE_VALUE{
	PERCENT, //퍼센트
	VALUE, //값
	STATIC //고정
}

public enum TYPE_BUFF_STATE{
	CONTINUE, //지속
	TIME, //시간
	COUNT, //횟수
	TOGGLE, //토글
	TAG //태그
}

public enum TYPE_BUFF_STATE_ACT{
	NONE, //없음
	MOVE, //이동
	ATTACK, //공격
	HIT, //피격
	VIEW, //보기
	KILL, //사살
	DEAD, //사망
	ACCIST, //도움
	SKILL, //스킬
	HEALTH, //회복
	TELESCOPE //망원경
}


public abstract class BuffDataClass : MonoBehaviour, IContentView{

	//const float c_timeGap = 0.1f;
//	protected TYPE_BUFF_STATE m_buffState;
//	protected BitArray m_buffState = new BitArray(Enum.GetValues(typeof(TYPE_BUFF_STATE)).Length, false);
	protected BitArray m_buffStateAct = new BitArray(Enum.GetValues(typeof(TYPE_BUFF_STATE_ACT)).Length, false);

	/// <summary>
	/// 버프를 받는 캐릭터
	/// </summary>
	ICharacterInterface m_actCharacter = null;

	/// <summary>
	/// 버프를 사용한 캐릭터
	/// </summary>
	ICharacterInterface m_ownerCharacter = null;

	protected ValueAddStateClass[] valueState;
//	protected TYPE_BUFF_STATE m_buffState;

	protected int m_instanceID;

	TextInfoClass m_textInfo;


	[SerializeField] string m_key;
	[SerializeField] string m_name;
	[SerializeField] string m_animationName;
	//[SerializeField] TYPE_BUFF m_buffType;
	[SerializeField] string m_contents;
	[SerializeField] string m_group;
	[SerializeField] TYPE_BUFF_STATE m_buffState;
	[SerializeField] TYPE_BUFF_STATE_ACT[] m_buffActionEvents = null;
	[SerializeField] TYPE_CONSTRAINT[] m_constraintList = null;
//	[SerializeField] int m_loopCount;
//	[SerializeField] float m_loopTime;
//	[SerializeField] TYPE_BUFF_ACTIVATE m_buffActivate;
	[SerializeField] protected Sprite m_icon;
	/// <summary> 아이콘 보이기 </summary>
	[SerializeField] bool m_isIconView = true;
//	[SerializeField] float m_prepTime = 0f;
	[SerializeField] float m_time = 0f;
//	[SerializeField] bool m_isIconView;
	[SerializeField] ParticleSystem m_particle;
//	[SerializeField] bool m_isParticleView;

	/// <summary>
	/// 버프 조건에 부합시 나타나는 파티클
	/// </summary>
	[SerializeField] ParticleSystem m_useActParticle;

	//범위
	[SerializeField] float m_range;


	/// <summary> true 자신 포함 false 자신 미포함 </summary>
	[SerializeField] bool m_isMyself = true;
	/// <summary> true 아군, false 적군 </summary>
	[SerializeField] bool m_isAlly = true;
	/// <summary> true 사망자, false 생존자 </summary>
	[SerializeField] bool m_isDead = false;
	/// <summary> true 버프관리 false 일회성 </summary>
	/// 
	protected int m_count = 0;

	ParticleSystem m_buffParticle;

	protected delegate void BuffLoopDelegate();
	BuffLoopDelegate m_buffDelegate = null;

	//BuffLoopDelegate buffLoopDel;

	protected Coroutine m_buffCoroutine = null;

	protected bool m_isRemove = true; //삭제됨
	protected float m_maxTime = 0f;
	protected float m_runTime = 0f;

	SoundPlayClass m_soundPlayer;

	protected SoundPlayClass soundPlayer {
		get { 
			if (m_soundPlayer == null)
				m_soundPlayer = GetComponent<SoundPlayClass> ();
			return m_soundPlayer;
		}
	}

//	public TYPE_BUFF buffType{ get { return m_buffType; } }
//	public TYPE_BUFF_STATE buffState{get{return m_buffState;}}
	public string key{get{return textInfo.key;}}// m_key;}}
	public string name{ get { return textInfo.name;}}// m_name; } }
	public string animationName{ get { return m_animationName; } }
	public string contents{ get { return textInfo.contents; } }// m_contents;}}
	public string group{ get { return m_group; } }
	public TYPE_SKILL typeSkill{ get { return TYPE_SKILL.ENEMY; } }
	/// <summary>
	/// 현재 진행중인 시간
	/// </summary>
	/// <value>The time.</value>
	public float time{ get { return (m_runTime <= 0f) ? 0f : m_maxTime - m_runTime; } }
	public TYPE_CONSTRAINT[] constraintList{ get { return m_constraintList; } }


	public TYPE_BUFF_STATE buffState{get{return m_buffState;}}
	public BitArray buffStateAct{get{return m_buffStateAct;}}
	public Sprite icon{ get { return m_icon; } set{ m_icon = value; }}
	public bool isIconView{get{return m_isIconView;}}
//	public ParticleSystem particle{get{return m_particle;}}
//	public float prepTime{get{return m_prepTime;}}
	public float runTime{get{return m_runTime;}}
	public float maxTime{get{return m_maxTime;}}
	public bool isRemove{ get { return m_isRemove; } }
	public int count{ get { return m_count; } }
	public float range{get{return m_range;}}


	public bool isMyself{ get { return m_isMyself; } }
	public bool isAlly{get{return m_isAlly;}}
	public bool isDead{get{return m_isDead;}}



	protected ParticleSystem buffParticle{ get { return m_buffParticle; } }
	protected ParticleSystem useActParticle{ get { return m_useActParticle; } }


	TextInfoClass textInfo{ 
		get { 
			if (m_textInfo == null) {
				string preName = gameObject.name.Replace ("(Clone)", "");
				m_textInfo = TextInfoFactoryClass.GetInstance.getTextInfo (preName);
			}
			return m_textInfo; 
		} 
	}

	protected virtual void Awake(){
		m_textInfo = textInfo;
		buffStateSet (m_buffActionEvents);
	}

	protected virtual void Start(){
		startCoroutine (m_time);
	}


	/// <summary>
	/// 버프에 당하는 캐릭터
	/// </summary>
	/// <value>The act character.</value>
	public ICharacterInterface actCharacter{ get { return m_actCharacter; } }

	/// <summary>
	/// 버프를 사용한 캐릭터
	/// </summary>
	public ICharacterInterface ownerCharacter{ get { return m_ownerCharacter; } }


	protected void setBuffLoopDelegate(BuffLoopDelegate del){
		m_buffDelegate = del;
	}

	//protected float timeGap{ get { return c_timeGap; } }

	/// <summary>
	/// 버프상태 셋
	/// </summary>
	/// <param name="buffState">Buff state.</param>
	/// <param name="buffStateAct">Buff state act.</param>
	protected void buffStateSet(TYPE_BUFF_STATE_ACT buffStateAct = TYPE_BUFF_STATE_ACT.NONE){
		Debug.Log ("buffStateSet : " + buffState);
		m_buffStateAct.Set ((int)buffStateAct, true);
	}

	/// <summary>
	/// 버프상태 셋
	/// </summary>
	/// <param name="buffState">Buff state.</param>
	/// <param name="buffStateAct">Buff state act.</param>
	protected void buffStateSet(TYPE_BUFF_STATE_ACT[] buffStateActs){
		foreach(TYPE_BUFF_STATE_ACT buffAct in buffStateActs)// buffStateActSet(buffAct);
			m_buffStateAct.Set ((int)buffAct, true);
	}



	/// <summary>
	/// 버프 시작
	/// </summary>
	/// <param name="ownerCharacter">버프 사용한 캐릭터</param>
	/// <param name="setCharacter">버프 걸리는 캐릭터</param>
	public virtual void buffStart(ICharacterInterface ownerCharacter, ICharacterInterface actCharacter){
		Debug.Log ("BuffStart");
		m_ownerCharacter = ownerCharacter;
		m_actCharacter = actCharacter;
		m_instanceID = GetInstanceID ();
		valueState = (ValueAddStateClass[])GetComponents<ValueAddStateClass> ().Clone();

		if (m_particle != null) {
			m_buffParticle = Instantiate (m_particle, transform.position, Quaternion.identity);
			m_buffParticle.transform.SetParent (this.transform);
		}

		setConstraint ();

	}

	/// <summary>
	/// 제약조건 적용
	/// </summary>
	protected void setConstraint(){
		if (constraintList != null) 
			actCharacter.addState.setConstraint (this, constraintList);
	}

	/// <summary>
	/// 제약조건 해제
	/// </summary>
	protected void resetConstraint(){
		if (constraintList != null) {
			actCharacter.addState.resetConstraint (this, constraintList);
		}
		
	}

	/// <summary>
	/// 버프 코루틴 실행하기
	/// </summary>
	/// <param name="time">Time. 0 > 시간만큼, 0 <= 지속 </param></param>
	/// <param name="del">Del.</param>
	protected void startCoroutine(float time){
		//양수이면 
		if(time > 0f)
			m_buffCoroutine = StartCoroutine (buffLoop (time, m_buffDelegate));
		else //if(time <= 0f)
			m_buffCoroutine = StartCoroutine (buffLoop (-1f, m_buffDelegate));

	}

	/// <summary>
	/// 버프 사용 당함
	/// </summary>
	/// <param name="character">Character.</param>
	/// <param name="bullet">Bullet.</param>
	public virtual bool useBuff (ICharacterInterface character, IBullet bullet){
		if (useActParticle != null) Instantiate (useActParticle, character.transform.position, Quaternion.identity);
		return false;
	}

	/// <summary>
	/// 버프 사용
	/// </summary>
	/// <returns><c>true</c>, if buff was used, <c>false</c> otherwise.</returns>
	/// <param name="useActcharacter">실시간 버프 당하는 캐릭터.</param>
	public virtual bool useBuff(ICharacterInterface useActcharacter){
		return false;
	}

	/// <summary>
	/// 버프 종료
	/// </summary>
	public virtual bool buffEnd(){
		//가동중인 코루딘 강제 종료
//		try{
//		if(ownerCharacter != null)
//		Debug.LogWarning ("buffEnd : " + ownerCharacter.playerName + " " + actCharacter.playerName + " " + GetType());
//		if(GetType() == typeof(SacrificeTagBuffStateClass))
//		Debug.LogWarning("type : " + gameObject.activeSelf);
//		Debug.LogWarning("type : " + GetComponentInParent<ICharacterInterface> ());

//		returnValueState ();
		resetConstraint ();
		m_buffStateAct.SetAll(false);

//		
			if(m_buffCoroutine != null)
				StopCoroutine (m_buffCoroutine);

			//버프 종료 보내기
			return GetComponentInParent<ICharacterInterface>().addState.buffEnd (this);
//
//		}
//		catch(MissingReferenceException e){
//			Debug.LogError ("buffEnd Error : " + e.Message + " " + ownerCharacter.playerName + " " + actCharacter.playerName );
//			return true;
//		}


		//버프 파괴 - 토글
		//Destroy (gameObject);
	}

	/// <summary>
	/// 버프 갱신하기
	/// </summary>
	/// <returns><c>true</c>갱신 완료 <c>false</c>갱신 안함.</returns>
	public virtual bool buffReplace(){
		initTime ();
		return true;
	}

	/// <summary>
	/// 시간 초기화
	/// </summary>
	/// <param name="maxTime">Max time.</param>
	protected void initTime(){
		m_maxTime = m_time;
		m_runTime = 0f;
	}

	/// <summary>
	/// 시간 추가
	/// </summary>
	/// <param name="addTime">Add time.</param>
	protected void addTime(float addTime){
		m_maxTime += addTime;
	}

	/// <summary>
	/// 버프 루프
	/// </summary>
	/// <returns>The loop.</returns>
	/// <param name="time">Time.</param>
	/// <param name="buffLoopDel">Buff loop del.</param>
	IEnumerator buffLoop(float time, BuffLoopDelegate buffLoopDel){

		initTime ();

//		try {
//			if (time > 0f) {
//				var particle = GetComponent<ParticleSystem>().main;
//				particle.duration = time + 1f;
//				particle.startLifetime = time + 1f;
//				particle.loop = false;
//			} 
//			else
//				GetComponent<ParticleSystem> ().loop = true;
//		}
//		catch {
//		}
	

		//진행시간이 음수이거나 최대시간이 0이하이면 루프
		while(!m_isRemove || maxTime <= 0f || runTime <= maxTime){
//			Debug.Log ("runTime : " + name + maxTime);

			//A0.8 타이머 수정
			if (maxTime > 0f) {
				if (m_runTime >= maxTime) m_runTime = maxTime;
				else m_runTime += PrepClass.c_timeGap;
			}
			//델리게이트가 있으면 델리게이트 메소드 실행
			if(buffLoopDel != null) buffLoopDel();
			yield return new WaitForSeconds(PrepClass.c_timeGap);
		}
		//버프 종료

		m_buffCoroutine = null;
		buffEnd ();
	}


	protected void addValueState(BuffDataClass buffData){
		for(int i = 0; i < valueState.Length; i++){
			actCharacter.addState.setValue (valueState[i], buffData);
		}
	}

	protected void returnValueState(BuffDataClass buffData){
		for(int i = 0; i < valueState.Length; i++){
			actCharacter.addState.returnValue (valueState[i], buffData);
		}
	}

	/// <summary>
	/// 버프 보조값 반환
	/// </summary>
	/// <returns>The assist buff data.</returns>
	/// <param name="value">Value.</param>
	protected float getAssistBuffData(float value){
		AssistantBuffDataClass assistBuff = (AssistantBuffDataClass)ownerCharacter.addState.getBuff (typeof(AssistantBuffDataClass));

		//버프 보조가 있으면
		if (assistBuff != null) {
			//버프 타입이 있으면 추가값 반환
			return assistBuff.getAssistBuffCalculator (this.GetType (), value);
		}
		return value;
	}
		
	protected int getAssistBuffData(int value){
		return (int)getAssistBuffData((float)value);
	}
		
//	public object Clone(){
//		return (object)this.MemberwiseClone ();
//	}
}
