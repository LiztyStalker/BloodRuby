using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIKillListBarClass : MonoBehaviour
{
	[SerializeField] Image m_killIcon;
	[SerializeField] Text m_killerText;
	[SerializeField] Image m_AttackIcon;
	[SerializeField] Text m_deathText;
	[SerializeField] Image m_deathIcon;

	const float c_time = 3f;
	float timer = 0f;

	Coroutine coroutine_killList = null;
		
	void OnEnable(){
		coroutine_killList = StartCoroutine (killListBarCoroutine ());
	}

	public void setKillListBar(ICharacterInterface deathCharacter, ICharacterInterface inflictCharacter, IBullet bullet){

//		Debug.LogWarning ("killData : " + deathCharacter + " " + inflictCharacter);

		if (inflictCharacter != null) {
			m_killIcon.sprite = inflictCharacter.mosData.mosData.characterRound;
			m_killerText.text = string.Format ("{0}", inflictCharacter.playerName);

			if (inflictCharacter.team == TYPE_TEAM.TEAM_0)
				m_killerText.color = Color.cyan;
			else
				m_killerText.color = PrepClass.getFlagColor (inflictCharacter.team);
			
		} else {
			m_killIcon.sprite = null;
			m_killerText.text = "";
		}




//		Debug.Log ("killType : " + type);




		//사살 방식
		m_AttackIcon.sprite = bullet.weaponSprite;

		m_deathText.text = string.Format ("{0}", deathCharacter.playerName);

		if (deathCharacter.team == TYPE_TEAM.TEAM_0)
			m_deathText.color = Color.cyan;
		else
			m_deathText.color = PrepClass.getFlagColor (deathCharacter.team);
		

		m_deathIcon.sprite = deathCharacter.mosData.mosData.characterRound;

		if (gameObject.activeSelf) {
			if(coroutine_killList != null)
				timer = c_time;
		} else {
			gameObject.SetActive (true);
		}
	}


	IEnumerator killListBarCoroutine(){
		timer = c_time;
		while (timer >= 0f) {
			timer -= PrepClass.c_timeGap;
			yield return new WaitForSeconds (PrepClass.c_timeGap);
		}
		gameObject.SetActive (false);
	}





}


