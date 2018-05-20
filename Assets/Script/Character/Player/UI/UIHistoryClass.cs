//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.36388
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHistoryClass : MonoBehaviour
{

	[SerializeField] UIHistoryLabelClass m_labelPanel;
    [SerializeField] Transform[] m_parentPanel;


    List<UICharacterClass> m_characterSortList;
	bool m_isHistory = true;
   // List<UIHistoryLabelClass> m_labelList = new List<UIHistoryLabelClass>();

	/// <summary>
	/// 게임 내역 보여주기
	/// </summary>
	/// <param name="ctrler">Ctrler.</param>
	public void gameHistory(GameControllerClass ctrler){



		if (m_isHistory) {

			UIHistoryLabelClass[] historyLabels1 = m_parentPanel [0].GetComponentsInChildren<UIHistoryLabelClass> ();
			UIHistoryLabelClass[] historyLabels2 = m_parentPanel [1].GetComponentsInChildren<UIHistoryLabelClass> ();
			Debug.Log ("history : " + historyLabels1.Length + " " + historyLabels2.Length);


//			List<UIHistoryLabelClass> m_historyLabelList1 = new List<UIHistoryLabelClass> ();
//			List<UIHistoryLabelClass> m_historyLabelList2 = new List<UIHistoryLabelClass> ();

			m_characterSortList = new List<UICharacterClass> (ctrler.characters);
		
			m_characterSortList.Sort (
				(char1, char2) => {
					return char2.getReport (TYPE_REPORT.KILL).CompareTo (char1.getReport (TYPE_REPORT.KILL));
				} 
			);


//
//			UIHistoryLabelClass headLabel1 = Instantiate (m_labelPanel);
//			headLabel1.setLabel(TYPE_TEAM.TEAM_0, "", ctrler.ticketScore [0].ToString ());
//			headLabel1.transform.SetParent (m_parentPanel [0]);
			historyLabels1 [0].setLabel (TYPE_TEAM.TEAM_0, "", ctrler.ticketScore [0].ToString ());

//			UIHistoryLabelClass headLabel2 = Instantiate (m_labelPanel);
//			headLabel2.setLabel(TYPE_TEAM.TEAM_1, "", ctrler.ticketScore [1].ToString ());
//			headLabel2.transform.SetParent (m_parentPanel [1]);
			historyLabels2 [0].setLabel (TYPE_TEAM.TEAM_1, "", ctrler.ticketScore [1].ToString ());








			//왼쪽 팀
			int index = 1;
			for (int i = 0; i < m_characterSortList.Count; i++) {
				if (m_characterSortList [i].team == TYPE_TEAM.TEAM_0) {
					historyLabels1 [index++].setLabel (m_characterSortList [i]);
				} 
			}

			for (int i = index; i < historyLabels1.Length; i++) {
				historyLabels1 [i].gameObject.SetActive (false);
			}


			//오른쪽 팀
			index = 1;
			for (int i = 0; i < m_characterSortList.Count; i++) {
				if (m_characterSortList [i].team == TYPE_TEAM.TEAM_1) {
					historyLabels2 [index++].setLabel (m_characterSortList [i]);
				} 
			}

			for (int i = index; i < historyLabels2.Length; i++) {
				historyLabels2 [i].gameObject.SetActive (false);
			}
			m_isHistory = false;
		}

//        if (m_labelList.Count == 0)
//        {
//
//
//            UIHistoryLabelClass tmpTitle1 = (UIHistoryLabelClass)Instantiate(m_labelPanel);
//            tmpTitle1.setLabel(TYPE_TEAM.TEAM_0.ToString(), "-", ctrler.ticketScore[0].ToString());
//            tmpTitle1.transform.SetParent(m_parentPanel[0]);
//            m_labelList.Add(tmpTitle1);
//
//            UIHistoryLabelClass tmpTitle2 = (UIHistoryLabelClass)Instantiate(m_labelPanel);
//            tmpTitle2.setLabel(TYPE_TEAM.TEAM_1.ToString(), "-", ctrler.ticketScore[1].ToString());
//            tmpTitle2.transform.SetParent(m_parentPanel[1]);
//            m_labelList.Add(tmpTitle2);
//
//
//            m_characterSortList = new List<UICharacterClass>(ctrler.characters);
//
//            m_characterSortList.Sort(
//                (char1, char2) => 
//                {
//                    return char2.getReport(TYPE_REPORT.KILL).CompareTo(char1.getReport(TYPE_REPORT.KILL));
//                } 
//            );
//
//            foreach (UICharacterClass character in m_characterSortList)
//            {
//                UIHistoryLabelClass tmpCharacter = (UIHistoryLabelClass)Instantiate(m_labelPanel);
//                tmpCharacter.setLabel(character);
//                tmpCharacter.transform.SetParent(m_parentPanel[(int)character.team]);
//                m_labelList.Add(tmpCharacter);
//            }
//
//        }
	}


}

