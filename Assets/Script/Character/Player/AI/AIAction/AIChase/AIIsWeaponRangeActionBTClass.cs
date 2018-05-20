using System;


public class AIIsWeaponRangeActionBTClass : AIActionBTClass
{
	public override bool Run (CPUClass cpu)
	{
		return cpu.isWeaponRange ();
	}
}


