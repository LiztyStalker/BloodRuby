using System;
using UnityEngine;
using UnityEngine.UI;



public class UIKillListClass : MonoBehaviour
{

	UIKillListBarClass[] m_killListBarArray;
	int m_index = -1;

	void Awake(){
		m_killListBarArray = GetComponentsInChildren<UIKillListBarClass> ();
	}

	void OnEnable(){
		
		m_index = -1;
		foreach (UIKillListBarClass killListBar in m_killListBarArray) {
			killListBar.gameObject.SetActive (false);
		}
	}


	public void setKillListBar(ICharacterInterface deathCharacter, ICharacterInterface inflictCharacter, IBullet bullet){
		//다음 인덱스에 데이터를 넣고 보여줌



		if (gameObject.activeSelf) {
			int index = getIndex ();
//			Debug.LogWarning ("index : " + index);
			m_killListBarArray [index].setKillListBar (deathCharacter, inflictCharacter, bullet);
			m_killListBarArray [index].transform.SetAsLastSibling ();
		}
		//위치 변경
	}


	/// <summary>
	/// 인덱스 큐
	/// </summary>
	/// <returns>The index.</returns>
	int getIndex(){
		if (++m_index >= m_killListBarArray.Length) m_index = 0;
		return m_index;
	}
}


