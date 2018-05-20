using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOSAssaultDataClass : MOSDataClass {



//	protected override bool skillRun(ICharacterInterface player, int slot){
//		Debug.Log ("SkillRun : " + slot);
//
//
//		if (!skillData [slot].skillAction (player))
//			return false;
//
//		m_typeSkillState = skillData [slot].typeSkillState;
//
//
//		Debug.Log ("typeSkillState : " + m_typeSkillState + " " + mosAnimation);
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
//				//각각의 스킬 사용시
//				switch(slot){
//				//총검돌격 사용시
//				case 1:
//					//은엄폐 버프가 걸려있으면
//					//은엄폐 쿨타임
////					animationExtend = "_" + skillData [slot].getBuffData ().GetType ().ToString ().Replace ("BuffDataClass", "");
////
////					mosAnimation = TYPE_ANIMATION.MOVE;
//
//					if (player.addState.getBuff (typeof(CoverBuffDataClass)) != null) {
//						activeSlot = 2;
//						skillData [activeSlot].skillAction (player);
//						return true;//TYPE_BUFF_STATE.CONTINUE;
//					}
//					break;
//				//은엄폐 사용시
//				case 2:
//					//총검돌격버프 걸려있으면 해제
//					//총검돌격버프 쿨타임
//					if (player.addState.getBuff (typeof(BayonetChargeBuffDataClass)) != null) {
////						animationExtend = "";
//						activeSlot = 1;
//						skillData [activeSlot].skillAction (player);
//						mosAnimation = TYPE_ANIMATION.IDLE;
//						return true;//TYPE_BUFF_STATE.CONTINUE;
//					}
//
//					break;
//				}
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
//
//		Debug.Log ("mosAnimation : " + mosAnimation);
//		return true;
//	}
}
