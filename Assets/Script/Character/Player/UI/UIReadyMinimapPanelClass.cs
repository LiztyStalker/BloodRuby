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
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIReadyMinimapPanelClass : MonoBehaviour
{



//	[SerializeField] Text m_modeText; //모드 텍스트
//	[SerializeField] Text m_mapNameText; //맵 이름 텍스트
//	[SerializeField] Text[] m_tecketText; //티켓 점수 텍스트
//	[SerializeField] Text m_timeText; //시간 텍스트

	[SerializeField] UIMinimapFlagClass m_minimapBtn;
	[SerializeField] RectTransform m_mapPanel;
	[SerializeField] RectTransform m_minimap;

	[SerializeField] UITicketDataClass m_ticketPanel;


	Dictionary<UIMinimapFlagClass, CaptureObjectClass> m_flagData;

	//Toggle[] m_flagBtns;
	//CaptureObjectClass[] m_flags;
	//int m_index = 0;


	/// <summary>
	/// 깃발 가져오기
	/// null인 경우 깃발 없음
	/// </summary>
	/// <value>The flag.</value>
	public CaptureObjectClass flag{
		get{
			foreach (UIMinimapFlagClass flag in m_flagData.Keys){
				if(flag.toggle.isOn){
                    return m_flagData[flag];
                }
			}
			return null;
		}
	}

	/// <summary>
	/// 거점 가져오기
	/// </summary>
	/// <param name="flags">Flags.</param>
//	public void setCapture(CaptureObjectClass[] flags){
//		//m_flags = flags;
//	}

	public void mapUpdate(GameControllerClass ctrler, TYPE_TEAM team){

		if (m_mapPanel != null) {

			//미니맵 깃발 초기화
			if (m_flagData == null) {
				m_flagData = new Dictionary<UIMinimapFlagClass, CaptureObjectClass> ();

				//현재 맵 세로 가져오기
				float height = ctrler.mapData.minimapSprite.texture.height * 4f;// * ctrler.mapData.mapSprite..localScale.y;


				//패널 세로 가져오기
				float pheight = m_mapPanel.rect.height - m_mapPanel.rect.height * 0.1f;

				//Debug.Log("rect : " + m_minimap.rect);

				//비율 맞추기
				float vratio = pheight / height;


				Debug.Log ("height : " + vratio + " " + height + " " + pheight);

				//비율에 맞춰 크기 삽입하기
				m_minimap.sizeDelta = vratio * 4f * new Vector2 (ctrler.mapData.minimapSprite.texture.width, ctrler.mapData.minimapSprite.texture.height);
				Debug.Log ("sizeDelta : " + m_minimap.sizeDelta);

				//이미지 삽입하기
				m_minimap.GetComponent<Image> ().sprite = ctrler.mapData.minimapSprite;

				//모든 깃발 순환 
				int i = 0;
				foreach (CaptureObjectClass flag in ctrler.mapData.flags) {


					UIMinimapFlagClass flagBtn = (UIMinimapFlagClass)Instantiate (m_minimapBtn);
					//깃발 위치 변경하기
					//전체 맵과 미니맵의 비율을 구하여 줄여야함
					Vector2 vec = Camera.main.WorldToScreenPoint (vratio * flag.transform.position);

					//전체 화면에서 약간 내리기 -37 (화면 비율에 따라서 크기를 낮춰야 함)
					vec.y -= Screen.height * 0.037f;

					//토글깃발 위치 정하기
					flagBtn.transform.position = vec;

					//토글깃발  버튼 설정
					flagBtn.transform.SetParent (m_minimap.transform);

					//그룹 설정
					flagBtn.toggle.group = m_minimap.gameObject.GetComponent<ToggleGroup> ();
					//flagBtn.colors.normalColor = PrepClass.getFlagColor(flag.team);


                    //거점 불가인 점령지인 경우 팀 이름 삽입
                    //if(!flag.isCaptured)
                    //    flagBtn.setCaptureName(flag.name, "-");
                    //else
                    //    flagBtn.setCaptureName (flag.name, ((char)('A' + i++)).ToString ());

                    flagBtn.setCaptureName(flag.name, flag.flagTag);
					flagBtn.setFlagView (flag, team);
					//flag.addFlagDelegate(flagBtn.flagSet);

					//깃발 리스트에 삽입
					m_flagData.Add (flagBtn, flag);


					//flagBtn.colors flag.team
				}

				//버튼 기본값 설정
				foreach (UIMinimapFlagClass flag in m_flagData.Keys) {
                    if (flag.toggle.interactable)
                    {
                        flag.toggle.isOn = true;
                        break;
                    }
				}

			} 
			//미니맵이 있으면
			else {


				//현재 매칭되는 토글깃발 새로고침
				foreach (UIMinimapFlagClass flag in m_flagData.Keys) {
					flag.setFlagView (m_flagData [flag], team);
				}



                

			}
		}


		m_ticketPanel.gameUpdate (ctrler);

		//티켓 및 시간 띄우기
		//모드 띄우기
		
		



		//컨트롤러에 있는 맵 데이터 갱신
		//거점데이터, 맵 데이터
		//거점량, 모드, 시간 등
		//거점은 토글 형식으로 되어 있으며 새로고침 실시
	}



	/// <summary>
	/// 팀에 맞는 거점 개수 가져오기
	/// </summary>
	/// <returns>The flag count.</returns>
	/// <param name="team">Team.</param>
	public int getFlagCount(TYPE_TEAM team){
		return m_flagData.Count (flagData => flagData.Value.team == team);
	}

}


