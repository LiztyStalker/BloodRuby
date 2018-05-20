using System;
using System.Collections;
using UnityEngine;

public class TestTurretBuildingObjectClass : MonoBehaviour
{

	[SerializeField] BulletClass m_bullet;

	TYPE_TEAM m_team = TYPE_TEAM.ENEMY;
	float m_viewRange = 10f;
	ICharacterInterface character = null;
	ICharacterInterface m_target = null;

	void Start(){
		StartCoroutine (turretCoroutine ());
	}



	IEnumerator turretCoroutine(){


		while (gameObject.activeSelf) {

			if (m_target == null) {

				RaycastHit2D[] hits = Physics2D.CircleCastAll (transform.position, m_viewRange, Vector2.zero);

				foreach (RaycastHit2D hit in hits) {
					if (PrepClass.isCharacterTag(hit.collider.tag)) {
						if (hit.collider.GetComponent<ICharacterInterface> () != character) {
							if (hit.collider.GetComponent<ICharacterInterface> ().team != m_team) {
								if (!hit.collider.GetComponent<ICharacterInterface> ().isDead) {
									Debug.Log ("타겟 : " + hit.collider.name);

									RaycastHit2D[] rays = Physics2D.RaycastAll (transform.position, hit.transform.position - transform.position, m_viewRange);

									foreach (RaycastHit2D ray in rays) {
										if (PrepClass.isCharacterTag(ray.collider.tag)) {
											if (ray.collider.GetComponent<ICharacterInterface> ().team != m_team) {
												if (!ray.collider.GetComponent<ICharacterInterface> ().isDead) {
													Debug.Log ("리얼타겟 : " + ray.collider.name);
													m_target = ray.collider.GetComponent<ICharacterInterface> ();
													break;
												}
											}
										} 
									}

								}
							}
						}
					}


				}
			} else if (m_viewRange < Vector2.Distance (m_target.transform.position, transform.position)) {
				m_target = null;
			} else {

				if (m_target.isDead) {
					m_target = null;
					continue;
				}

				Vector2 dirVec = m_target.transform.position - transform.position;
				float angle = Mathf.Atan2 (dirVec.y, dirVec.x) * Mathf.Rad2Deg;

				BulletClass bullet = (BulletClass)Instantiate (m_bullet, transform.position, new Quaternion ());
				bullet.attack (null, null, 10, 10f, 10f, angle);
				//				Debug.Log ("weaponSprite : " + m_weaponSprite);
			}

			yield return new WaitForSeconds (PrepClass.c_timeGap);
		}

		//
	}

}


