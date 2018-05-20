using System;

public class AIAttackActionBTClass : AIActionBTClass
{
	public override bool Run (CPUClass cpu)
	{
//		Debug.LogError ("attack");
		cpu.attackAction ();
		return true;
	}
}


