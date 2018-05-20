using System;


public class AISkillActionBTClass : AIActionBTClass
{
	public override bool Run (CPUClass cpu)
	{
		return cpu.skillAction ();
	}
}


