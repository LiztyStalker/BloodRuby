using System;
using UnityEngine;

public enum TYPE_ITEM{HEALTH, SHIELD, COOLTIME, RUBY, COIN, INVICIBLE}


public class ItemObjectClass : ObjectClass
{
	[SerializeField] string m_key;
	[SerializeField] string m_name;
	[SerializeField] TYPE_ITEM m_typeItem;
	[SerializeField] int m_value;
	[SerializeField] int m_weight;
	[SerializeField] BuffDataClass m_buffData;

	public override void useObject (){}
	public override void releaseObject(){}


	public string key{get{return m_key;}}
	public string name{ get { return m_name; } }
	public TYPE_ITEM typeItem{get{return m_typeItem;}}
	public int value{get{return m_value;}}
	public int weight{get{return m_weight;}}
	public BuffDataClass buffData{get{return m_buffData;}}

	void OnTriggerEnter2D(Collider2D col){
		if (PrepClass.isCharacterTag (col.tag)) {
			if (!col.GetComponent<ICharacterInterface> ().isDead) {
				col.GetComponent<ICharacterInterface> ().itemAction (this);
				removeObject (gameObject);
			}
		}
	}
}


