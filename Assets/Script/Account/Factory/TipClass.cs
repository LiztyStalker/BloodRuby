using System;
using UnityEngine;


//A0.8
public class TipClass
{
	Sprite m_image;
	string m_name;
	string m_contents;

	public Sprite image{ get { return m_image; } }
	public string name{ get { return m_name; } }
	public string contents{ get { return m_contents; } }

	public TipClass(Sprite image, string name, string contents){
		m_image = image;
		m_name = name;
		m_contents = contents;
	}


	
}


