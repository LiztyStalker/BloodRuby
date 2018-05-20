using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//A0.8
public class UIContentViewClass : MonoBehaviour
{
	[SerializeField] Text m_nameText;
	[SerializeField] Text m_timeText;
	[SerializeField] Text m_contentText;

	const float c_viewTime = 3f;
	float time = 0f;
	IContentView m_contentsData = null;
	Coroutine m_coroutine = null;

	void Awake(){
		gameObject.SetActive (false);
	}

//	void Update(){
//		if (Input.touchCount > 0) {
//			gameObject.SetActive (false);
//		}
//	}

	public void setContentView(IContentView contentsData, Vector2 pos){

		time = c_viewTime;
		
		transform.position = pos;


		if (m_contentsData != contentsData) {
			m_contentsData = contentsData;

			viewContents ();
			gameObject.SetActive (true);

			if (m_coroutine == null) {
				m_coroutine = StartCoroutine (contentsCoroutine ());
			}

		}

	}


	IEnumerator contentsCoroutine(){

		while (time >= 0f) {
			viewContents ();
			time -= PrepClass.c_timeGap;
			yield return new WaitForSeconds (PrepClass.c_timeGap);
		}


		gameObject.SetActive (false);

	}

	void viewContents(){
		if (m_contentsData != null) {
			m_nameText.text = m_contentsData.name;

			if(m_contentsData.typeSkill == TYPE_SKILL.PASSIVE)
				m_timeText.text = TYPE_SKILL.PASSIVE.ToString();
			else
				m_timeText.text = string.Format ("{0:F1}", m_contentsData.time);

			m_contentText.text = m_contentsData.contents;
		}
	}

	public void closeContentView(){
		time = 0f;
		gameObject.SetActive (false);
	}

	void OnDisable(){
		m_contentsData = null;
		m_coroutine = null;
	}





}


