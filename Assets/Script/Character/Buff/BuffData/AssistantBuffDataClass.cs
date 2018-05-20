using System;
using UnityEngine;
using System.Linq;

public class AssistantBuffDataClass : BuffDataClass
{
	//버프 보조
	//버프를 더욱 강하게 하거나 약하게 하기


	/// <summary>
	/// 보조할 버프
	/// </summary>
	[SerializeField] BuffDataClass[] m_buffData;

	/// <summary>
	/// 변수 값 타입
	/// </summary>
	[SerializeField] TYPE_VALUE m_typeValue;
	/// <summary>
	/// 추가될 변수
	/// </summary>
	[SerializeField] float m_assistValue;

//	void Start(){
//		startCoroutine (-1f, null);
//	}

	public override void buffStart (ICharacterInterface ownerCharacter, ICharacterInterface setCharacter)
	{
		base.buffStart (ownerCharacter, setCharacter);
	}

	public override bool buffEnd ()
	{
		return base.buffEnd ();
	}

	/// <summary>
	/// 버프 보조값 계산하기
	/// </summary>
	/// <returns>The assist buff data.</returns>
	/// <param name="buffType">Buff type.</param>
	public float getAssistBuffCalculator(Type buffType, float value){
		if (m_buffData != null) {
			if (m_buffData.Where (buffData => buffData.GetType () == buffType).SingleOrDefault () != null) {
				switch (m_typeValue) {
				//고정값
				case TYPE_VALUE.STATIC:
					return m_assistValue;
				//추가값
				case TYPE_VALUE.VALUE:
					return value + m_assistValue;
				//퍼센트 값
				case TYPE_VALUE.PERCENT:
					return value + value * m_assistValue;
				}
			}
		}
		return value;
	}
}


