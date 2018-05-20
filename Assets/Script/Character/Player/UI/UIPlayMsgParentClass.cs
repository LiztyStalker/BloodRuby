using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class UIPlayMsgParentClass : MonoBehaviour
{

    [SerializeField] Text m_text;
    float m_timer = 0f;
    protected Coroutine m_coroutine = null;
	public delegate void msgFlagDelegate();


	void Start(){
		gameObject.SetActive (false);
	}

	protected void setMsg(string msg, float timer, msgFlagDelegate del = null)
	{
		m_timer = 0f;
		m_text.text = msg;

		if (m_coroutine == null) {
			m_coroutine = StartCoroutine (msgCoroutine (timer, del));
		}
			
	}

	protected IEnumerator msgCoroutine(float timer, msgFlagDelegate del = null)
    {

		while (m_timer <= timer || timer < 0f)
        {
			if (timer >= 0f) m_timer += PrepClass.c_timeGap;
			if(del != null) del ();

            yield return new WaitForSeconds(PrepClass.c_timeGap);
        }

        m_coroutine = null;
        gameObject.SetActive(false);
    }

	protected virtual void OnDisable(){
		if (m_coroutine != null)
			StopCoroutine (m_coroutine);
		
		m_coroutine = null;
		gameObject.SetActive(false);
	}



}

