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
public class SacrificeTagBuffStateClass : BuffDataClass
{

//	[SerializeField] ParticleSystem m_useParticle;
//	float m_time = 3f;




	//public ICharacterInterface character{ set { m_character = value; } }
	//public float time{ set { m_time += value; } }

//	void Awake(){
//		buffStateSet(TYPE_BUFF_STATE_ACT.HIT);
//	}
	
	
//	void Start(){
//		startCoroutine (-1f, null);
//	}

//	public override bool buffEnd ()
//	{
//		return base.buffEnd ();
////		Debug.LogWarning ("buffEnd " + gameObject.name);
//	}

	public override bool useBuff (ICharacterInterface character, IBullet bullet)
	{
//		Debug.LogWarning ("owner act : " + ownerCharacter.playerName + " " + actCharacter.playerName);
		ownerCharacter.hitAction (bullet.characterCtrler.team, bullet);
		base.useBuff (character, bullet);
		return true;
		//피격시 시전자 체력 소모
	}

}


