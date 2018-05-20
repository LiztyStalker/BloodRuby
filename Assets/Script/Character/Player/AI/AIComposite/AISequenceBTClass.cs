using System;
using  System.Collections;

//A0.8
public class AISequenceBTClass : AICompositeBTClass
{
//	public override void SetResult(bool r)
//	{
//		if(r == true)
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
			//1개가 거짓이면 거짓 반환 (and)
			if (!node.Run (cpu)) {
				return false;
			}
		}
		return true;
	}




}



