using System;


public class AIIdleActionBTClass : AIActionBTClass
{
	public override bool Run (CPUClass cpu)
	{
		
		cpu.moveAction (0f, 0f);
		return true;
	}
}

