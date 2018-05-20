using System.Collections;
using UnityEngine;
using Spine;
using Spine.Unity;
using Spine.Unity.Modules.AttachmentTools;
using System.Globalization;

public class MOSSkeletonClass : MonoBehaviour
{

	const float c_bodyScale = 0.6f;
	const float c_legScale = 1.0f;

	MOSDataClass m_mosData;

	[SerializeField] Transform m_root;

	SkeletonAnimation m_LegSkeletonAnimation;
	Spine.AnimationState m_legSkeletonAnimationState;
	Spine.Skeleton m_legSkeleton;


	SkeletonAnimation m_bodySkeletonAnimation;
	Spine.AnimationState m_bodySkeletonAnimationState;
	Spine.Skeleton m_bodySkeleton;

	Bone shootPosBone = null;
	Vector2 dir = Vector2.zero;


//	TextInfo ti = CultureInfo.CurrentCulture.TextInfo;

//	public delegate void AttackSkeletonDelegate(UICharacterClass characterCtrler, Vector3 shootPos);
//	public AttackSkeletonDelegate m_attackSkeletonDelegate;
//
//	public delegate void SkillSkeletonDelegate(ICharacterInterface player);
//	public SkillSkeletonDelegate m_skillSkeletonDelegate;

//	[SerializeField] Transform m_shootPos;

//	[SpineSlot] Sprite sprites;

//	Spine.Attachment attachments;

	public Vector2 shootPos{
		get{
			//현재 스파인 월드 좌표 가져오기


			shootPosBone = m_bodySkeleton.FindBone ("ShootPos");

			if (shootPosBone != null) {

				dir.x = m_bodySkeleton.FindBone ("ShootPos").WorldX; 
				dir.y = m_bodySkeleton.FindBone ("ShootPos").WorldY;

				//로컬 위치를 월드 위치로 변환하기
				return transform.TransformPoint ((Vector3)dir);
			} else {
				Debug.LogWarning ("본을 찾을 수 없음. 캐릭터 중심으로 대체 : " + name);
				return transform.position;
			}
			
		}
	}
	public Spine.AnimationState skeletonAnimationState{ get { return m_bodySkeletonAnimationState; } }

//	[System.Serializable]
//	public class SlotRegionPair {
//		[SpineSlot] - 몸 스파인 슬롯
//		public string slot;
//
//		[SpineAtlasRegion] - 알타리스 스파인 레기온
//		public string region;
//	}

//	public AtlasAsset atlasAsset;
//	public SlotRegionPair[] attachments;
//	class SlotRegionPair {
//		public string slot;
//		public string region;
//	}

	/// <summary>
	/// 애니메이션 초기화
	/// </summary>
	public void initAnimation(MOSDataClass mosData){


		m_mosData = mosData;



		transform.localScale = new Vector2 (c_bodyScale, c_bodyScale);

		m_bodySkeletonAnimation = GetComponent<SkeletonAnimation> ();

		m_bodySkeletonAnimationState = m_bodySkeletonAnimation.AnimationState;
		m_bodySkeleton = m_bodySkeletonAnimation.Skeleton;

		string legStr = string.Format ("Character/{0}_{1}/Leg/MOS@{0}_{1}SkeletonAnimationLeg", (int)mosData.mos, PrepClass.TypeTextInfo.ToTitleCase(mosData.mos.ToString ()));

		m_LegSkeletonAnimation = Instantiate (Resources.Load (legStr) as GameObject, transform.position, new Quaternion()).GetComponent<SkeletonAnimation>();
		m_LegSkeletonAnimation.transform.SetParent (m_root);
		m_LegSkeletonAnimation.transform.localScale = new Vector2 (c_legScale, c_legScale);
		m_legSkeletonAnimationState = m_LegSkeletonAnimation.AnimationState;
		m_legSkeleton = m_LegSkeletonAnimation.Skeleton;

		initSkeletonColor (mosData);

		m_bodySkeletonAnimation.transform.Rotate(0f, 0f, -90f);

	}

