//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.36373
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;
public class BuilderSkill0Class : SkillClass, ISkillMOSInterface
{

//	[SerializeField] 버프
//	[SerializeField] float m_time;
//	[SerializeField] float m_rate;

	[SerializeField] int m_repairPoint;
	[SerializeField] float m_range;
	[SerializeField] ParticleSystem m_repairParticle;

	public override void initSkill (ICharacterInterface player){
		base.initSkill (player);
		player.buffAdd (skillBuffData.getBuffData (), player, player);
	}

	public override bool skillAction(ICharacterInterface player){

		setParticle (player.transform.position, player.transform);

		RaycastHit2D[] hits = Physics2D.CircleCastAll (player.transform.position, m_range, Vector2.zero);

		foreach (RaycastHit2D hit in hits) {
			if (hit.collider.tag == "ActObject") {
				if (hit.collider.transform.parent.GetComponent<BuildingObjectClass> ().team == player.team) {
					hit.collider.transform.parent.GetComponent<BuildingObjectClass> ().addHealth (m_repairPoint);
					if(m_repairParticle != null){
						ParticleSystem repairParticle = Instantiate (m_repairParticle, hit.collider.transform.position, Quaternion.identity);
						repairParticle.transform.SetParent (hit.collider.transform);
					}
				}
			}
		}
		return base.skillAction (player);

	}

}

