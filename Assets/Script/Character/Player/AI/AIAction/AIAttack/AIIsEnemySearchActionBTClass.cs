using System;


public class AIIsEnemySearchActionBTClass : AIActionBTClass
{
	public override bool Run (CPUClass cpu)
	{
		return cpu.setTarget ();
	}
}


