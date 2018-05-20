using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UILoadingClass : MonoBehaviour {



	[SerializeField] Image m_imagePanel;
	[SerializeField] Text m_tipText;

	[SerializeField] Slider m_loadBar;
	[SerializeField] Text m_loadPercentText;
	[SerializeField] Text m_loadContentsText;

	AsyncOperation loadSceneAsync = null;
	TipClass m_tipData = null;
	
	
	void Start(){

//		if(AccountClass.GetInstance.playPanel.nextPanel == null)
//			AccountClass.GetInstance.playPanel.nextPanel = PrepClass.c_MainPanelScene;
//		loadSceneAsync.allowSceneActivation
		m_tipData = TipFactoryClass.GetInstance.getRandomTip();
		m_imagePanel.sprite = m_tipData.image;
		m_tipText.text = string.Format ("TIP {0}:{1}", m_tipData.name, m_tipData.contents);
		StartCoroutine (loadSceneCoroutine());
	}
	
	
	/// <summary>
	/// 게임 화면으로 이동
	/// </summary>
	/// <returns>The scene coroutine.</returns>
	IEnumerator loadSceneCoroutine(){
		
//		PlayerPrefs.DeleteKey ("isLoad");
		loadSceneAsync = SceneManager.LoadSceneAsync(AccountClass.GetInstance.playPanel.nextPanel);

		while (!loadSceneAsync.isDone) {
			Debug.Log("load Scene " + loadSceneAsync.progress);
			m_loadPercentText.text = string.Format("{0:f0}%", loadSceneAsync.progress * 100f);
			m_loadBar.value = loadSceneAsync.progress;
			yield return null;
		}
	}
}
