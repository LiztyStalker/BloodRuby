using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIInverterBTClass : AIDecoratorBTClass {

	public AIInverterBTClass(AINodeClass child) : base(child){}

	public override bool Run (CPUClass cpu)
	{
		return !child.Run (cpu);
	}
}
