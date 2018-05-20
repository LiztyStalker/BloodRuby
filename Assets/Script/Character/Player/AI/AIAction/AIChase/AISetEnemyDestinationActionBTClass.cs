using System;

public class AISetEnemyDestinationActionBTClass : AIActionBTClass
{
	public override bool Run (CPUClass cpu)
	{
		return cpu.enemyDestination ();
	}
}


