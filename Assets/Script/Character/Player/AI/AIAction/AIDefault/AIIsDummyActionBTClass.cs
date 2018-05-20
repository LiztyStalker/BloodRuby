using System;


public class AIIsDummyActionBTClass : AIActionBTClass
{
	public override bool Run (CPUClass cpu)
	{
		return cpu.isDummy;
	}
}


