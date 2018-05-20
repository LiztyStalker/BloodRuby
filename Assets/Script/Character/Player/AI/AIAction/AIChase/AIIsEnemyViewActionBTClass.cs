using System;


public class AIIsEnemyViewActionBTClass : AIActionBTClass
{
	public override bool Run (CPUClass cpu)
	{
		return cpu.isEnemyView ();
	}
}


