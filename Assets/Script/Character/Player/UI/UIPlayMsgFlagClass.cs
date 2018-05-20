using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class UIPlayMsgFlagClass : UIPlayMsgParentClass
{
    [SerializeField] UIMinimapFlagClass m_flag;
	CaptureObjectClass m_flagData;

	public void setMsgFlag(string msg, CaptureObjectClass flag)
	{
		m_flagData = flag;
		setMsg(msg, PrepClass.c_timeGap, flagReflash);

	}

	void flagReflash(){
		m_flag.setCaptureName(m_flagData.name, m_flagData.flagTag);
		m_flag.setFlagView(m_flagData, TYPE_TEAM.NONE);
	}

}

