using System;

public class AIIsSetFlagActionBTClass : AIActionBTClass
{
	public override bool Run (CPUClass cpu)
	{
		return cpu.isSetFlag ();
	}

}

