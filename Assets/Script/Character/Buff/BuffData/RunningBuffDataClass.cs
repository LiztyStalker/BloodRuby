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

public class RunningBuffDataClass : BuffDataClass
{

//	[SerializeField] float m_time;

//	void Awake(){
//		buffStateSet(TYPE_BUFF_STATE_ACT.HIT);
//	}
	
	
//	void Start(){
//		startCoroutine (m_time, null);
//	}
	
	public override void buffStart (ICharacterInterface ownerCharacter, ICharacterInterface actCharacter)
	{
		base.buffStart (ownerCharacter, actCharacter);
		addValueState (this);
	}
	
	public override bool buffEnd ()
	{
		returnValueState (this);
		ownerCharacter.resetSkillCoolTime (1);
		ownerCharacter.mosData.resetToggle ();

		return base.buffEnd ();
	}

	public override bool buffReplace ()
	{
		initTime ();
		return base.buffReplace ();
	}

	//사용시
	public override bool useBuff (ICharacterInterface useActCharacter){
		Debug.Log ("질주 스킬 종료");
		buffEnd ();
		return base.useBuff (useActCharacter);
	}

	//피격시
	public override bool useBuff(ICharacterInterface useActCharacter, IBullet bullet){
		Debug.Log ("질주 스킬 종료");
		buffEnd ();
		return base.useBuff (useActCharacter);
	}
}


