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

public class BeliefBuffDataClass : BuffDataClass
{

//	int m_count = 0;

//	protected override void Start(){
//		base.Start ();
//	}
	
	public override bool buffEnd ()
	{
		initCount ();
		return base.buffEnd ();
	}

	void beliefBuff(){
		
		initCount ();

		m_count = (actCharacter.maxHealth - actCharacter.nowHealth) / 10;

		if (m_count > 0) {
			for(int i = 0; i < valueState.Length; i++){
				valueState[i].value = getAssistBuffData (valueState[i].defaultValue) * (float)m_count;
				actCharacter.addState.setValue (valueState[i], this);
				//Debug.Log("setdamage : " + m_addState.valueCalculator(100f, valueState[i]));
			}
		}
		
	}
	
	void initCount(){
		if (m_count > 0) {
			for(int i = 0; i < valueState.Length; i++){
				valueState[i].value = getAssistBuffData (valueState[i].defaultValue) * (float)m_count;
				actCharacter.addState.returnValue (valueState[i], this);
				//Debug.Log("returndamage : " + m_addState.valueCalculator(100f, valueState[i]));
			}
		}
	}


	public override bool useBuff (ICharacterInterface useActcharacter)
	{
		beliefBuff ();
		return false;
	}

	public override bool useBuff (ICharacterInterface useActCharacter, IBullet bullet)
	{
		return useBuff (useActCharacter);
	}

}

