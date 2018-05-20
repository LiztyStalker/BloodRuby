using System;

//A0.8 조건부 실행 - 자식은 1개만 가짐
public abstract class AIDecoratorBTClass : AINodeClass
{
	AINodeClass m_child;
	protected AINodeClass child{get{ return m_child; }}
	public AIDecoratorBTClass(AINodeClass child){m_child = child;}
}


