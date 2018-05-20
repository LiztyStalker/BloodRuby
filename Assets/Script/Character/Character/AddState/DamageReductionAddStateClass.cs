using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 데미지 감소 값 변동
/// </summary>
public class DamageReductionAddStateClass : ValueAddStateClass
{


	public override float valueCalculator(float value){//, BuffDataClass[] buffData){

		if (valueDataList.Count > 0) {



			List<ValueData> valueArr = valueDataList.Where(valueData => valueData.m_typeValue == TYPE_VALUE.STATIC).ToList<ValueData>();
			if (valueArr.Count > 0) {
				return valueArr.Sum (staticValue => staticValue.m_value);
			}


			return variableCalculator (value, valueDataList);

		}
		return value;


		//		return value * this.percent + this.value;
	}

	public override float valueCalculator(float value, Type[] excBuffType){
		//Debug.Log (GetType() + " " + value + " " + this.percent + " " + this.value);

		//버프제외
		if (valueDataList.Count > 0) {
			List<ValueData> valueArr = valueDataList.ToList<ValueData>();


			foreach (Type buffType in excBuffType) {
//				Debug.Log ("buffLength1 : " + valueArr.Length + " " + buffType);
				valueArr = valueArr.Where (valueData => valueData.m_type != buffType).ToList<ValueData> ();
//				Debug.Log ("buffLength2 : " + valueArr.Length);
			}

			//데이터 가져오기
			valueArr = valueArr.Where(valueData => valueData.m_typeValue == TYPE_VALUE.STATIC).ToList<ValueData>();
			if (valueArr.Count > 0) return valueArr.Sum (staticValue => staticValue.m_value);


			return variableCalculator (value, valueArr);

		}

		return value;
		//		return value * this.percent + this.value;
	}

	protected override float variableCalculator (float value, List<ValueData> valueDataArr)
	{
		float cal_value = value;

		float variableValue = 1f + valueDataArr.Where (valueData => valueData.m_typeValue == TYPE_VALUE.PERCENT).Sum (valueData => valueData.m_value);
//		Debug.Log ("variableValue : " + variableValue);

		if (variableValue >= 2f)
			return 0f;
		else
			cal_value = value * (2f - variableValue);

		cal_value -= valueDataArr.Where (valueData => valueData.m_typeValue == TYPE_VALUE.VALUE).Sum (valueData => valueData.m_value);

		if (cal_value <= 0f)
			return 0f;

//		Debug.Log ("cal : " + cal_value);
		return cal_value;
	}

}


