using System;


public class AIIsCapturePosActionBTClass : AIActionBTClass
{
	public override bool Run (CPUClass cpu)
	{
		//현재 위치가 거점지인지 확인
		return cpu.isCapturePosDestination();
	}
}


