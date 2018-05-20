using System;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayMsgCastingClass : UIPlayMsgParentClass
{
//	[SerializeField] Text m_castingText;
	[SerializeField] Text m_castingTimeText;
	[SerializeField] Slider m_castingSlider;


	CastingParticleBuffDataClass m_castingBuff;


	public void setCastAlarm(string msg, bool isLoop)
	{
		Debug.Log ("msg");
		if(isLoop)
			setMsg(msg, -1f);
		else
			setMsg(msg, 3f);
	}

	public void setCastAlarm(string msg, CastingParticleBuffDataClass castingBuff){
		m_castingSlider.gameObject.SetActive (true);
		m_castingBuff = castingBuff;
		Debug.Log ("setCastAlarm : " + castingBuff.maxTime);
		setMsg (msg, castingBuff.maxTime, castingBar);
	}


	void castingBar(){
		Debug.Log ("CastingBar");
		m_castingSlider.value = PrepClass.ratioCalculator (m_castingBuff.runTime, m_castingBuff.maxTime - PrepClass.c_timeGap);
		m_castingTimeText.text = string.Format ("{0:f1}", m_castingBuff.maxTime - m_castingBuff.runTime - PrepClass.c_timeGap);
	}

	protected override void OnDisable(){
		base.OnDisable ();
		m_castingSlider.gameObject.SetActive (false);
	}
}


