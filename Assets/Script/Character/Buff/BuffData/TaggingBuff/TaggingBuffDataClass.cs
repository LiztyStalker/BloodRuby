using System;
using System.Collections.Generic;
using UnityEngine;

//public enum TYPE_TAG_BUFF{}

public class TaggingBuffDataClass : BuffDataClass
{
	/// <summary> 태깅할 버프 </summary>
	[SerializeField] BuffDataClass m_buffData;
	/// <summary> 시간 / 0 시간지속, 0 >= 무한 지속 </summary>
//	[SerializeField] float m_time;

	//AttackTaggingBuffData는 사용하지 않음

	[SerializeField] bool m_isManage = false;

	List<ICharacterInterface> m_characterList = new List<ICharacterInterface>();

	public List<ICharacterInterface> characterList{ get { return m_characterList; } }
	protected BuffDataClass buffData{ get { return m_buffData; } }

//	protected float time{get{return m_time;}}
	protected bool isManage{get{return m_isManage;}}

//	protected virtual void Start(){
//		startCoroutine (-1f, null);
//	}

	public override void buffStart (ICharacterInterface ownerCharacter, ICharacterInterface setCharacter)
	{
		base.buffStart (ownerCharacter, setCharacter);
	}


	/// <summary>
	/// 버프데이터 일회성 붙이기
	/// 0 = 화면 전체
	/// </summary>
	protected void instanceTaggingBuff(float radius){

		RaycastHit2D[] hits = null;

		if (radius < 0f)
			radius = 0f;

		if (radius > 0f) {
            //범위가 정해져 있으면
            //범위만큼
			hits = Physics2D.CircleCastAll(
                ownerCharacter.transform.position, 
                radius, 
                Vector2.zero);		
		}
		else{
            //범위가 정해져 있지 않으면
            //화면 전체
			hits = Physics2D.BoxCastAll(
                ownerCharacter.transform.position, 
                new Vector2(
                    2 * Camera.main.orthographicSize * Camera.main.aspect, 
                    2 * Camera.main.orthographicSize), 
                0f, 
                Vector2.zero);
		}

		if (hits != null) {
//			Debug.LogWarning ("cnt : " + hits.Length);
			foreach (RaycastHit2D hit in hits) {
				setCollider (hit.collider);
			}
		}


	}


	/// <summary>
	/// 지속 버프 붙이기
	/// </summary>
	/// <param name="col">Col.</param>
	protected void setCollider(Collider2D col){
		//		Debug.Log ("tag : " + col.tag);
		if (PrepClass.isCharacterTag (col.tag)) {
			//			if (col.GetComponent<ICharacterInterface> () != m_character) {

			//근처 캐릭터 가져오기
			ICharacterInterface neighbourCharacter = col.GetComponent<ICharacterInterface> ();

//			Debug.LogWarning ("neighbour : " + neighbourCharacter.playerName);
			neighbourCharacter = PrepClass.getCharacter(ownerCharacter, neighbourCharacter, isAlly, isDead, isMyself);


//				deadCheck(myselfCheck(teamCheck(neighbourCharacter)));

			//모든 해당사항이 부합하면
			if(neighbourCharacter != null){
				//캐릭터 버프 붙이기
				neighbourCharacter.buffAdd (m_buffData, ownerCharacter, neighbourCharacter);
//				Debug.LogWarning ("neighbour : " + neighbourCharacter.playerName);

				//버프 관리이고 버프가 걸려있는지 여부
				if (m_isManage && !m_characterList.Contains (neighbourCharacter)) {
					//캐릭터 버프 관리
					m_characterList.Add (neighbourCharacter);
				}
			}
			//			}
		}
	}

	/// <summary>
	/// 지속 버프 떼기
	/// </summary>
	/// <param name="col">Col.</param>
	protected void resetCollider(Collider2D col){
		
		if (!m_isManage) return;

		if (PrepClass.isCharacterTag (col.tag)) {
			//			if (col.GetComponent<ICharacterInterface> () != m_character) {


			ICharacterInterface neighbourCharacter = col.GetComponent<ICharacterInterface> ();

			//버프관리중인 캐릭터가 있으면
			if(m_characterList.Contains(neighbourCharacter)){
				
				BuffDataClass buffData = neighbourCharacter.addState.getBuff (m_buffData.GetType());
			
				if (buffData != null) {
					//버프가 완전히 종료되면
					if (buffData.buffEnd ()) {
						//버프 없애기
						m_characterList.Remove (neighbourCharacter);
					}
				}
			}
			//			}
		}
	}
//		
//	ICharacterInterface teamCheck(ICharacterInterface anotherCharacter){
//		if (anotherCharacter == null)
//			return null;
//
//		//같은팀 체크일때 같은팀이면
//		if (isAlly && ownerCharacter.team == anotherCharacter.team)
//			return anotherCharacter;
//		//다른팀 체크일때 다른팀이면
//		else if (!isAlly && ownerCharacter.team != anotherCharacter.team)
//			return anotherCharacter;
//		//해당사항 없음
//		return null;
//	}
//
//
//	ICharacterInterface myselfCheck(ICharacterInterface anotherCharacter){
//
//		if (anotherCharacter == null)
//			return null;
//		
//
//		//자신포함이 미체크일때 자신이면
//		if (!isMyself && ownerCharacter == anotherCharacter)
//			return null;
//		
//		//자신포함이 체크이면
//		return anotherCharacter;
//	}
//
//	ICharacterInterface deadCheck(ICharacterInterface anotherCharacter){
//		if (anotherCharacter == null)
//			return null;
//		//사망자 체크일때 사망자이면
//
//		if (isDead && anotherCharacter.isDead)
//			return anotherCharacter;
//		//사망자 미체크일때 사망자가 아니면
//		else if (!isDead && !anotherCharacter.isDead)
//			return anotherCharacter;
//		return null;
//	}


	public override bool buffReplace ()
	{
		initTime ();
		return true;
	}
}


