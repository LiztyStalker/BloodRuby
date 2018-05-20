using System;


public class AIIsGameRunActionBTClass : AIActionBTClass
{
	public override bool Run (CPUClass cpu)
	{
		return cpu.isGameRun;
	}
}


