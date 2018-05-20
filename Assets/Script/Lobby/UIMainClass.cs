using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIMainClass : MonoBehaviour
{

    [SerializeField] Text m_versionText;
	[SerializeField] UIMsgClass m_msgPanel;



	void Awake(){
		SoundFactoryClass.GetInstance.initDictionary();
	}

    void Start()
    {

		//로딩창으로 옮겨야함

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
		m_versionText.text = "v" + Application.version + " alpha";
		AccountClass.GetInstance.initInstance ();
		EquipmentFactoryClass.GetInstance.initInstance ();
		MOSFactoryClass.GetInstance.initInstance ();
		MapFactoryClass.GetInstance.initInstance ();
		TextInfoFactoryClass.GetInstance.initInstance (); //A0.8

    }


	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (m_msgPanel.isActiveAndEnabled)
				m_msgPanel.gameObject.SetActive (false);
			else
				exitMsgView ();
		}
	}

    public void startButtonClicked()
    {

		AccountClass.GetInstance.playPanel.nextPanel = PrepClass.c_LobbyPanelScene;

		SceneManager.LoadScene(PrepClass.c_LoadPanelScene);

    }

	void exitMsgView(){
		m_msgPanel.initMsgPanel ("게임을 종료하시겠습니까?", exitMsgEvent, TYPE_MSG_SIGN.OKCANCEL, TYPE_MSG_ICON.WARNING);
	}


	void exitMsgEvent(){
		Application.Quit ();
	}
}

