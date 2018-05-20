using System;


public class AIIsAmmoActionBTClass : AIActionBTClass
{
	public override bool Run (CPUClass cpu)
	{
//		Debug.LogError ("ammo");
		if (cpu.useAmmo <= 0)
			return false;
		return true;
	}
}


