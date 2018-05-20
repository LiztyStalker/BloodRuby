using System;
using System.Collections.Generic;
using UnityEngine;

public class UILobbyParentClass : MonoBehaviour
{


    void OnEnable()
    {
        GetComponentInParent<UILobbyClass>().PushPanel(this);
    }


	public virtual bool ClosePanel(){return false;}

}

