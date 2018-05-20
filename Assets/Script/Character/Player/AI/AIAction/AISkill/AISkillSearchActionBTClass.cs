using System;


public class AISkillSearchActionBTClass : AIActionBTClass
{
	public override bool Run (CPUClass cpu)
	{
		return cpu.skillSearch ();
	}
	
}


