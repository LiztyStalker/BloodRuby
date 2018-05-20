using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOSHeavyDataClass : MOSDataClass {




//
//	protected override bool skillRun(ICharacterInterface player, int slot){
//		Debug.Log ("SkillRun : " + slot);
//
//		if (!skillData [slot].skillAction (player))
//			return false;
//
//		m_typeSkillState = skillData [slot].typeSkillState;
//
//		Debug.Log ("typeSkillState : " + m_typeSkillState);
//
//
//		//토글형이면
//		//아니면
//
//		//토글형이면
//		if (m_typeSkillState == TYPE_BUFF_STATE.TOGGLE) {
//			//
//
//
//
//
//			//현재버프가 있으면 버프 애니메이션 등록
//			if (player.addState.getBuff (skillData [slot].getBuffData ().GetType ()) != null) {
//
//
//
//				switch (slot) {
//				//거치버프를 사용하면
//				case 0:
//					//질주버프가 걸려있으면 해제
//					//질주버프 쿨타임
//
//					//거치 애니메이션
//					animationExtend = "_" + skillData [slot].getBuffData ().GetType ().ToString ().Replace ("BuffDataClass", "");
//
//					if (player.addState.getBuff (typeof(RunningBuffDataClass)) != null) {
//						activeSlot = 1;
//						skillData [activeSlot].skillAction (player);
//						return true;
//					}
//					break;
//				//질주버프 스킬 사용시
//				case 1:
//					//거치버프 걸려있으면 해제
//					//거치버프 쿨타임
//					if (player.addState.getBuff (typeof(MountBuffDataClass)) != null) {
//						activeSlot = 0;
//						skillData [activeSlot].skillAction (player);
//						animationExtend = "";
//						idleAction ();
//						return true;
//					}
//					break;
//				}
//
//
//
//			}
//
//			//버프가 없으면 애니메이션 초기화
//			else {
//				m_typeSkillState = TYPE_BUFF_STATE.CONTINUE;
//				animationExtend = "";
//				idleAction ();
//			}
//		} 
//
//		mosAnimation = TYPE_ANIMATION.IDLE;
//		activeSlot = slot;
//		return true;
//	}
}
