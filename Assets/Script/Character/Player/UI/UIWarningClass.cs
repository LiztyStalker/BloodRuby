using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIWarningClass : MonoBehaviour
{
	const float c_rateLength = 0.1f;
	const float c_flipSpeed = 1f;

	Image m_warningImage;
	float m_rate = 0f;
	bool m_flip = false;

	Color m_color = Color.white;

	public void setRate(float rate){m_rate = rate;}

	void OnEnable(){
		m_warningImage = GetComponent<Image> ();
		m_rate = 0f;
		StartCoroutine (WarningFlipCoroutine());
	}


	IEnumerator WarningFlipCoroutine(){

		float nowRate = m_rate;

		while (gameObject.activeSelf) {

			if (m_rate != 0f) {

				//아래
				if (m_flip) {
					nowRate -= PrepClass.c_timeGap * c_flipSpeed;
					if (nowRate < m_rate - c_rateLength)
						m_flip = !m_flip;
					
				}


			//위
			else {
					nowRate += PrepClass.c_timeGap * c_flipSpeed;
					if (nowRate > m_rate + c_rateLength)
						m_flip = !m_flip;
				}
				m_color.a = nowRate;

			} else {
				m_color.a = m_rate;
			}

			m_warningImage.color = m_color;

			yield return new WaitForSeconds (PrepClass.c_timeGap);
		}


	}





}


