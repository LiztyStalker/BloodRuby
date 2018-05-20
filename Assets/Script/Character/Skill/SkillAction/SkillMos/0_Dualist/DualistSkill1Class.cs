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



public class DualistSkill1Class : SkillClass, ISkillMOSInterface, IBullet
{

	[SerializeField] ParticleLifeClass m_bulletParticle;
	[SerializeField] float m_movement;
	[SerializeField] int m_damage;
	[SerializeField] TYPE_VALUE m_valueType;
	float m_range;

    ICharacterInterface m_player;

	Vector3 m_pastPos = Vector2.zero;
	Vector2 m_moveVector = Vector2.zero;

    ICharacterInterface character{get{return m_player;}}
	public UICharacterClass characterCtrler{get{return m_player.characterCtrler;}}
    public Type type { get { return this.GetType(); } }
	public bool isPenetrate{ get { return false; } set{}}
	public int damage{ get { return m_damage; } set{}}
	public bool isInTrench{get{return true;}}
	public Sprite weaponSprite{get{return iconRound;}}

	public override bool skillAction(ICharacterInterface player){
        m_player = player;

		setParticle (player.transform.position);

        //장외로 넘어가면 안되니 예외조건 필요
		//현재위치
		m_pastPos = new Vector3(player.transform.position.x, player.transform.position.y);

		//최종목표 위치 연산
		if (player.GetType () == typeof(CPUClass)) {
            //컴퓨터이면 타겟을 향해 회전 후 이동 계산
            ((CPUClass)player).targetRotate();
			m_moveVector = PrepClass.movementCalculator (m_movement, player.angle);
		} else {
            //스킬이 향하는 각도로 이동 계산
            m_moveVector = PrepClass.movementCalculator(m_movement, player.skillAngle);
		}

        //최종목표 계산
		m_moveVector.Set (
            m_moveVector.x + player.transform.position.x, 
            m_moveVector.y + player.transform.position.y
            );
	

		//해당 선분 내에 걸리는 콜라이더 파악
		RaycastHit2D[] rayHits = Physics2D.LinecastAll  (m_pastPos, m_moveVector);
		bool isChk = false;

        //걸린 모든 콜라이더 판단
		foreach (RaycastHit2D rayHit in rayHits) {
            //캐릭터는 무시
			if (PrepClass.isCharacterTag (rayHit.collider.tag))
				continue;
            //벽이거나 오브젝트이면
			else if (rayHit.collider.tag == "Wall" || 
                    rayHit.collider.tag == "ActObject") {

                //스킬 역각도의 0.1 길이만큼 위치 가져오기
				Vector2 centroid = PrepClass.movementCalculator (
                                        0.1f, 
                                        PrepClass.reverseAngleCalculator(player.skillAngle)
                                        );

                //ray에 걸린 위치와 스킬 역각도 위치 더하기
				centroid.Set (
                    centroid.x + rayHit.centroid.x,
                    centroid.y + rayHit.centroid.y
                    );

                //스킬 거리 - 스킬탄환이 생성하기 위하여 중간값을 가져옴
				m_range = Vector2.Distance (m_pastPos, centroid) * 0.5f;
                //이동위치 삽입
				player.transform.position = centroid;

                //오브젝트 충돌 여부 
				isChk = true;
				break;
			} 
		}

        //오브젝트나 벽에 충돌하지 않았으면
		if (!isChk) {
            //해당 거리만큼 이동
			m_range = Vector2.Distance (m_pastPos, m_moveVector) * 0.5f;
			if (player.GetType () == typeof(CPUClass))
				((CPUClass)player).warpPosition (m_moveVector);
			else
				player.transform.position = m_moveVector;
		}

		Debug.Log ("range : " + m_range);



		//탄환 생성 및 데이터 삽입
		ParticleLifeClass bullet = Instantiate(m_bulletParticle, player.transform.position, Quaternion.identity);
        //탄환 설정
		bullet.setParticleBox(player.characterCtrler, iconRound, m_damage, m_range * 2f, 1f, true, false, 1f, player.angle);
		return base.skillAction (player);
	}

	public override void skillGuideLine(ICharacterInterface player){
		//플레이어의 앞에 가이드라인 보이기
		//


		//적 찾기
		//가장 가까운 적 목표로 가져오기
		player.setSkillGuideLine (this);//, getTarget(player, TYPE_TEAM.ENEMY));


	}


}

