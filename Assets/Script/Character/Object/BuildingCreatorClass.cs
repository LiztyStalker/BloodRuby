using System.Collections;
using UnityEngine;

public class BuildingCreatorClass : MonoBehaviour
{
	[SerializeField] BuildingObjectClass m_buildingObjectPrefeb;
	[SerializeField] float m_angle = 0f;
	[SerializeField] bool m_isBroken = true;
	[SerializeField] bool m_repeat = false;

	BuildingObjectClass m_buildingObject;

	const float m_time = 30f;
	float m_runTime = 0f;


	void Start(){


		if (m_repeat)
			StartCoroutine (repeatCoroutine ());
		else
			m_buildingObject = (BuildingObjectClass)Instantiate (m_buildingObjectPrefeb, transform.position, new Quaternion ());

		if (m_buildingObject != null) {
			m_buildingObject.initBuilding (null, m_angle);
		} else {
			Debug.LogError ("건물 등록되지 않음 : " + gameObject.name + " " + transform.position);
		}

	}


	IEnumerator repeatCoroutine(){
		while (gameObject.activeSelf) {



			if (transform.childCount == 0) {
				m_runTime += PrepClass.c_timeGap;
				if (m_time < m_runTime) {
					m_buildingObject = (BuildingObjectClass)Instantiate (m_buildingObjectPrefeb, transform.position, new Quaternion ());

					if (m_buildingObject != null) {
						m_buildingObject.initBuilding (null, m_angle);

						m_buildingObject.transform.SetParent (transform);
						m_runTime = 0f;
					} 
				} else {
					m_runTime = 0f;
				}

				yield return new WaitForSeconds (PrepClass.c_timeGap);
			}
		}
	}


	void OnDrawGizmos()
	{



		if (m_buildingObjectPrefeb != null) {

//			Debug.Log ("bound : " + m_buildingObjectPrefeb.bound);

//			return;


			Rect rect = m_buildingObjectPrefeb.rect;


			if (rect != Rect.zero) {


				rect.size = rect.size * 0.5f;
	
				Mesh mesh = new Mesh ();

				mesh.vertices = new Vector3[] {
					new Vector3 (rect.width, rect.height, 0f),
					new Vector3 (-rect.width, rect.height, 0f),
					new Vector3 (rect.width, -rect.height, 0f),
					new Vector3 (-rect.width, -rect.height, 0f)
				};

				//		mesh.vertices = new Vector3[]{
				//			new Vector3(6.2f, 3.5f, 0f),
				//			new Vector3(-6.2f, 3.5f, 0f),
				//			new Vector3(6.2f, -3.5f, 0f),
				//			new Vector3(-6.2f, -3.5f, 0f)
				//		};

				mesh.triangles = new int[] {
					0, 1, 2, 1, 2, 3
				};


				mesh.RecalculateNormals ();

				Gizmos.color = Color.white;
				Gizmos.DrawWireMesh (mesh, transform.position, Quaternion.Euler (0f, 0f, m_angle));
			}

		}
	}


}


