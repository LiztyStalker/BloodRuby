using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;
using System.Linq;


public class UILobbyClass : MonoBehaviour {



    [SerializeField] UILobbyTitleClass m_titlePanel;
    [SerializeField] UILobbyParentClass m_optPanel;
	[SerializeField] UIMsgClass m_msgPanel;
//	[SerializeField] BtnSoundPackageClass m_btnSoundPackage;

    [SerializeField] UILobbyParentClass[] m_panels;

    Stack<UILobbyParentClass> m_stack = new Stack<UILobbyParentClass>();

	SoundPlayClass m_soundPlayer;

	public UIMsgClass msgPanel{ get { return m_msgPanel; } }

	// Use this for initialization
	void Awake () {

		m_soundPlayer = GetComponent<SoundPlayClass> ();

		if (m_soundPlayer == null)
			m_soundPlayer = gameObject.AddComponent<SoundPlayClass> ();
        //m_panels = GetComponentsInChildren<UILobbyParentClass>();
        //Debug.Log("panelLength : " + m_panels.Length);
        //foreach (UILobbyParentClass parent in m_panels)
        //{
        //    if(parent.isActiveAndEnabled)
        //        parent.gameObject.SetActive(false);
        //}
		//AccountClass.GetInstance.initInstance();
    }



    void Update()
    {
        if (Input.GetKeyDown (KeyCode.Escape))
        {
            if (!PopPanel())
            {
				exitMsgView ();
            }
        }
    }

    public void PopPanelClicked()
    {
        if (!PopPanel())
        {
            //패널 이동
			exitMsgView();
        }
    }

	/// <summary>
	/// 패널 빼기
	/// </summary>
	/// <returns><c>true</c>, if panel was poped, <c>false</c> otherwise.</returns>
    bool PopPanel()
    {
        if(m_stack.Count > 0){
            UILobbyParentClass panel = m_stack.Pop();

			if (!panel.ClosePanel ()) {
				panel.gameObject.SetActive (false);
			}
				
//            panel.gameObject.SetActive(false);

            if (m_stack.Count == 0)
            {
				if(m_titlePanel != null) m_titlePanel.gameObject.SetActive(false);
            }
			else if(m_titlePanel != null)
            {
				m_titlePanel.titleText = TranslatorClass.GetInstance.panelTranslator(m_stack.Peek().GetType());
            }

			m_soundPlayer.audioPlay (TYPE_BTN_SOUND.CANCEL);

            return true;
        }
        return false;
    }

	/// <summary>
	/// 패널 쌓기
	/// </summary>
	/// <param name="panel">Panel.</param>
    public void PushPanel(UILobbyParentClass panel)
    {
		m_soundPlayer.audioPlay (TYPE_BTN_SOUND.OK);
        m_stack.Push(panel);
    }


	void exitMsgView(){
		m_soundPlayer.audioPlay (TYPE_BTN_SOUND.WARNING);
		m_msgPanel.initMsgPanel ("게임을 종료하시겠습니까?", exitMsgEvent, TYPE_MSG_SIGN.OKCANCEL, TYPE_MSG_ICON.WARNING);
	}

	/// <summary>
	/// 종료 버튼
	/// </summary>
	public void exitBtnClickEvent(){
		m_soundPlayer.audioPlay (TYPE_BTN_SOUND.NONE);
		exitMsgView ();
	}

	/// <summary>
	/// 옵션 버튼
	/// </summary>
	public void optionBtnClickEvent(){
		m_soundPlayer.audioPlay (TYPE_BTN_SOUND.INFOR);
		optionPanelView ();
	}


	/// <summary>
	/// 어플 종료
	/// </summary>
    void exitMsgEvent()
    {
		Application.Quit ();
    }

	void optionPanelView(){
		m_optPanel.gameObject.SetActive (true);
	}

	/// <summary>
	/// 버튼 클릭시 패널 이름을 찾아서 보임
	/// </summary>
	/// <param name="panelName">Panel name.</param>
    public void viewPanelClicked(string panelName)
    {

        if (!viewPanel(Type.GetType(panelName)))
        {
            Debug.LogWarning("패널 없음 : " + panelName);
        }
    }

	/// <summary>
	/// 패널 보이기
	/// </summary>
	/// <returns><c>true</c>, if panel was viewed, <c>false</c> otherwise.</returns>
	/// <param name="typePanel">Type panel.</param>
    bool viewPanel(Type typePanel)
    {
        if (m_panels.Length > 0)
        {




            UILobbyParentClass panel = m_panels.Where(pan => pan.GetType() == typePanel).SingleOrDefault();

            if (panel != null)
            {
                panel.gameObject.SetActive(true);

//				if (panel.GetType() == Type.GetType("UIOptionClass")){	
////					if (m_titlePanel.isActiveAndEnabled) 
//
//				}
//				else if(panel.GetType() == Type.GetType("UIMsgClass"))
//                {
//                }
//
//                else
//                {
					if (!m_titlePanel.isActiveAndEnabled) m_titlePanel.gameObject.SetActive(true);

                    if (panel.GetType() == Type.GetType("UILobbyMosInforClass"))
                    {
                        TYPE_MOS mos = (TYPE_MOS)int.Parse(EventSystem.current.currentSelectedGameObject.gameObject.name.Substring(0, 1));
                        ((UILobbyMosInforClass)panel).setMosInforView(mos);
                    }

					//이름
					m_titlePanel.titleText = TranslatorClass.GetInstance.panelTranslator(panel.GetType());

//                }
				return true;

            }
                

        }
        return false;
    }

}
