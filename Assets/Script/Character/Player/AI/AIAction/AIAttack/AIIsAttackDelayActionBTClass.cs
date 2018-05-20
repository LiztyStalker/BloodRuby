using System;

public class AIIsAttackDelayActionBTClass : AIActionBTClass
{
	public override bool Run (CPUClass cpu)
	{
		return cpu.isAttackDelay ();
		//cpu.shootDelay
	}
}


