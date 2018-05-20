using System;
using System.Collections.Generic;
using UnityEngine;

//모든 텍스트 번역
//A0.8
public class ContentsFactoryClass : SingletonClass<ContentsFactoryClass>
{
	Dictionary<string, string> m_contentsDic = new Dictionary<string, string>();

	public ContentsFactoryClass(){
		initParse ();
	}

	void initParse(){
		TextAsset tAsset = Resources.Load<TextAsset> (PrepClass.getLanguagePath (PrepClass.contentsPath));

		if (tAsset != null) {
			string[] splitList = tAsset.text.Split ('\n');

			foreach (string splitStr in splitList) {
				string[] data = splitStr.Split ('\t');
				m_contentsDic.Add (data [0], data [1]);
			}
		}
	}

	public string getContents(string key){
		if (m_contentsDic.ContainsKey (key))
			return m_contentsDic [key];
		return null;
	}
}


