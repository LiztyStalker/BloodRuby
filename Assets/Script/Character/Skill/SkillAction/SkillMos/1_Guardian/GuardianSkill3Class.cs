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
public class GuardianSkill3Class : SkillClass, ISkillMOSInterface
{

//	[SerializeField] 버프
//	[SerializeField] float m_time;
//	[SerializeField] float m_rate;

//	[SerializeField] BuffDataClass m_buff;

//	public override void initSkill (ICharacterInterface player){
//	}

	public override bool skillAction(ICharacterInterface player){
		setParticle (player.transform.position, player.transform);
		player.buffAdd (skillBuffData.getBuffData (), player, player);
		return base.skillAction (player);
	}

}


