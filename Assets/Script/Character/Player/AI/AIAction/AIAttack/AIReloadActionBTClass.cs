using System;

public class AIReloadActionBTClass : AIActionBTClass
{
	public override bool Run (CPUClass cpu)
	{
		cpu.reloadAction ();
		return true;
	}
}


