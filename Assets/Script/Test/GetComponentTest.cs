using System;
using UnityEngine;

public class GetComponentTest : MonoBehaviour
{
	[SerializeField] GameObject m_obj;

	GameObject otherObj;

	void Start(){

		RaycastHit2D ray = Physics2D.CircleCast (transform.position, 1f, Vector2.zero);

		Debug.LogWarning ("ray : " + ray.collider.GetComponent<GameObject> ().GetInstanceID ());
		Debug.LogWarning ("obj : " + m_obj.GetInstanceID ());
	}
}


