using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A0.8
public abstract class AICompositeBTClass : AINodeClass
{

	List<AINodeClass> m_children = new List<AINodeClass>();
//	protected bool result = false;
//	protected bool isFinished = false;


	protected List<AINodeClass> children { get { return m_children; } }

	/// <summary>
	/// 종료
	/// </summary>
	/// <param name="r">If set to <c>true</c> r.</param>
//	public virtual void SetResult(bool r){
//		result = r;
//		isFinished = true;
//	}

//	public override void setCPU (CPUClass cpu)
//	{
//		foreach (AINodeClass node in children) {
//			node.setCPU (cpu);
//		}
//	}

	public void addChild(AINodeClass child){
//		child.setCPU (cpu);
		m_children.Add (child);

	}

	/// <summary>
	/// 행동
	/// </summary>
//	public override IEnumerator Run(){
//		SetResult (true);
//		yield break;
//	}


	public override bool Run (CPUClass cpu)
	{
		throw new System.NotImplementedException ();
	}

	/// <summary>
	/// 일반 행동
	/// </summary>
	/// <returns>The task.</returns>
//	public virtual IEnumerator RunTask(){
//		yield return StartCoroutine (Run ());
//	}
}



