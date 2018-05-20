using System;

public class AIIsSetEnemyActionBTClass : AIActionBTClass
{
	public override bool Run (CPUClass cpu)
	{
		return cpu.isSetEnemy ();
	}
}


