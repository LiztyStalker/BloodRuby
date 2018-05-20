using System;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class AIParserClass
{

	Stack<XmlNode> stack = new Stack<XmlNode>();

	public AIParserClass ()
	{
		initParse ();
	}

	void initParse(){
		TextAsset tAsset = Resources.Load<TextAsset> ("Data/AI/ai");
		Debug.LogError ("tAsset : " + tAsset.text);
		if (tAsset != null) {
			XmlDocument xmlDoc = new XmlDocument ();

			xmlDoc.LoadXml (tAsset.text);

			XmlNode root = xmlDoc.DocumentElement;
			Debug.LogError ("xmlDoc : " + root.ChildNodes.Count);
			if (root.HasChildNodes) {
				stack.Push (root);

				//selector, sequence, decorator는 stack에 삽입 /문이 나오면 스택 팝
				//클래스 생성
				//action은 클래스 생성

				//
//				for (int i=0; i<root.ChildNodes.Count; i++)
//				{
//					root.ChildNodes[i].ChildNodes
//
//					Debug.LogError(root.ChildNodes[i].InnerXml);
//				}
			}


			//selector, Sequence, Invertor - 스택 삽입

		} else {
			Debug.LogError ("AI 설정되지 않음");
		}
	}


	void setNode(XmlNode XmlNode){
		
	}


	public AISequenceBTClass getRoot(){
		return null;
	}
}


