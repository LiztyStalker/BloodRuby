using System;


public class AIIsSetAllyActionBTClass : AIActionBTClass
{
	public override bool Run (CPUClass cpu)
	{
		return cpu.isSetAlly ();
	}
}


