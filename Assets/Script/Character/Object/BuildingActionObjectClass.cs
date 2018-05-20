using System;
using UnityEngine;

public abstract class BuildingActionObjectClass : MonoBehaviour
{
	protected BuildingActionObjectClass m_buildingAction;
	protected BuildingObjectClass m_buildingObject;
	protected UICharacterClass m_characterCtrler = null;
	protected CharacterFrameClass m_buildingFrame;
	protected Sprite m_weaponSprite;
		
	protected virtual void Start(){
		m_buildingAction = GetComponent<BuildingActionObjectClass>();
	}
		
	public virtual void initAction(UICharacterClass characterCtrler, BuildingObjectClass buildingObject, CharacterFrameClass buildingFrame){
		m_characterCtrler = characterCtrler;
		m_buildingObject = buildingObject;
		m_buildingFrame = buildingFrame;
	}

	public virtual void useAction(){}

	public void removeObject(GameObject obj){
		Destroy(obj);
	}

	public void setWeaponSprite(Sprite weaponSprite){
		m_weaponSprite = weaponSprite;
		Debug.Log ("setWeapon : " + m_weaponSprite);
	}


}


