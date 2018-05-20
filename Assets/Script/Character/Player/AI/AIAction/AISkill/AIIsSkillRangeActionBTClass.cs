using System;


public class AIIsSkillRangeActionBTClass : AIActionBTClass
{
	public override bool Run (CPUClass cpu)
	{
		return cpu.isSkillRange ();
	}
}