	/// <summary>
	/// 스켈레톤 슬롯 부위 컬러 변경
	/// </summary>
	/// <param name="mosData">Mos data.</param>
	void initSkeletonColor(MOSDataClass mosData){

		switch(mosData.mos){
		case TYPE_MOS.DUALIST:
			m_bodySkeleton.FindSlot ("Body").SetColor (PrepClass.getFlagColor (mosData.character.team, 0.2f));
			m_bodySkeleton.FindSlot ("UpperArmL").SetColor (PrepClass.getFlagColor (mosData.character.team, 0.2f));
			m_bodySkeleton.FindSlot ("UpperArmR").SetColor (PrepClass.getFlagColor (mosData.character.team, 0.2f));
			break;
		case TYPE_MOS.GUARDIAN:
			m_bodySkeleton.FindSlot ("Cape").SetColor (PrepClass.getFlagColor (mosData.character.team, 0.2f));
			break;
		case TYPE_MOS.FIREBAT:
			m_bodySkeleton.FindSlot ("HeadBand").SetColor (PrepClass.getFlagColor (mosData.character.team, 0.2f));
			m_bodySkeleton.FindSlot ("BagMark").SetColor (PrepClass.getFlagColor (mosData.character.team, 0.2f));
			break;
		case TYPE_MOS.ASSAULT:
			m_bodySkeleton.FindSlot ("Bag").SetColor (PrepClass.getFlagColor (mosData.character.team, 0.2f));
			break;
		case TYPE_MOS.HEAVY:
			m_bodySkeleton.FindSlot ("Bag").SetColor (PrepClass.getFlagColor (mosData.character.team, 0.2f));
			break;
		case TYPE_MOS.SNIPER:
			m_bodySkeleton.FindSlot ("Bag").SetColor (PrepClass.getFlagColor (mosData.character.team, 0.2f));
			break;
		case TYPE_MOS.MAGICIAN:
			m_bodySkeleton.FindSlot ("Body").SetColor (PrepClass.getFlagColor (mosData.character.team, 0.2f));
			m_bodySkeleton.FindSlot ("Cape").SetColor (PrepClass.getFlagColor (mosData.character.team, 0.2f));
			m_bodySkeleton.FindSlot ("ArmBandL").SetColor (PrepClass.getFlagColor (mosData.character.team, 0.2f));
			m_bodySkeleton.FindSlot ("ArmBandR").SetColor (PrepClass.getFlagColor (mosData.character.team, 0.2f));
			break;
		case TYPE_MOS.CLERIC:
			m_bodySkeleton.FindSlot ("BodyMark").SetColor (PrepClass.getFlagColor (mosData.character.team, 0.2f));
			m_bodySkeleton.FindSlot ("HeadMark").SetColor (PrepClass.getFlagColor (mosData.character.team, 0.2f));
			m_bodySkeleton.FindSlot ("ArmMarkL").SetColor (PrepClass.getFlagColor (mosData.character.team, 0.2f));
			m_bodySkeleton.FindSlot ("ArmMarkR").SetColor (PrepClass.getFlagColor (mosData.character.team, 0.2f));
			break;
		case TYPE_MOS.BUILDER:
			m_bodySkeleton.FindSlot ("Bag").SetColor (PrepClass.getFlagColor (mosData.character.team, 0.2f));
			break;
		}


//		m_bodySkeletonAnimation.Skeleton.SetAttachment ();
	}

//	void Apply(SkeletonRenderer skeletonRenderer) {
//		Debug.Log ("SkeletonOnBuild");
//
//
//
////		AtlasAsset atlasAsset;
////		SlotRegionPair[] attachments;
//
//		Atlas atlas;
//
//		Slot[] slots = skeletonRenderer.Skeleton.Slots.ToArray ();
//		string[] slotNames = new string[slots.Length];
//		for (int i = 0; i < slotNames.Length; i++){
//			
//			slotNames [i] = slots [i].attachment.Name;
//			Debug.Log ("slotBone : " + slots [i].bone.data.name);
//		}
//
//		//각 슬롯을 가져와서 아틀라스 에 있는 슬롯 삽입
//
//		atlas = atlasAsset.GetAtlas();
//		float scale = skeletonRenderer.skeletonDataAsset.scale;
//
//		//atlas.
//
////		var enumerator = attachments.GetEnumerator();
//		foreach(string slotName in slotNames) {
//
//			//스켈레톤 가져오기
//			Slot slot = skeletonRenderer.skeleton.FindSlot(slotName);
//			Debug.Log ("slotName : " + slot);
//
//			//스켈레톤 슬롯이 있으면
//			if(slot != null){
//				//아틀라스 부품 가져오기
//				AtlasRegion region = atlas.FindRegion(slotName);
//
//				if (region != null) {
//					Debug.Log ("region : " + region.name);
//
//					//아틀라스 부품 가져오기
//					RegionAttachment regionAtt = region.ToRegionAttachment (slotName, scale);
////					regionAtt.SetRotation ();
////					regionAtt.rotation -= 180f;
//
////					switch(region.name){
////					case "head":
////						slot.Data.BoneData.rotation -= 90f;
////						break;
////					case "body":
////						slot.Data.BoneData.rotation += 90f;
////						break;
////					case "arm_l":
////						slot.Data.BoneData.rotation -= 90f;
////						break;
////					case "arm_r":
////						slot.Data.BoneData.rotation -= 90f;
////						break;
////
////					}
////
//					slot.Attachment = regionAtt;
//
//				}
//
//			}
//
//		}
//
//	}


