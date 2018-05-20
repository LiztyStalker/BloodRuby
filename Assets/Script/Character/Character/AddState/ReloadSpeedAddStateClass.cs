using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 장전속도 값 변동
/// </summary>
public class ReloadSpeedAddStateClass : ValueAddStateClass
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
				valueArr = valueArr.Where (valueData => valueData.m_type != buffType).ToList<ValueData> ();
			}

			//데이터 가져오기
			valueArr = valueDataList.Where(valueData => valueData.m_typeValue == TYPE_VALUE.STATIC).ToList<ValueData>();
			if (valueArr.Count > 0) {
				return valueArr.Sum (staticValue => staticValue.m_value);
			}

			return variableCalculator (value, valueArr);

		}

		return value;
	}

	protected override float variableCalculator(float value, List<ValueData> valueDataArr){
		float variableValue = 1f + valueDataArr.Where (valueData => valueData.m_typeValue == TYPE_VALUE.PERCENT).Sum (valueData => valueData.m_value);
		if (variableValue < 0.01f) variableValue = 0.01f;

		variableValue = value * 1f / variableValue;
		variableValue -= valueDataArr.Where (valueData => valueData.m_typeValue == TYPE_VALUE.VALUE).Sum (valueData => valueData.m_value);
		if (variableValue < 0.01f) return 0.01f;
		return variableValue;
	}
}


