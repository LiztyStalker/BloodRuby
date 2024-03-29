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
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public struct ValueData{
	public Type m_type; //버프 타입
	public string m_group; //버프 그룹
	public int m_instanceID; //버프 인스턴스 아이디
	public TYPE_VALUE m_typeValue; //값 형 고정, 퍼센트, 가변
	public float m_value; //값
}


public abstract class ValueAddStateClass : MonoBehaviour
{



	/// 기록된 값
	[SerializeField] protected TYPE_VALUE m_typeValue;
	[SerializeField] protected float m_value = 0f;
//	[SerializeField] protected float m_percent = 1f;

	List<ValueData> m_valueDataList = new List<ValueData> ();

	protected List<ValueData> valueDataList{ get { return m_valueDataList; } }




	//변동 값
    float m_actValue = 0f;
//	float m_actPercent = 1f;


//	protected List<ValueData> valueDataList{ get { return m_valueDataList; } }

	public float defaultValue{ get { return m_value; } }
//	public float defaultPercent{ get { return m_percent; } }

	public float value { get { return (m_actValue == 0f) ? m_value : m_actValue; } set { m_actValue = value; } }
//	public float percent { get { return m_actPercent; }  set { m_actPercent = value; } }
	public TYPE_VALUE typeValue{get{return m_typeValue;}}

	public virtual void addValue(ValueAddStateClass data, BuffDataClass buffData){

		//같은 그룹의 버프가 있으면 갱신

		Nullable<ValueData> valueD = valueDataList.Where (valueData => valueData.m_group == buffData.group).SingleOrDefault ();
		if (valueD.HasValue) {
			valueDataList.Remove (valueD.Value);
		}


		valueDataList.Add (changeValueData (data, buffData));

	}
	
	
	public virtual void returnValue(ValueAddStateClass data, BuffDataClass buffData){


		if (valueDataList.Count > 0) {
			Nullable<ValueData> valueD = valueDataList.Where (valueData => valueData.m_instanceID == buffData.GetInstanceID()).SingleOrDefault ();
			if (valueD.HasValue) valueDataList.Remove (valueD.Value);
		}

	}

	public virtual float valueCalculator(float value){
		if (valueDataList.Count > 0) {

			List<ValueData> staticArr = valueDataList.Where(valueData => valueData.m_typeValue == TYPE_VALUE.STATIC).ToList<ValueData>();
			if (staticArr.Count > 0) {
				return staticArr.Sum (staticValue => staticValue.m_value);
			}

			return variableCalculator (value, valueDataList);
		}
		return value;


	}

	public virtual float valueCalculator(float value, Type[] excBuffType){
		//Debug.Log (GetType() + " " + value + " " + this.percent + " " + this.value);

		//버프제외
		if (valueDataList.Count > 0) {
			List<ValueData> valueArr = valueDataList.ToList<ValueData> ();

			foreach (Type buffType in excBuffType) {
				valueArr = valueArr.Where (valueData => valueData.m_type != buffType).ToList<ValueData> ();
			}

			//데이터 가져오기
			valueArr = valueDataList.Where (valueData => valueData.m_typeValue == TYPE_VALUE.STATIC).ToList<ValueData> ();
			if (valueArr.Count > 0) {
				return valueArr.Sum (staticValue => staticValue.m_value);
			}

			return variableCalculator (value, valueArr);
		}
		return value;
	}


	ValueData changeValueData(ValueAddStateClass data, BuffDataClass buffData){
		ValueData valueData = new ValueData ();
		valueData.m_type = buffData.GetType();
		valueData.m_group = buffData.group;
		valueData.m_instanceID = buffData.GetInstanceID();
		valueData.m_typeValue = data.typeValue;
		valueData.m_value = data.value;
		return valueData;
	}

	protected virtual float variableCalculator(float value, List<ValueData> valueDataArr){

		//퍼센트 합
		float variableValue = 1f + valueDataArr.Where (valueData => valueData.m_typeValue == TYPE_VALUE.PERCENT).Sum (valueData => valueData.m_value);
//		Debug.Log ("valuableValue : " + variableValue);
		if (variableValue <= 0f) variableValue = 0f;
		variableValue *= value;

		//가변변수 합
		variableValue += valueDataArr.Where (valueData => valueData.m_typeValue == TYPE_VALUE.VALUE).Sum (valueData => valueData.m_value);
		if (variableValue <= 0f) return 0f;
		return variableValue;
	}
}


