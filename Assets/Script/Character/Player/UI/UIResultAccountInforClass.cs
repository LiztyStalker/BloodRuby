using System;
using UnityEngine;
using UnityEngine.UI;

public class UIResultAccountInforClass : MonoBehaviour
{
	[SerializeField] Text m_playTimeText;
	[SerializeField] Text m_mainMosText;
	[SerializeField] Text m_KDAText;
	[SerializeField] Text m_CaptureText;

	public void gameResult(GameReportClass nowReport)
	{
		m_playTimeText.text = string.Format ("{0}", nowReport.totalTime);
		m_mainMosText.text = string.Format ("{0}", TranslatorClass.GetInstance.mosTranslator(nowReport.getBestMOS()));
		m_KDAText.text = nowReport.totalKDA;
		m_CaptureText.text = nowReport.totalFlag;
	}
}


