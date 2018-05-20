using System;
using UnityEngine;

public class SkillBuffClass : MonoBehaviour
{
	[SerializeField] BuffDataClass m_buff;

	public BuffDataClass getBuffData(){return m_buff;}
}


