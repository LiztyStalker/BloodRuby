using System.Collections;
using UnityEngine;

public class RebirthBuffDataClass : BuffDataClass
{
	[SerializeField] BuffDataClass m_buff;
//	[SerializeField] float m_time;

//	List<ICharacterInterface> m_characterList = new List<ICharacterInterface>();

//	void Awake(){
//		buffStateSet(TYPE_BUFF_STATE.CONTINUE);
//	}


//	void Start(){
//		startCoroutine (1f, null);
//	}

	public override void buffStart (ICharacterInterface ownerCharacter, ICharacterInterface setCharacter)
	{
		base.buffStart (ownerCharacter, setCharacter);
		buffArea ();

	}

	void buffArea(){
		//화면 전체 스캔
		//모든 아군 가져오기
		//아군에게 대리자 버프 등록 및 시간 삽입
		//주변에 있던 아군에게 한번만 실행


		RaycastHit2D[] hits = Physics2D.BoxCastAll(ownerCharacter.transform.position, new Vector2(2 * Camera.main.orthographicSize * Camera.main.aspect, 2 * Camera.main.orthographicSize), 0f, Vector2.zero);
		//Debug.Log ("sac : " + hits.Length);

		foreach(RaycastHit2D hit in hits){


			if (PrepClass.isCharacterTag(hit.collider.tag)) {

				ICharacterInterface neighbourCharacter = hit.collider.GetComponent<ICharacterInterface> ();

				if(neighbourCharacter != ownerCharacter){
					if (neighbourCharacter.team == ownerCharacter.team) {
						if (neighbourCharacter.isDead) {
//							hit.collider.GetComponent<ICharacterInterface> ().rebirthAction ();
							Debug.Log("hit : " + neighbourCharacter.playerName);
							neighbourCharacter.buffAdd (m_buff, ownerCharacter, neighbourCharacter);
						}
					}
				}
			}

		}
	}

//	void OnTriggerEnter2D(Collider2D col){
//		if (PrepClass.isCharacterTag (col.tag)) {
//			if (col.GetComponent<ICharacterInterface> () != m_character) {
//				if (col.GetComponent<ICharacterInterface> ().team == transform.parent.GetComponent<ICharacterInterface> ().team) {
//					col.GetComponent<ICharacterInterface> ().buffAdd (m_buff, col.GetComponent<ICharacterInterface> ());
////					if(!m_characterList.Contains(col.GetComponent<ICharacterInterface> ())){
////						m_characterList.Add (col.GetComponent<ICharacterInterface> ());
////					}
//				}
//			}
//		}
//	}

//	void OnTriggerExit2D(Collider2D col){
//		if (PrepClass.isCharacterTag (col.tag)) {
//			if (col.GetComponent<ICharacterInterface> () != m_character) {
//				if (col.GetComponent<ICharacterInterface> ().team == transform.parent.GetComponent<ICharacterInterface> ().team) {
//					BuffDataClass buffData = col.GetComponent<ICharacterInterface> ().addState.getBuff (m_buff.GetType());
//					if (buffData != null) {
//						m_characterList.Remove (col.GetComponent<ICharacterInterface> ());
//						buffData.buffEnd ();
//					}
//				}
//			}
//		}
//	}

}


