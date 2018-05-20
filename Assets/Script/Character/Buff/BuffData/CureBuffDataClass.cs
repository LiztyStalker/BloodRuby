using System.Collections.Generic;
using UnityEngine;

public class CureBuffDataClass : BuffDataClass
{

//	[SerializeField] float m_time = 1f;
	[SerializeField] BuffDataClass m_buff;


//	List<ICharacterInterface> m_characterList = new List<ICharacterInterface>();


//	void Awake(){
//		buffStateSet (TYPE_BUFF_STATE.TIME);
//	}


//	void Start(){
//		//Debug.Log ("Start : " + GetInstanceID());
//		startCoroutine (m_time, null);
//	}

	public override void buffStart (ICharacterInterface ownerCharacter, ICharacterInterface setCharacter)
	{
		base.buffStart (ownerCharacter, setCharacter);
		healingArea ();
	}

//	public override void buffEnd ()
//	{
//		base.buffEnd ();
//	}

	void healingArea(){
		RaycastHit2D[] hits = Physics2D.BoxCastAll(ownerCharacter.transform.position, new Vector2(2 * Camera.main.orthographicSize * Camera.main.aspect, 2 * Camera.main.orthographicSize), 0f, Vector2.zero);
		//Debug.Log ("sac : " + hits.Length);

		foreach(RaycastHit2D hit in hits){


			if (PrepClass.isCharacterTag(hit.collider.tag)) {
				if (hit.collider.GetComponent<ICharacterInterface> ().team == ownerCharacter.team) {
					if (!hit.collider.GetComponent<ICharacterInterface> ().isDead) {
						hit.collider.GetComponent<ICharacterInterface> ().buffAdd (m_buff, ownerCharacter, hit.collider.GetComponent<ICharacterInterface> ());
					}
				}
			}
		}
	} 

	public override bool buffReplace ()
	{
		initTime ();
		return true;
	}

//	void OnDisable(){
//
//		if (m_characterList.Count > 0) {
//			foreach (ICharacterInterface bufCha in m_characterList) {
//				try{
//					Debug.Log("버프 강제 종료 : " + bufCha.name);
//					bufCha.addState.buffEnd (m_buff.GetType());
//				}
//				catch{
//				}
//			}
//		}
//
//		m_characterList.Clear ();
//	}
	
}


