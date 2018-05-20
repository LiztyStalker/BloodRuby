using System;
using System.Collections;
using UnityEngine;

public class UILobbyMosInforClass : UILobbyParentClass
{

	[SerializeField] UIReadyMOSClass m_readyMosPanel;
//	[SerializeField] UIReadyEquipmentClass m_readyEquipmentPanel;



    public void setMosInforView(TYPE_MOS mos){
		m_readyMosPanel.initMOS (mos);
    }




}

