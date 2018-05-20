using System;
using UnityEngine;

public class InstanceTaggingBuffDataClass : TaggingBuffDataClass
{
//	[SerializeField] float m_time = 1f;
//	[SerializeField] float m_radius;

//	protected override void Start(){
//		startCoroutine (time, null);
//	}

	public override void buffStart (ICharacterInterface ownerCharacter, ICharacterInterface setCharacter)
	{
		base.buffStart (ownerCharacter, setCharacter);
		addValueState (this);
		instanceTaggingBuff (range);
	}

	public override bool buffEnd ()
	{
		returnValueState (this);
		buffClear ();
		return base.buffEnd ();
	}
		

	/// <summary>
	/// 버프 강제 종료
	/// </summary>
	protected void buffClear(){

		if (!isManage) return;

		if (characterList.Count > 0) {
			foreach (ICharacterInterface bufCha in characterList) {
				try{
					Debug.LogWarning("버프 강제 종료 : " + bufCha.name);
					BuffDataClass buff = bufCha.addState.getBuff(buffData.GetType());
					if(buff != null)
						buff.buffEnd();
				}
				catch{
				}
			}
		}

		characterList.Clear ();
	}


//	public override bool buffReplace ()
//	{
//		buffReplace (time);
//		return true;
//	}

	void OnDrawGizmos(){
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere  (transform.position, range);
	}
}


