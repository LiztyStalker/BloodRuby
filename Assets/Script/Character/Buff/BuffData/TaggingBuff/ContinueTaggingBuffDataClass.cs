using System;
using UnityEngine;

public class ContinueTaggingBuffDataClass : TaggingBuffDataClass
{
	[SerializeField] float m_radius;

	public override void buffStart (ICharacterInterface ownerCharacter, ICharacterInterface setCharacter)
	{
		base.buffStart (ownerCharacter, setCharacter);
		GetComponent<CircleCollider2D> ().radius = m_radius;
	}


	/// <summary>
	/// 접근시 버프데이터 붙이기
	/// </summary>
	/// <param name="col">Col.</param>
	void OnTriggerEnter2D(Collider2D col){
		setCollider (col);
	}


	/// <summary>
	/// 버프 제거하기
	/// </summary>
	/// <param name="col">Col.</param>
	void OnTriggerExit2D(Collider2D col){
		resetCollider (col);
	}


	void OnDrawGizmos(){
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere  (transform.position, m_radius);
	}
}


