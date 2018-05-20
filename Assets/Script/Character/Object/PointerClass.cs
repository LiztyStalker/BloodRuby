using System;
using System.Collections;
using UnityEngine;


public class PointerClass : MonoBehaviour
{
	[SerializeField] ItemZoneObjectClass m_itemZoneObject;

	void Start(){
		if(m_itemZoneObject != null)
			Instantiate(m_itemZoneObject,transform.position, new Quaternion()).transform.SetParent(transform);
	}


    void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}

