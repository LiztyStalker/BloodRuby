using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOSSniperDataClass : MOSDataClass {

//	bool m_isTelescope;
//
//
//	/// <summary>
//	/// 망원경 상태
//	/// </summary>
//	/// <param name="isTelescope">If set to <c>true</c> is telescope.</param>
//	public override void telescopeAction(bool isTelescope){
//
//		m_isTelescope = isTelescope;
//
//		if (!isTelescope) {
//			idleAction ();
//		}
//		else{
//
//			mosAnimation = TYPE_ANIMATION.TELESCOPE;
//			setAnimation (mosData.getAnimationKey (mosAnimation), false, false, 1f, 1f);
//
//			//무기 교체 버프가 걸려있으면 -
//			//
//			if (character.addState.getBuff (typeof(WeaponChangeBuffDataClass)) != null) {
//				activeSlot = 2;
//				skillData [activeSlot].skillAction (character);
//				animationExtend = "";
//				character.resetSkillCoolTime (activeSlot);
//			}
//		}
//
//	}
//
//
//	/// <summary>
//	/// 스킬 상태
//	/// </summary>
//	/// <param name="slot">Slot.</param>
//	public override bool skillAction(ICharacterInterface player, int slot, float time){

//		//		Debug.Log ("스킬 : " + m_skillAction [slot].name);
//		if (skillData [slot] != null) {
//			Debug.Log("스킬 발동 : " + skillData[slot].name);
//
//			mosAnimation = TYPE_ANIMATION.SKILL0 + slot;
//			player.closeSkillGuideLine ();
//
//
//			if (mosAnimation == TYPE_ANIMATION.SKILL3)
//				mosAnimation = TYPE_ANIMATION.TELESCOPE;
//
//
//			//스킬 1 사용시 무기변환이 있으면
////			if (mosAnimation == TYPE_ANIMATION.SKILL1) {
////				if (player.addState.getBuff (typeof(WeaponChangeBuffDataClass)) != null) {
////					//알람
////					return TYPE_BUFF_STATE.CONTINUE;
////				}
////			}
//
//			//헤드샷이 무기변경되어 있으면 작동 불가로 변환
//
//			//애니메이션이 없으면 즉시 실행
//			if (!setAnimation (mosData.getAnimationKey (mosAnimation), false, false, time, time)) {
//				return skillRun (player, slot);
//			} 
//			//애니메이션이 있음
//			else {
//				//애니메이션 내에 있는 이벤트에 맞춰 행동
//				if (skillData [slot].isAnimationEvent) {
//					//있으면 스킬 슬롯 저장
//					activeSlot = slot;
//					return true;//skillData [slot].typeSkillState;
//				}
//				//애니메이션은 있으나 이벤트는 없어 즉시 시전
//				else {
//					return skillRun (player, slot);
//				}
//			}
//		} 
//
//		//		Debug.Log ("typeState : " + m_typeSkillState);
//
//		return true;
//	}
//
//	public override void attackAction(UICharacterClass characterCtrler, Vector3 shootPos, float time){
//		//공격 애니메이션
//		//무기에서 탄환 공격
//		//쿨타임 진행
//		//Debug.Log("weapon : " + weapon.name);
//		this.characterCtrler = characterCtrler;
//
//		//공격 등록 - 스켈레톤 애니메이션 실행
//		//스켈레톤 애니메이션에서 이벤트 발생
//		//이벤트 실행
////		weapon.attackAction (characterCtrler, shootPos);
//
//
//
//
//		if (mosAnimation == TYPE_ANIMATION.RELOAD)
//			return;
//
//		setBodyAnimation(mosData.getAnimationKey (TYPE_ANIMATION.ATTACK), false, time);
//
//	}
//
//
//	/// <summary>
//	/// 종료 이벤트 - AnimationState.End에 등록
//	/// </summary>
//	/// <param name="trackEntry">Track entry.</param>
//	protected override void endEvent(Spine.TrackEntry trackEntry){
//		//if(재장전 trackEntry이면) reload = false;
//		//애니메이션 재장전 이름 일반화 필요
//		Debug.Log ("TrackEntry " + trackEntry);
//
//
//		//망원경, 탑승, 사망은 끝에서 멈춰야함
//
//		if (character.isDead)
//			return;
//
//
//
//		//장전이 끝나면
//		if (trackEntry.ToString () == "Reload" + animationExtend) {
//
//			if (!m_isTelescope) {
//				mosAnimation = TYPE_ANIMATION.IDLE;
//			}
//			else{
//				mosAnimation = TYPE_ANIMATION.TELESCOPE;
//				mosSkeleton.addBodyAnimation (getNameKey (mosData.getAnimationKey (TYPE_ANIMATION.TELESCOPE)), 0.1f);				
//			}
//		}
//
//
//		if (!m_isTelescope) {
//
//
//			if (isMove)
//				mosSkeleton.addBodyAnimation (getNameKey (mosData.getAnimationKey (TYPE_ANIMATION.MOVE)), 1f);
//			else
//				mosSkeleton.addBodyAnimation (getNameKey (mosData.getAnimationKey (TYPE_ANIMATION.IDLE)), 1f);		
//		}
//
//
//
//
//
//
//		//m_isReload = false;
//	}


//
//	protected override bool skillRun (ICharacterInterface player, int slot)
//	{
//		base.skillRun (player, slot);
//		if (player.addState.getBuff (typeof(WeaponChangeBuffDataClass)) != null) {
//			telescopeAction (false);
//		}
//		return true;
//	}

}
