using System;


public class AIIsFlagTeamActionBTClass : AIActionBTClass
{
	public override bool Run (CPUClass cpu)
	{
		//현재 위치가 적 거점인지 확인
		return cpu.isCaptureTeam();
	}

}


