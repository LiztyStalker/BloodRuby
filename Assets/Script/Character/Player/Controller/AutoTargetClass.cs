using System;
using UnityEngine;

public class AutoTargetClass : MonoBehaviour
{
	//자동목표 최소 감지 길이
	const float c_rangeMin = 1f;
	const float c_length = 6.5f;

	//자동 타겟팅
//	[SerializeField] SpriteRenderer m_focus;
	[SerializeField] bool m_isUsed = true; //사용여부

	float m_range;
	Vector2 m_direction = Vector2.zero;

	ICharacterInterface m_target = null;
	ICharacterInterface m_ally = null;

	public ICharacterInterface target{get{return (m_target != null && !m_target.isDead) ? m_target : null;}}
	public ICharacterInterface ally{get{return m_ally;}}

//	void Start(){
//		aimPosition (transform.parent.eulerAngles.z);
//	}
		


	public void resetTarget(){
		m_target = null;
	}

	public void resetAlly(){
		m_ally = null;
	}


	public bool isSetTarget(TYPE_TEAM team){
		//적이 이미 있으면
		if (target != null) {
			/// 거리 이상이면 true


			if (target.isDead || c_length < Vector2.Distance(transform.position, target.transform.position))	{
				resetTarget ();
				return false;
			}
			else{
//				Debug.LogError ("search : " + m_target);
				return true;
			}


		} 
		//적이 없으면
		else{
			//범위
			RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, c_length, Vector2.zero);

			//적 발견 
			//반경 10 이내의 아이템 가져오기
			foreach (RaycastHit2D hit in hits)
			{
				//Debug.Log("hits : " + hit.collider.name);
				//가져온 아이템이 캐릭터이면

				//충돌체가 자신이면 무시
				if (hit.collider.gameObject == gameObject)
					continue;

				//캐릭터이면
				else if (PrepClass.isCharacterTag(hit.collider.tag))
				{
					//레이 발사
					//Debug.Log("character : " + hit.collider.name);
					RaycastHit2D[] tmpTargets = Physics2D.RaycastAll(transform.position, hit.transform.position - transform.position, c_length);

					Debug.DrawRay(transform.position, hit.transform.position - transform.position, Color.green);

					//레이에 걸린 모든 오브젝트  순환
					foreach (RaycastHit2D tmpTarget in tmpTargets)
					{
						// Debug.Log("tmpTarget : " + tmpTarget.collider.name);

						//레이를 쏴서 무언가에 막혀 있는지 확인
						//자신이면 무시
						if (tmpTarget.collider.gameObject == gameObject) {
							continue;
						}
						else if (tmpTarget.collider.tag == "ActObject") {
							continue;
						}

						else if (tmpTarget.collider.tag == "Wall" || tmpTarget.collider.tag == "Object") 
						{
							m_target = null;
							return false;
						}
						//캐릭터인 경우
						else if (PrepClass.isCharacterTag(tmpTarget.collider.tag))
						{
							//충돌체가 자신이면 무시


							//팀이 다르면
							if (tmpTarget.collider.GetComponent<ICharacterInterface>().team != team)
							{
								//사망한 상태가 아니면
								if (!tmpTarget.collider.GetComponent<ICharacterInterface> ().isDead) {
									//적으로 간주함

									//적이 정하지 않았을 경우
									//적 설정
									if (target == null) {
										m_target = hit.collider.GetComponent<ICharacterInterface> ();
									}
										
									//설정한 적보다 새 적이 가까울 경우
//									if (Vector2.Distance (transform.position, target.transform.position) < Vector2.Distance (transform.position, tmpTarget.transform.position)) {
//										//적 변경
//										m_target = tmpTarget.collider.GetComponent<ICharacterInterface> ();
//									}
									//목표 선택 완료
									return true;
								}

							}
							//팀이 같으면 반복
						}
						//캐릭터가 아니면 반복
					}
				}
			}
		}
        return false;
	}

	public bool isSetAlly(TYPE_TEAM team){
		//적이 이미 있으면
		if (ally != null) {
			/// 거리 이상이면 true

			if (ally.isDead || c_length < Vector2.Distance(transform.position, ally.transform.position))	{
				m_ally = null;
				return false;
			}
			else{
				return true;
			}


		} 
		else{
			//범위
			RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, c_length, Vector2.zero);

			//적 발견 
			//반경 10 이내의 아이템 가져오기
			foreach (RaycastHit2D hit in hits)
			{
				//Debug.Log("hits : " + hit.collider.name);
				//가져온 아이템이 캐릭터이면

				//충돌체가 자신이면 무시
				if (hit.collider.gameObject == gameObject)
					continue;

				//캐릭터이면
				else if (PrepClass.isCharacterTag(hit.collider.tag))
				{
					//레이 발사
					//Debug.Log("character : " + hit.collider.name);
					RaycastHit2D[] tmpTargets = Physics2D.RaycastAll(transform.position, hit.transform.position - transform.position, c_length);

					Debug.DrawRay(transform.position, hit.transform.position - transform.position, Color.green);

					//레이에 걸린 모든 오브젝트  순환
					foreach (RaycastHit2D tmpTarget in tmpTargets)
					{
						// Debug.Log("tmpTarget : " + tmpTarget.collider.name);

						//레이를 쏴서 무언가에 막혀 있는지 확인
						//자신이면 무시
						if (tmpTarget.collider.gameObject == gameObject) {
							continue;
						}
						else if (tmpTarget.collider.tag == "ActObject") {
							continue;
						}

						else if (tmpTarget.collider.tag == "Wall" || tmpTarget.collider.tag == "Object") 
						{
							m_ally = null;
							return false;
						}
						//캐릭터인 경우
						else if (PrepClass.isCharacterTag(tmpTarget.collider.tag))
						{
							//충돌체가 자신이면 무시


							//팀이 다르면
							if (tmpTarget.collider.GetComponent<ICharacterInterface>().team != team)
							{
								//사망한 상태가 아니면
								if (!tmpTarget.collider.GetComponent<ICharacterInterface> ().isDead) {
									//적으로 간주함

									//적이 정하지 않았을 경우
									//적 설정
									if (ally == null)
										m_ally = hit.collider.GetComponent<ICharacterInterface> ();

									//설정한 적보다 새 적이 가까울 경우
									//									if (Vector2.Distance (transform.position, target.transform.position) < Vector2.Distance (transform.position, tmpTarget.transform.position)) {
									//										//적 변경
									//										m_target = tmpTarget.collider.GetComponent<ICharacterInterface> ();
									//									}
									//목표 선택 완료
									return true;
								}

							}
							//팀이 같으면 반복
						}
						//캐릭터가 아니면 반복
					}
				}
			}

		}

		return false;
	}



	/// <summary>
	/// 타겟 가져오기
	/// </summary>
	/// <returns>The target.</returns>
	/// <param name="angle">현재 각도.</param>
	/// <param name="team">현재 팀.</param>
	/// <param name="isAlly">아군여부 <c>true</c> 아군 선택.</param>
	/// <param name="isMyself">자신여부 <c>true</c> 자신 선택.</param>
	/// <param name="isDead">사망여부 <c>true</c> 사망자 선택.</param>
	/// <param name="maxAngle">최대 인식 각도.</param>
	public ICharacterInterface getTarget(float angle, TYPE_TEAM team, bool isAlly, bool isMyself, bool isDead, float maxAngle = 10f){

		try{

			//적이 등록되어 있으면
			if (m_target != null) {
				//적이 죽어 있거나 사정거리를 벗어나면
				if (m_target.isDead || m_range < Vector2.Distance (transform.parent.position, m_target.transform.position))
					m_target = null;
			}

			//캐릭터 가져오기
			ICharacterInterface rayCharacter = getCharacterTarget (angle, team, isAlly, isMyself, isDead);

			if(rayCharacter != null){

				//적이 등록되어 있지 않으면
				if (m_target == null) {
					m_target = rayCharacter;
				}

				//등록된 적이 다르면
				else if (m_target != rayCharacter) {

					//등록되어 있는 적과 각도 계산
					float newAngle = PrepClass.angleCalculator (
                                        PrepClass.angleCalculator (
                                            transform.parent.position, 
                                            m_target.transform.position) 
                                        - angle);

					//등록된 적이 각도에 벗어나면
					if (Mathf.Abs (newAngle) > maxAngle) {

						//새로운 적 등록
						rayCharacter = getCharacterTarget (angle, team, isAlly, isMyself, isDead);
						//	레이에 걸린 적이나 null 등록
						m_target = rayCharacter;
					} 
					//각도에 벗어나지 않았으면
					else {

						//좌우로 5도씩 검사
						int angleCnt = (int)(newAngle / 5f);

						//
						for (int i = 1; i <= angleCnt; i++) {
						
							//양수검사
							rayCharacter = getCharacterTarget (
                                                            angle + (5f * (float)i), 
                                                            team, 
                                                            isAlly, 
                                                            isMyself, 
                                                            isDead
                                                            );
							//	레이에 걸린 적이나 null 등록
							m_target = rayCharacter;

							//적이 걸려있으면 
							if (m_target != null) break;

							//음수 검사
							rayCharacter = getCharacterTarget (
                                                            angle - (5f * (float)i), 
                                                            team, 
                                                            isAlly, 
                                                            isMyself, 
                                                            isDead
                                                            );
							//	레이에 걸린 적이나 null 등록
							m_target = rayCharacter;

							if (m_target != null) break;
						}

					}

				}
					
			}
			//캐릭터가 아니면
			return m_target;
		}
		catch{
			return null;
		}
	}


	ICharacterInterface getCharacterTarget(float angle, TYPE_TEAM team, bool isAlly, bool isMyself, bool isDead){
		
		//각도만큼 사정거리좌표 구하기
		float dirX = Mathf.Cos (angle * Mathf.Deg2Rad) * m_range + transform.parent.position.x;
		float dirY = Mathf.Sin (angle * Mathf.Deg2Rad) * m_range + transform.parent.position.y;
		m_direction.Set (dirX, dirY);

		//거리좌표 방향으로 레이발사
		RaycastHit2D[] rays = Physics2D.LinecastAll(transform.parent.position, m_direction);
		Debug.DrawLine (transform.parent.position, m_direction);

		ICharacterInterface owner = transform.parent.GetComponent<ICharacterInterface> ();

		foreach (RaycastHit2D ray in rays) {
			if (PrepClass.isCharacterTag (ray.collider.tag)) {
                //캐릭터 가져오기
				ICharacterInterface target = ray.collider.GetComponent<ICharacterInterface> ();
                //주어진 조건에 맞지 않으면 null 반환
				target = PrepClass.getCharacter (owner, target, isAlly, isDead, isMyself);

				if(target != null) return target;
			} 
		}
		return null;
	}

//	ICharacterInterface deadCheck(ICharacterInterface target, bool isDead){
//		if(isDead && target.isDead)
//			return target;
//		else if(!isDead && !target.isDead)
//			return target;
//		return null;
//	}

	/// <summary>
	/// 거리 등록
	/// </summary>
	/// <param name="range">Range.</param>
	public void setRange(float range){
		if (range < c_rangeMin)
			m_range = c_rangeMin;
		m_range = range;
	}

}


