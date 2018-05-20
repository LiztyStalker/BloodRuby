using System;

public class AIIsSkillUsedActionBTClass : AIActionBTClass
{
	public override bool Run (CPUClass cpu)
	{
		return cpu.isSkillUsed ();
	}
}


