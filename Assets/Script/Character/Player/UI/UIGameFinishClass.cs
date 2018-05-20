using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

enum TYPE_RESULT_PANEL { TOTAL, PERSONAL }

class UIGameFinishClass : MonoBehaviour
{


    [SerializeField] Text m_nameText;
    [SerializeField] Text m_timeText;

    [SerializeField] UIHistoryClass m_historyPanel;
    [SerializeField] UIResultClass m_resultPanel;

    [SerializeField] Toggle[] m_toggleBtn;

    TYPE_RESULT_PANEL m_panel;

    GameControllerClass m_ctrler;

    void Start()
    {

        m_panel = TYPE_RESULT_PANEL.TOTAL;

        m_historyPanel.gameObject.SetActive(false);
        m_resultPanel.gameObject.SetActive(false);

        for (int i = 0; i < m_toggleBtn.Length; i++)
        {
            m_toggleBtn[i].onValueChanged.AddListener(delegate { OnValueChanged(); });
        }


        

    }

	/// <summary>
	/// 계정 결과 리포트 작성됨
	/// </summary>
	/// <param name="report">Report.</param>
    public void setReport(GameReportClass report)
    {
		Debug.Log ("리포트 작성");
        AccountClass.GetInstance.gameReport.addReport(report);
		m_resultPanel.gameResult(report);
    }

    /// <summary>
    /// 게임 내역 보여주기
    /// </summary>
    /// <param name="ctrler">Ctrler.</param>
    public void gameUpdate(GameControllerClass ctrler)
    {


        m_ctrler = ctrler;

        //게임 내역 보여주기 - 킬 데스 등
        //몇초 후 게임 결과 및 보상 보여주기

		m_nameText.text = TranslatorClass.GetInstance.modeTranslator(ctrler.mode) + " : " + TranslatorClass.GetInstance.mapTranslator(ctrler.mapData.mapKey);
        m_timeText.text = "다음 전투 진행 시간 : " + ctrler.time.ToString();



        viewPanel(ctrler);
        //게임 결과 패널로 보여주기 - 1번만 실행

    }


	void viewPanel(GameControllerClass ctrler)
    {

        switch (m_panel)
        {
            case TYPE_RESULT_PANEL.TOTAL:

                if (!m_historyPanel.isActiveAndEnabled)
                {
                    m_resultPanel.gameObject.SetActive(false);
                    m_historyPanel.gameObject.SetActive(true);

                }

                m_historyPanel.gameHistory(ctrler);

                break;
            case TYPE_RESULT_PANEL.PERSONAL:
                if (!m_resultPanel.isActiveAndEnabled)
                {
                    m_resultPanel.gameObject.SetActive(true);
                    m_historyPanel.gameObject.SetActive(false);

                }
                break;
        }
    }



    void OnValueChanged()
    {
        for(int i = 0; i < m_toggleBtn.Length; i++)
        {
            if (m_toggleBtn[i].isOn)
            {
                m_panel = (TYPE_RESULT_PANEL)i;
                viewPanel(m_ctrler);
                break;
            }
        }
    }

    /// <summary>
    /// 게임 종료
    /// </summary>
    public void gameExit()
    {
        //로딩창 띄우고 
        //로비로 이동하기
        AccountClass.GetInstance.playPanel.nextPanel = PrepClass.c_LobbyPanelScene;
		SceneManager.LoadScene(PrepClass.c_LoadPanelScene);
//        Application.LoadLevel(

    }


}

