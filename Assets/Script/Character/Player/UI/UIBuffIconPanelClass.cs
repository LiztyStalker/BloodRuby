using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIBuffIconPanelClass : MonoBehaviour
{

	const int c_buffIconCnt = 10;

	[SerializeField] UIBuffIconClass m_buffIcon;
	[SerializeField] GameObject m_buffPanel;

	//오브젝트 풀링
	List<UIBuffIconClass> m_buffIconList = new List<UIBuffIconClass>();

	public void gameUpdate(){
		foreach (UIBuffIconClass uiBuff in m_buffIconList) {
			uiBuff.buffUpdate ();
		}
	}

	/// <summary>
	/// 버프 아이콘 활성
	/// </summary>
	/// <returns>활성화된 버프.</returns>
	/// <param name="buffData">버프 데이터.</param>
	public BuffDataClass buffAdd(BuffDataClass buffData, UIPlayerCtrlClass playerCtrler){
		Debug.Log ("버프 삽입");
		UIBuffIconClass buffIcon = (UIBuffIconClass)Instantiate (m_buffIcon);

		float size = GetComponent<RectTransform>().rect.height;
		buffIcon.GetComponent<RectTransform>().sizeDelta = new Vector2(size, size);
		buffIcon.setBuff (buffData, playerCtrler);

		buffIcon.transform.SetParent (m_buffPanel.transform);
		m_buffIconList.Add (buffIcon);
		return buffData;
	}

	/// <summary>
	/// 버프 아이콘 종료
	/// </summary>
	/// <returns>종료 여부 : true 정상 종료</returns>
	/// <param name="buffData">버프 데이터.</param>
	public bool buffEnd(BuffDataClass buffData){
		UIBuffIconClass uiBuffData = m_buffIconList.Where (uiBuff => uiBuff.buffData == buffData).SingleOrDefault ();
		if (uiBuffData != null) {
			if (m_buffIconList.Remove (uiBuffData)) {
				Destroy (uiBuffData.gameObject);
				Debug.Log ("버프종료완료");
				return true;
			}
		}
		Debug.LogWarning ("버프 못찾음 : " + buffData.GetType());

		return false;
	}

	/// <summary>
	/// 모든 버프 아이콘 지우기
	/// </summary>
	/// <returns>종료 여부 : true 정상 종료</returns>
	/// <param name="buffData">버프 데이터.</param>
	public bool buffAllEnd(){
		try{
			foreach (UIBuffIconClass uiBuffData in m_buffIconList) {
				Debug.LogWarning ("BuffAllEnd " + uiBuffData.buffData.GetType());
				Destroy (uiBuffData.gameObject);
			}
			m_buffIconList.Clear ();
			return true;
		}
		catch(UnityException e){
			Debug.LogWarning ("BuffAllEnd Error " + e.Message);
			return false;
		}
	}

}


