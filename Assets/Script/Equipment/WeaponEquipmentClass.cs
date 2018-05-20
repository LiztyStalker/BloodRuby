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

public enum TYPE_ATTACK{DEFAULT, SLASH, THRUSH, SHOOT}
public enum TYPE_RANGE{LONG, SHORT}

public abstract class WeaponEquipmentClass : EquipmentClass
{


	[SerializeField] int m_damage; //공격력
	[SerializeField] float m_bulletSpeed; //탄환 속도
	[SerializeField] float m_shootDelay; //공격속도
	[SerializeField] float m_reloadDelay; //장전속도
	[SerializeField] float m_range; //사거리
	[SerializeField] int m_ammo; //장탄수
	[SerializeField] bool m_isConsume; //장탄 소비 = true 소비
//	[SerializeField] int m_ammoMax; //휴행탄수
	[SerializeField] TYPE_RANGE m_typeRange;
	[SerializeField] TYPE_ATTACK m_typeAttack;

	[Range(70f, 100f)]	
	[SerializeField] float m_accuracy; //명중률 
	[Range(0f, 100f)]
	[SerializeField] float m_recoil; //반동 - 수치가 높으면 명중률 저하 높음

	//[SerializeField] TYPE_BULLET m_typeBullet; //공격 방식


	[SerializeField] BulletClass m_bulletObject; //탄환
	[SerializeField] GameObject m_shootParticle; //공격 파티클
	[SerializeField] string m_attackSoundKey; //공격사운드 키
	[SerializeField] string m_reloadSoundKey; //재장전사운드 키







	public int damage {get{return m_damage;}}
	public float bulletSpeed {get{return m_bulletSpeed;}}
	public float range {get{return m_range;}}

	public float shootDelay{ get { return m_shootDelay; } }
	public float reloadDelay{ get { return m_reloadDelay; } }
	public int ammo{ get { return m_ammo; } }
	//public TYPE_BULLET typeBullet{ get { return m_typeBullet; } }

	public float accuracy{ get { return m_accuracy; } } 
	public float recoil{ get { return m_recoil; } }
	protected BulletClass bulletObject{get{return m_bulletObject;}}
	public GameObject shootParticle{get{return m_shootParticle;}}


	public string typeAttack{
		get{

			if(m_typeAttack == TYPE_ATTACK.DEFAULT)	return "";
			return "_" + m_typeAttack.ToString ().ToLower ();
		}
	}
	public TYPE_RANGE typeRange{get{return m_typeRange;}}
	public string attackSoundKey{ get { return m_attackSoundKey; } }
	public string reloadSoundKey{ get { return m_reloadSoundKey; } }
	public bool isConsume{ get { return m_isConsume; } }

	public abstract void attackAction (UICharacterClass characterCtrler, Vector2 shootPos);

}

