//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.36373
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;

public interface IMOSInterface
{

	MOSClass mosData{get;}
	int health {get;}
	float speed { get; }
	TYPE_MOS mos { get; }
	WeaponEquipmentClass weapon { get; set;}
	Sprite[] skillIcons{get;}
		
	IEquipmentInterface[] equipments{ get ; }

	SkillClass[] skillData{get;}

	void gameReady (ICharacterInterface player, int[] equipmentSlots);

	void attackAction (ICharacterInterface character, Vector3 shootPos);

	void deadAction ();

	void moveAction ();

	void reloadAction ();

	void hitAction ();

	void rebirthAction ();

	void skillAction(ICharacterInterface player, int slot);

	void skillGuideLine(ICharacterInterface player, int slot);

	float getSkillCoolTime(int slot);
}

