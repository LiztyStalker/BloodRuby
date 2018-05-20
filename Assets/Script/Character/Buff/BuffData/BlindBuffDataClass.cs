using System.Collections;
using UnityEngine;

public class BlindBuffDataClass : BuffDataClass
{
	[SerializeField] float m_missPercent;

	//A0.8 시야방해 수정
	bool isActive = false;
//	[SerializeField] float m_time;

//	float m_missTime;

//	void Awake(){
//		buffStateSet(TYPE_BUFF_STATE_ACT.ATTACK);
//	}
		
	protected override void Start(){
		//A0.8 시야방해 수정
		m_isRemove = false;
		setBuffLoopDelegate (checkBuff);
		isActive = false;
		base.Start ();

		if (buffParticle != null) {
			buffParticle.Clear ();
			buffParticle.Stop ();
		}

//		startCoroutine (m_time, checkBuff);
	}

	void checkBuff(){

//		Debug.Log ("time : " + m_runTime + " " + m_maxTime);

		if (runTime >= maxTime) {
			if (isActive) {
				resetConstraint ();
				//A0.8 시야방해 수정
				isActive = false;

				if (buffParticle != null) {
					buffParticle.Clear ();
					buffParticle.Stop ();
				}
				//gameObject.SetActive (false);
			}
		} 
	}
		

	public override bool useBuff (ICharacterInterface character)
	{
		//공격도중회피율증가


		//A0.8 시야방해 수정
		if (!isActive) {
			isActive = true;
			if (buffParticle != null) {
				Debug.Log ("play");

				buffParticle.Play ();
			}
		}

		m_runTime = 0f;

		Debug.Log ("attack");

		return false;
	}


	public override bool useBuff (ICharacterInterface character, IBullet bullet)
	{
//		valueState

		float missPercent = getAssistBuffData (m_missPercent);

		//A0.8 시야방해 수정
		if (isActive) {
			if (Random.Range (0f, 1f) < missPercent) {
				Debug.Log ("회피");
				return true;
			}
		}
		return false;
	}

}


