using System;
using UnityEngine;

public class DummyClass : CharacterCommonClass, ICharacterInterface 
{
	[SerializeField] TYPE_TEAM m_selectTeam;
	[SerializeField] TYPE_MOS m_dummyMos;
	[SerializeField] int m_nowHealth;
	[SerializeField] int[] equipmentArray;

	[SerializeField] bool m_isInvisible;
	[SerializeField] bool m_isView;
	[SerializeField] bool m_isAttack;
	[SerializeField] bool m_isMove;
	[SerializeField] bool m_isSkill;

	[Range(0, 3)]
	[SerializeField] int m_skillRun;

	NavMeshAgent2D m_navMesh;
	ICharacterInterface m_target = null;

	void Start(){
		base.Start ();
		gameReady (null, m_dummyMos, equipmentArray);
		m_team = m_selectTeam;
		m_navMesh = GetComponent<NavMeshAgent2D>();
		m_navMesh.speed = moveSpeed;

		m_health = m_nowHealth;
	}


	void Update(){
//		if(m_isMove)
//		if(m_isSkill)
	}

	/// <summary>
	/// 재장전
	/// </summary>
	public override void reloadAction (){
		base.reloadAction ();
		Debug.Log ("Dummy reloadAction");
	}

	public override void attackAction (float angle){
		if (!m_isAttack) return;
		base.attackAction (angle);
		Debug.Log ("Dummy attackAction");
	}

	public override void deadAction (IBullet bullet){
		base.deadAction (bullet);
		m_navMesh.enabled = false;
		Debug.Log ("Dummy deadAction");
	}

	public override void rebirthAction (){
		base.rebirthAction ();
		m_navMesh.enabled = true;
		Debug.Log ("Dummy rebirthAction");
	}

	public override bool hitAction (TYPE_TEAM team, IBullet bullet){
		if (m_isInvisible) return false;
		return base.hitAction (team, bullet);
		Debug.Log ("Dummy hitAction");
	}

}


