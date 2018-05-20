using System;
using System.Collections.Generic;
using UnityEngine;




//public class PointClass{
//	Vector2 m_point;
//	public PointClass(Vector2 point){
//		m_point = point;
//	}
//	public Vector2 point{get{return m_point;}}
//}

//A0.8
public class PointerManagerClass
{

	List<Transform> m_pointers = new List<Transform>(); //위치 포인터 그룹
	float[][] m_edges;



	public PointerManagerClass (List<Transform> pointers)
	{
		m_pointers = pointers;

		int count = m_pointers.Count;
		if (m_pointers != null) {
			m_edges = new float[count][];

			for (int i = 0; i < count; i++) {
				m_edges [i] = new float [count];
				for (int j = 0; j < count; j++) {
					if (i == j)
						m_edges [i] [j] = -1f;
					else
						m_edges [i] [j] = Vector2.Distance (m_pointers[i].position, m_pointers[j].position);
					Debug.Log ("edge : " + m_edges [i] [j]);
				}
			}
		}
	}

	/// <summary>
	/// 포인트 가져오기
	/// </summary>
	/// <returns>The point.</returns>
	/// <param name="transform">Transform.</param>
	public Transform getPoint(Transform transform){


		if (transform == null) {
			return m_pointers [UnityEngine.Random.Range (0, m_pointers.Count)];
		}

		else if (m_pointers.Contains (transform)) {
			int vertex = m_pointers.IndexOf (transform);
			int index = 0;
			float edge = -1f;

			do {
				index = UnityEngine.Random.Range (0, m_pointers.Count);
				edge = m_edges [vertex] [index];
			} while(edge <= 0f);
				
			Debug.Log ("point : " + index);
			return m_pointers [index];
		}
		return null;
	}

}




