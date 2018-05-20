using System;
using UnityEngine;

public class ArmorEquipmentClass : EquipmentClass
{
	[SerializeField] int m_health; //체력
	[SerializeField] float m_moveSpeed; //이동속도 

	public int health{ get { return m_health; } }
	public float moveSpeed{ get { return m_moveSpeed; } }
}


