using System;

public class AISucceederBTClass : AIDecoratorBTClass
{
	public AISucceederBTClass(AINodeClass child) : base(child){}

	public override bool Run (CPUClass cpu)
	{
		child.Run (cpu);
		return true;
	}
}


