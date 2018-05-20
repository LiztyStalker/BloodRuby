using System;
using UnityEngine;

public class DistanceTestClass : MonoBehaviour
{
	[SerializeField] GameObject m_target;
	[SerializeField] bool m_isLogger = true;

	void Update(){
		Debug.Log("Distance : " + Vector2.Distance(transform.position, m_target.transform.position));
	}


	void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine (transform.position, m_target.transform.position);
		if(m_isLogger)
			Debug.LogWarning("Distance : " + Vector2.Distance(transform.position, m_target.transform.position));
	}
}



