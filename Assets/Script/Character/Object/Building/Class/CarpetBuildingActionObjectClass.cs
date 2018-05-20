using System;
using System.Collections.Generic;
using UnityEngine;

public class CarpetBuildingActionObjectClass : BuildingActionObjectClass
{
	[SerializeField] BuffDataClass m_buffData;
	[SerializeField] float m_time;


	List<ICharacterInterface> m_characterList = new List<ICharacterInterface> ();

	protected override void Start(){
		base.Start ();
		if(m_time <= 0f)
			Destroy (gameObject, 1f);
		else
			Destroy (gameObject, m_time);
	}

//	public override void initAction(UICharacterClass characterCtrler, BuildingObjectClass buildingObject, CharacterFrameClass buildingFrame){
//		base.initAction (characterCtrler, buildingObject, buildingFrame);
//	}

	void Update(){
		foreach (ICharacterInterface character in m_characterList) {
			character.buffAdd (m_buffData, m_characterCtrler.character, character);			
		}
	}


	void OnTriggerEnter2D(Collider2D col){
//		if(m_characterCtrler != null && m_characterCtrler.character != null){

			if(PrepClass.isCharacterTag(col.tag)){

				ICharacterInterface enemyCharacter = col.GetComponent<ICharacterInterface>();

				if(m_characterCtrler.team != enemyCharacter.team){
					if(!enemyCharacter.isDead){
						enemyCharacter.buffAdd (m_buffData, m_characterCtrler.character, enemyCharacter);
						if (!m_characterList.Contains (enemyCharacter))
							m_characterList.Add (enemyCharacter);
					}
				}
			}
//		}
	}


	void OnTriggerExit2D(Collider2D col){
//		if (m_characterCtrler != null && m_characterCtrler.character != null) {
		if (m_time <= 0f) {
			if (PrepClass.isCharacterTag (col.tag)) {
				ICharacterInterface enemyCharacter = col.GetComponent<ICharacterInterface> ();

				if (m_characterCtrler.team != enemyCharacter.team) {

					BuffDataClass buffData = enemyCharacter.addState.getBuff (m_buffData.GetType ());
					if (buffData != null) {
						if (buffData.buffEnd ()) {
							m_characterList.Remove (enemyCharacter);
						}
					}
				}
			}
		}
//		}
	}


}


