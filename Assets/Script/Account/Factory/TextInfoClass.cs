using System;

//A0.8
public class TextInfoClass
{


	string m_key;
	string m_prefebName;
	string m_name;
	string m_contents;


	public string key{get{return m_key;}}
	public string prefebName{get{return m_prefebName;}}
	public string name{get{return m_name;}}
	public string contents{get{return m_contents;}}

	public static int Count{get{return 4;}}

	public TextInfoClass(string key, string prefebName, string name, string contents){
		m_key = key;
		m_prefebName = prefebName;
		m_name = name;
		m_contents = contents;
	}
}


