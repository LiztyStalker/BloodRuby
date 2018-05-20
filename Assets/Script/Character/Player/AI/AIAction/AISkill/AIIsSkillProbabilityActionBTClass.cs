using System;


public class AIIsSkillProbabilityActionBTClass : AIActionBTClass
{
	public override bool Run (CPUClass cpu)
	{
		return cpu.isSkillProbability ();
	}
}


