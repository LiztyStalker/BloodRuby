using System;

public class AIIsLowHPActionBTClass : AIActionBTClass
{
	public override bool Run (CPUClass cpu)
	{
		return cpu.isLowHP ();
	}
	
}


