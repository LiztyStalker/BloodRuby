using System.Collections;
using UnityEngine;

public class ItemZoneObjectClass : ObjectClass
{


	[SerializeField] ItemObjectClass[] m_itemObjects;
	[SerializeField] float m_time = 10;

	//체력회복 50 100 full
	//쿨타임감소 30%
	//실드 50 100 생성
	//블러드루비 1
	//배틀포인트 5 10 20
	//무적 5
	//각각의 레어도 판별해서 가져옴
	//아이템 패키지에서 가져옴

	float m_runTime = 0f;



	void OnEnable(){
		StartCoroutine (itemCreateCoroutine ());
	}



	public override void useObject (){
		
	}

	/// <summary>
	/// 사용중인 오브젝트 해제
	/// </summary>
	public override void releaseObject(){
		
	}

	ItemObjectClass getRandomItem(){
		return m_itemObjects[Random.Range(0, m_itemObjects.Length)];
	}


	IEnumerator itemCreateCoroutine(){
		while (gameObject.activeSelf) {



			if (transform.childCount == 0) {
				m_runTime += PrepClass.c_timeGap;
				if (m_time < m_runTime) {
					ItemObjectClass item = (ItemObjectClass)Instantiate (getRandomItem (), transform.position, new Quaternion ());
					item.transform.SetParent (transform);
					m_runTime = 0f;
				} 
			} else {
				m_runTime = 0f;
			}

			//일정시간이 지나면 아이템 생성

			yield return new WaitForSeconds (PrepClass.c_timeGap);
		}
	}

	
}


