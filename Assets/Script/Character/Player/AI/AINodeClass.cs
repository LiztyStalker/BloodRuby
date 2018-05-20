using System.Collections;
using UnityEngine;

public abstract class AINodeClass
{
//	CPUClass m_cpu;
//	protected CPUClass cpu{ get { return m_cpu; } }
//	public virtual void setCPU(CPUClass cpu){m_cpu = cpu;}
	public abstract bool Run (CPUClass cpu);
//	public virtual object Clone(){
//		return (object)this.MemberwiseClone();
//	}
	//public abstract virtual IEnumerator Run ();
}


