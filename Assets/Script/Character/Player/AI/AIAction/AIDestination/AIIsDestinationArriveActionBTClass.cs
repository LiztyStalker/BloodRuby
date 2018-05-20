using System;

public class AIIsDestinationArriveActionBTClass : AIActionBTClass
{
	public override bool Run (CPUClass cpu)
	{
		//목적지 유무
		//도착시 false 반환
//		UnityEngine.Debug.Log ("Arrive");
		return cpu.isDestinationArrive();
	}
}

