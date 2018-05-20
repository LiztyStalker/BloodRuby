using System;
using System.Collections;

//A0.8
public class AISelectorBTClass : AICompositeBTClass
{
//	public override void SetResult (bool r)
//	{
//		if (r == true) 
//			isFinished = true;
//	}
//
//	public override IEnumerator RunTask ()
//	{
//		foreach (AICompositeBTClass t in children) {
//			yield return StartCoroutine (t.RunTask ());
//		}
//	}

	public override bool Run (CPUClass cpu)
	{
		foreach (AINodeClass node in children) {
			//1개가 참이면 참 반환 (or)
			if (node.Run (cpu)) {
				return true;
			}
		}
		return false;
	}
}