	/// <summary>
	/// 애니메이션 찾기 
	/// A0.7
	/// </summary>
	/// <returns><c>true</c>, if animation was ised, <c>false</c> otherwise.</returns>
	/// <param name="animationName">Animation name.</param>
	public bool isAnimation(string animationName){
		if (m_bodySkeleton.Data.FindAnimation (animationName) == null)
			return false;
		return true;
	}

	/// <summary>
	/// 애니메이션 실행
	/// </summary>
	/// <param name="animationName">Animation name.</param>
	/// <param name="isBodyLoop">If set to <c>true</c> is body loop.</param>
	/// <param name="isLegLoop">If set to <c>true</c> is leg loop.</param>
	/// <param name="bodytime">Bodytime.</param>
	/// <param name="legtime">Legtime.</param>
	public bool setAnimation(string animationName, bool isBodyLoop, bool isLegLoop, float bodytime, float legtime){
		if (animationName != "") {
			setLegAnimation (animationName, isLegLoop, legtime);
			return setBodyAnimation (animationName, isBodyLoop, bodytime);
		}
		return false;
	}



	/// <summary>
	/// 몸 애니메이션 실행 
	/// A0.7
	/// </summary>
	/// <returns><c>true</c>, if body animation was set, <c>false</c> otherwise.</returns>
	/// <param name="animationName">Animation name.</param>
	/// <param name="loop">반복횟수 0 무한, n회 반복.</param>
	/// <param name="time">반복 시간</param>
	public bool setBodyAnimation(string animationName, int loop, float time){



		if (m_bodySkeletonAnimation != null && animationName != "") {

			if (m_bodySkeleton.Data.FindAnimation (animationName) == null)
				return false;
			
			if (loop < 0) loop = 0;
			bool isLoop = (loop == 0) ? true : false;

			m_bodySkeletonAnimationState.SetAnimation (0, animationName, isLoop).timeScale = time;// + time * 0.1f;

			for(int i = 1; i < loop; i++){
				m_bodySkeletonAnimationState.AddAnimation(0, animationName, isLoop, time * i).timeScale = time;// + time * 0.1f;
			}

			return true;

		}
		return false;
	}


	/// <summary>
	/// 몸 애니메이션 실행
	/// </summary>
	/// <returns><c>true</c>, if body animation was set, <c>false</c> otherwise.</returns>
	/// <param name="animationName">Animation name.</param>
	/// <param name="isLoop">If set to <c>true</c> is loop.</param>
	/// <param name="time">Time.</param>
	public bool setBodyAnimation(string animationName, bool isLoop, float time){

		//재장전이면 끝날때까지 행동없음
//		m_bodySkeletonAnimationState.add

		if (m_bodySkeletonAnimation != null && animationName != "") {

			if (m_bodySkeleton.Data.FindAnimation (animationName) == null)
				return false;

			m_bodySkeletonAnimationState.SetAnimation (0, animationName, isLoop).timeScale = time;// + time * 0.1f;
			return true;

		}
		return false;
	}

	/// <summary>
	/// 다리 애니메이션 실행
	/// </summary>
	/// <returns><c>true</c>, if leg animation was set, <c>false</c> otherwise.</returns>
	/// <param name="animationName">Animation name.</param>
	/// <param name="isLoop">If set to <c>true</c> is loop.</param>
	/// <param name="time">Time.</param>
	public bool setLegAnimation(string animationName, bool isLoop, float time){
//		Debug.Log ("skeleton : " + m_LegSkeletonAnimation + " " + animationName + " : " + time);

		if (m_LegSkeletonAnimation != null && animationName != "") {

			if (m_legSkeleton.Data.FindAnimation (animationName) == null)
				return false;
			
			m_legSkeletonAnimationState.SetAnimation (0, animationName, isLoop).timeScale = time;// + time * 0.1f;
			return true;
		}
		return false;
	}


	/// <summary>
	/// 몸 애니메이션 실행
	/// </summary>
	/// <returns><c>true</c>, if body animation was set, <c>false</c> otherwise.</returns>
	/// <param name="animationName">Animation name.</param>
	/// <param name="isLoop">If set to <c>true</c> is loop.</param>
	/// <param name="time">Time.</param>
	public bool addBodyAnimation(string animationName, float time){

		//재장전이면 끝날때까지 행동없음
		//		m_bodySkeletonAnimationState.add

		if (m_bodySkeletonAnimation != null && animationName != "") {
//			Debug.Log ("addAnimationName : " + animationName);

			if (m_bodySkeleton.Data.FindAnimation (animationName) == null)
				return false;

			m_bodySkeletonAnimationState.AddAnimation (0, animationName, false, time).timeScale = time;// + time * 0.1f;
			return true;
		}
		return false;
	}
}


