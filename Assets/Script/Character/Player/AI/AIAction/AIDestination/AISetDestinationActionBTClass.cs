using System;

public class AISetDestinationActionBTClass : AIActionBTClass
{
	public override bool Run (CPUClass cpu)
	{
		//UnityEngine.Debug.Log ("run : " + cpu.setDestination ());
		return cpu.setDestination ();
	}
}


