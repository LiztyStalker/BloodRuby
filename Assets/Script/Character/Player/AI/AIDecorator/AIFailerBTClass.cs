using System;


public class AIFailerBTClass : AIDecoratorBTClass
{

	public AIFailerBTClass(AINodeClass child) : base(child){}

	public override bool Run (CPUClass cpu)
	{
		child.Run (cpu);
		return false;
	}
}


