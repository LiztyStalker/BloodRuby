using System;

public class AIIsDestinationActionBTClass : AIActionBTClass
{
	public override bool Run (CPUClass cpu)
	{
		//목적지 유무
		//UnityEngine.Debug.Log("Run : " + cpu.isDestination());
		return cpu.isDestination();
	}
}


