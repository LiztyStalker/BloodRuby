using System;
using UnityEngine;

public class ExplosiveBuildingActionObjectClass : BuildingActionObjectClass, IBullet
{
	[SerializeField] int m_damage;
	[SerializeField] float m_range;
	[SerializeField] float m_impact;
	[SerializeField] ParticleSystem m_particle;

//	ICharacterInterface m_character;

	ICharacterInterface character{get{ return m_characterCtrler.character; }}
	public UICharacterClass characterCtrler{get{ return m_characterCtrler; }}
	public Type type{get{ return GetType(); }}
	public bool isPenetrate{ get{ return false; } set{ }}
	public int damage{ get{ return m_damage; } set{ }}
	public bool isInTrench{get{ return false; }}
	public Sprite weaponSprite{get{ return m_weaponSprite; }}


	/// <summary>
	/// 폭파
	/// </summary>
	/// <param name="gameObj">삭제되는 오브젝트</param>
	/// <param name="character">가해자</param>
	public void removeObject (GameObject removeGameObj, UICharacterClass characterCtrler)
	{
		m_characterCtrler = characterCtrler;

		RaycastHit2D[] hits = Physics2D.CircleCastAll (transform.position, m_range, Vector2.zero);
		foreach (RaycastHit2D hit in hits) {
			if (PrepClass.isCharacterTag (hit.collider.tag)) {
				ICharacterInterface enemyCharacter = hit.collider.GetComponent<ICharacterInterface> ();
				if (!enemyCharacter.isDead) {
					enemyCharacter.hitAction (TYPE_TEAM.NONE, this);
				}
			} 
			else if (hit.collider.tag == "ActObject") {
				if (hit.collider.transform.parent != null) {
					hit.collider.transform.parent.GetComponent<BuildingObjectClass> ().hitAction (TYPE_TEAM.NONE, this);
				}
			}
		}
	}
}


